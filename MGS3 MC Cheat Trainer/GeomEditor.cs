using System.Globalization;

namespace MGS3_MC_Cheat_Trainer
{
    internal class GeomEditor
    {
        /// <summary>
        /// Represents a single guard's data from the ROUTES chunk (32 bytes).
        /// </summary>
        private class GuardHeader
        {
            public uint RouteID;
            public uint PointCount;
            public uint DataOffset;
        }

        /// <summary>
        /// Represents one route point (48 bytes), per your format:
        /// 
        /// 0x00 int flag (unused)
        /// 0x04 int group_id (unused)
        /// 0x08 float X
        /// 0x0C float Y
        /// 0x10 float Z
        /// 0x14 float AX
        /// 0x18 float AY
        /// 0x1C float AZ
        /// 0x20 short Action
        /// 0x22 short Move
        /// 0x24 short Time
        /// 0x26 short Dir
        /// 0x28 int reserved[2] (8 bytes padding)
        /// total = 48 bytes
        /// </summary>
        private class RoutePoint
        {
            public long FileOffset;

            // Main (X, Y, Z)
            public float X;
            public float Y;
            public float Z;

            // Additional (AX, AY, AZ)
            public float AX;
            public float AY;
            public float AZ;

            // Shorts
            public short Action;
            public short Move;
            public short Time;
            public short Dir;
        }

        /// <summary>
        /// Reads the .geom file, loads all guard routes into memory,
        /// and shows a multi-guard editing form.
        /// </summary>
        public static void EditGeomFile(string geomFilePath)
        {
            List<GuardHeader> guardHeaders = new List<GuardHeader>();

            using (FileStream fs = File.OpenRead(geomFilePath))
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] rawHeader = br.ReadBytes(112);
                if (rawHeader.Length < 112)
                {
                    MessageBox.Show("File is too small to be a valid .geom.", "Error");
                    return;
                }

                uint chunkCount = BitConverter.ToUInt32(rawHeader, 0x08);
                if (chunkCount > 8)
                {
                    MessageBox.Show("Invalid chunk count in .geom header.", "Error");
                    return;
                }

                int chunkEntryStart = 0x20;
                int chunkEntrySize = 16;
                uint routesOffset = 0;

                for (int i = 0; i < chunkCount; i++)
                {
                    int off = chunkEntryStart + (i * chunkEntrySize);
                    if (off + 16 > rawHeader.Length)
                        break;

                    ushort chunkType = BitConverter.ToUInt16(rawHeader, off);
                    uint chunkOffsetVal = BitConverter.ToUInt32(rawHeader, off + 8);

                    if (chunkType == 0x0007)
                        routesOffset = chunkOffsetVal;
                }

                if (routesOffset == 0)
                {
                    MessageBox.Show("No ROUTES chunk found in the header.", "Error");
                    return;
                }

                // Instead of br.ReadBytes(448), read 32-byte entries in a loop
                fs.Seek(routesOffset, SeekOrigin.Begin);

                // This is the special 32-byte terminator block you mentioned:
                byte[] terminator = new byte[]
                {
            0x00, 0x00, 0x00, 0x00,  0x00, 0x00, 0x00, 0x80,
            0x00, 0x00, 0x00, 0x00,  0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,  0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,  0x00, 0x00, 0x00, 0x00
                };

                while (true)
                {
                    // If not enough bytes left for a 32-byte entry, stop
                    if (fs.Position + 32 > fs.Length)
                        break;

                    // Read next 32 bytes
                    byte[] routeEntry = br.ReadBytes(32);
                    if (routeEntry.Length < 32)
                        break;

                    // If this 32 bytes exactly matches the terminator block, stop
                    if (routeEntry.SequenceEqual(terminator))
                        break;

                    // Otherwise, parse routeID, pointCount, dataOffset
                    uint routeID = ReadUIntBigEndian(routeEntry, 0);  // big-endian
                    uint pointCount = BitConverter.ToUInt32(routeEntry, 8);
                    uint dataOffset = BitConverter.ToUInt32(routeEntry, 16);

                    // If it's a valid guard entry, store it
                    if (routeID != 0 || pointCount != 0)
                    {
                        guardHeaders.Add(new GuardHeader
                        {
                            RouteID = routeID,
                            PointCount = pointCount,
                            DataOffset = dataOffset
                        });
                    }
                    // else if routeID==0 & pointCount==0 => probably just an empty entry,
                    // but you can decide if you want to break here too.
                }
            } // end using

            if (guardHeaders.Count == 0)
            {
                MessageBox.Show("No guards found in the ROUTES chunk.", "Info");
                return;
            }

            // Now load route points, show the form, etc.
            Dictionary<uint, List<RoutePoint>> allGuardPoints = new Dictionary<uint, List<RoutePoint>>();
            foreach (var guard in guardHeaders)
            {
                List<RoutePoint> points = LoadRoutePoints(geomFilePath, guard);
                allGuardPoints[guard.RouteID] = points;
            }

            bool userSavedAll = ShowMultiGuardEditorForm(geomFilePath, guardHeaders, allGuardPoints);

            if (userSavedAll)
            {
                MessageBox.Show("Successfully updated all route points for all guards!", "Success");
            }
        }


        /// <summary>
        /// Reads all X, Y, Z, AX, AY, AZ, Action, Move, Time, Dir for the given guard.
        /// </summary>
        private static List<RoutePoint> LoadRoutePoints(string geomFilePath, GuardHeader guard)
        {
            List<RoutePoint> points = new List<RoutePoint>();
            if (guard.PointCount == 0)
                return points;

            using (FileStream fs = File.OpenRead(geomFilePath))
            using (BinaryReader br = new BinaryReader(fs))
            {
                fs.Seek(guard.DataOffset, SeekOrigin.Begin);

                for (int i = 0; i < guard.PointCount; i++)
                {
                    long fileOffset = fs.Position;
                    // Each route point is 48 bytes
                    byte[] routePointBytes = br.ReadBytes(48);
                    if (routePointBytes.Length < 48)
                        break;

                    // According to your notes:
                    // 0x8  float X
                    // 0xC  float Y
                    // 0x10 float Z
                    // 0x14 float AX
                    // 0x18 float AY
                    // 0x1C float AZ
                    // 0x20 short Action
                    // 0x22 short Move
                    // 0x24 short Time
                    // 0x26 short Dir
                    float x = BitConverter.ToSingle(routePointBytes, 0x08);
                    float y = BitConverter.ToSingle(routePointBytes, 0x0C);
                    float z = BitConverter.ToSingle(routePointBytes, 0x10);
                    float ax = BitConverter.ToSingle(routePointBytes, 0x14);
                    float ay = BitConverter.ToSingle(routePointBytes, 0x18);
                    float az = BitConverter.ToSingle(routePointBytes, 0x1C);

                    short actionVal = BitConverter.ToInt16(routePointBytes, 0x20);
                    short moveVal = BitConverter.ToInt16(routePointBytes, 0x22);
                    short timeVal = BitConverter.ToInt16(routePointBytes, 0x24);
                    short dirVal = BitConverter.ToInt16(routePointBytes, 0x26);

                    points.Add(new RoutePoint
                    {
                        FileOffset = fileOffset,
                        X = x,
                        Y = y,
                        Z = z,
                        AX = ax,
                        AY = ay,
                        AZ = az,

                        Action = actionVal,
                        Move = moveVal,
                        Time = timeVal,
                        Dir = dirVal
                    });
                }
            }

            return points;
        }

        /// <summary>
        /// Displays a form for editing all the route points across multiple guards.
        /// </summary>
        private static bool ShowMultiGuardEditorForm(
            string geomFilePath,
            List<GuardHeader> guardHeaders,
            Dictionary<uint, List<RoutePoint>> allGuardPoints)
        {
            bool userSavedAll = false;

            Form form = new Form
            {
                Text = "Editing .geom - Multiple Guards",
                Width = 1100,
                Height = 800,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                AutoScroll = true
            };

            int currentGuardIndex = 0;

            // A panel to hold the table of route points
            Panel routePointsPanel = new Panel
            {
                Dock = DockStyle.Top,
                AutoScroll = true,
                Height = 650
            };
            form.Controls.Add(routePointsPanel);

            // A label at the top to show current guard info
            Label lblGuardTitle = new Label
            {
                Text = "",
                Dock = DockStyle.Top,
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            form.Controls.Add(lblGuardTitle);
            form.Controls.SetChildIndex(lblGuardTitle, 0);

            // Bottom panel (FlowLayout, RightToLeft)
            FlowLayoutPanel bottomPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10)
            };

            // Cancel
            Button btnCancel = new Button { Text = "Cancel", Width = 100 };
            btnCancel.Click += (s, e) => { form.Close(); };
            bottomPanel.Controls.Add(btnCancel);

            // Save All
            Button btnSaveAll = new Button { Text = "Save All", Width = 100 };
            btnSaveAll.Click += (s, e) =>
            {
                // Apply changes for the currently displayed guard
                if (!ApplyChangesFromUI(routePointsPanel, guardHeaders[currentGuardIndex].RouteID, allGuardPoints))
                    return;

                // Then save all updated route points for all guards
                using (FileStream fs = File.OpenWrite(geomFilePath))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    foreach (var gh in guardHeaders)
                    {
                        var rpList = allGuardPoints[gh.RouteID];
                        SaveUpdatedRoutePointsInternal(rpList, fs, bw);
                    }
                }

                userSavedAll = true;
                form.Close();
            };
            bottomPanel.Controls.Add(btnSaveAll);

            // ----------------------------------------------------
            // Button: Set All (X,Y,Z) to Snake's XYZ
            // ----------------------------------------------------
            Button btnSetAllXYZ = new Button { Text = "Set All X/Y/Z", Width = 120 };
            btnSetAllXYZ.Click += (s, e) =>
            {
                float[] snakePos = GetSnakePosition();
                if (snakePos == null)
                {
                    MessageBox.Show("Failed to get Snake's position.\nMGS3 may not be running.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Overwrite X/Y/Z of EVERY guard's route points
                foreach (var gh in guardHeaders)
                {
                    if (!allGuardPoints.ContainsKey(gh.RouteID)) continue;
                    foreach (var rp in allGuardPoints[gh.RouteID])
                    {
                        rp.X = snakePos[0];
                        rp.Y = snakePos[1];
                        rp.Z = snakePos[2];
                    }
                }

                // Rebuild the table for the currently displayed guard
                RebuildRoutePointsTable(routePointsPanel, lblGuardTitle,
                    guardHeaders, currentGuardIndex, allGuardPoints);
            };
            bottomPanel.Controls.Add(btnSetAllXYZ);

            // ----------------------------------------------------
            // Button: Set All (AX,AY,AZ) to Snake's XYZ
            // ----------------------------------------------------
            Button btnSetAllAXYZ = new Button { Text = "Set All A(X/Y/Z)", Width = 120 };
            btnSetAllAXYZ.Click += (s, e) =>
            {
                float[] snakePos = GetSnakePosition();
                if (snakePos == null)
                {
                    MessageBox.Show("Failed to get Snake's position.\nMGS3 may not be running.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Overwrite AX/AY/AZ for EVERY guard's route points
                foreach (var gh in guardHeaders)
                {
                    if (!allGuardPoints.ContainsKey(gh.RouteID)) continue;
                    foreach (var rp in allGuardPoints[gh.RouteID])
                    {
                        rp.AX = snakePos[0];
                        rp.AY = snakePos[1];
                        rp.AZ = snakePos[2];
                    }
                }

                // Rebuild the table for the currently displayed guard
                RebuildRoutePointsTable(routePointsPanel, lblGuardTitle,
                    guardHeaders, currentGuardIndex, allGuardPoints);
            };
            bottomPanel.Controls.Add(btnSetAllAXYZ);

            // Next >
            Button btnNext = new Button { Text = "Next >", Width = 80 };
            btnNext.Click += (s, e) =>
            {
                if (currentGuardIndex < guardHeaders.Count - 1)
                {
                    if (!ApplyChangesFromUI(routePointsPanel, guardHeaders[currentGuardIndex].RouteID, allGuardPoints))
                        return;

                    currentGuardIndex++;
                    RebuildRoutePointsTable(routePointsPanel, lblGuardTitle,
                        guardHeaders, currentGuardIndex, allGuardPoints);
                }
            };
            bottomPanel.Controls.Add(btnNext);

            // < Prev
            Button btnPrev = new Button { Text = "< Prev", Width = 80 };
            btnPrev.Click += (s, e) =>
            {
                if (currentGuardIndex > 0)
                {
                    if (!ApplyChangesFromUI(routePointsPanel, guardHeaders[currentGuardIndex].RouteID, allGuardPoints))
                        return;

                    currentGuardIndex--;
                    RebuildRoutePointsTable(routePointsPanel, lblGuardTitle,
                        guardHeaders, currentGuardIndex, allGuardPoints);
                }
            };
            bottomPanel.Controls.Add(btnPrev);

            // Add bottom panel and build initial table
            form.Controls.Add(bottomPanel);
            RebuildRoutePointsTable(routePointsPanel, lblGuardTitle,
                guardHeaders, currentGuardIndex, allGuardPoints);

            form.ShowDialog();
            return userSavedAll;
        }

        /// <summary>
        /// Clears the panel and re-creates the route points table for the *current* guard.
        /// Now includes AX, AY, AZ in the UI as well.
        /// </summary>
        private static void RebuildRoutePointsTable(
            Panel containerPanel,
            Label lblGuardTitle,
            List<GuardHeader> guardHeaders,
            int guardIndex,
            Dictionary<uint, List<RoutePoint>> allGuardPoints)
        {
            containerPanel.Controls.Clear();

            GuardHeader gh = guardHeaders[guardIndex];
            List<RoutePoint> routePoints = allGuardPoints[gh.RouteID];

            lblGuardTitle.Text = $"Guard {guardIndex + 1} of {guardHeaders.Count}  |  "
                               + $"RouteID=0x{gh.RouteID:X8}  |  Points={gh.PointCount}";

            // We want columns:
            //  0: Index
            //  1: X
            //  2: Y
            //  3: Z
            //  4: AX
            //  5: AY
            //  6: AZ
            //  7: [Button: Set to Snake's X/Y/Z]
            //  8: Action
            //  9: Move
            // 10: Time
            // 11: Dir
            int colCount = 12;

            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = colCount,
                RowCount = routePoints.Count + 1,
                Dock = DockStyle.Top,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            // Define column widths
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));  // Index
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));  // X
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));  // Y
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));  // Z
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));  // AX
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));  // AY
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));  // AZ
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));  // [Set to Snake X,Y,Z]
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));  // Action
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));  // Move
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));  // Time
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));  // Dir

            // Header row
            table.Controls.Add(HeaderLabel("Idx"), 0, 0);
            table.Controls.Add(HeaderLabel("X"), 1, 0);
            table.Controls.Add(HeaderLabel("Y"), 2, 0);
            table.Controls.Add(HeaderLabel("Z"), 3, 0);
            table.Controls.Add(HeaderLabel("AX"), 4, 0);
            table.Controls.Add(HeaderLabel("AY"), 5, 0);
            table.Controls.Add(HeaderLabel("AZ"), 6, 0);
            table.Controls.Add(HeaderLabel("Set to\nSnake XYZ"), 7, 0);
            table.Controls.Add(HeaderLabel("Action"), 8, 0);
            table.Controls.Add(HeaderLabel("Move"), 9, 0);
            table.Controls.Add(HeaderLabel("Time"), 10, 0);
            table.Controls.Add(HeaderLabel("Dir"), 11, 0);

            // Fill data rows
            for (int i = 0; i < routePoints.Count; i++)
            {
                RoutePoint rp = routePoints[i];
                int rowIdx = i + 1;

                // Column 0: Index
                table.Controls.Add(CreateCenterLabel(i.ToString()), 0, rowIdx);

                // Column 1: X
                TextBox txtX = new TextBox
                {
                    Text = $"{rp.X:F6}",
                    Width = 60,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Name = "txtX_" + i
                };
                table.Controls.Add(txtX, 1, rowIdx);

                // Column 2: Y
                TextBox txtY = new TextBox
                {
                    Text = $"{rp.Y:F6}",
                    Width = 60,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Name = "txtY_" + i
                };
                table.Controls.Add(txtY, 2, rowIdx);

                // Column 3: Z
                TextBox txtZ = new TextBox
                {
                    Text = $"{rp.Z:F6}",
                    Width = 60,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Name = "txtZ_" + i
                };
                table.Controls.Add(txtZ, 3, rowIdx);

                // Column 4: AX
                TextBox txtAX = new TextBox
                {
                    Text = $"{rp.AX:F6}",
                    Width = 60,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Name = "txtAX_" + i
                };
                table.Controls.Add(txtAX, 4, rowIdx);

                // Column 5: AY
                TextBox txtAY = new TextBox
                {
                    Text = $"{rp.AY:F6}",
                    Width = 60,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Name = "txtAY_" + i
                };
                table.Controls.Add(txtAY, 5, rowIdx);

                // Column 6: AZ
                TextBox txtAZ = new TextBox
                {
                    Text = $"{rp.AZ:F6}",
                    Width = 60,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Name = "txtAZ_" + i
                };
                table.Controls.Add(txtAZ, 6, rowIdx);

                // Column 7: [Button] "Snake's X/Y/Z" (only sets the main X/Y/Z fields)
                Button btnSetToSnake = new Button
                {
                    Text = "Snake XYZ",
                    Width = 70,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right
                };
                btnSetToSnake.Click += (s, e) =>
                {
                    float[] snakePos = GetSnakePosition();
                    if (snakePos != null)
                    {
                        txtX.Text = $"{snakePos[0]:F6}";
                        txtY.Text = $"{snakePos[1]:F6}";
                        txtZ.Text = $"{snakePos[2]:F6}";
                    }
                    else
                    {
                        MessageBox.Show("Failed to get Snake's position.\nMGS3 may not be running.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                table.Controls.Add(btnSetToSnake, 7, rowIdx);

                // Column 8: Action
                byte[] actionBytes = BitConverter.GetBytes(rp.Action);
                TextBox txtAction = new TextBox
                {
                    Text = $"{actionBytes[0]:X2} {actionBytes[1]:X2}",
                    Width = 60,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Name = "txtAction_" + i
                };
                txtAction.MouseDoubleClick += (s, e) => txtAction.SelectAll();
                table.Controls.Add(txtAction, 8, rowIdx);

                // Column 9: Move
                byte[] moveBytes = BitConverter.GetBytes(rp.Move);
                TextBox txtMove = new TextBox
                {
                    Text = $"{moveBytes[0]:X2} {moveBytes[1]:X2}",
                    Width = 60,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Name = "txtMove_" + i
                };
                txtMove.MouseDoubleClick += (s, e) => txtMove.SelectAll();
                table.Controls.Add(txtMove, 9, rowIdx);

                // Column 10: Time
                byte[] timeBytes = BitConverter.GetBytes(rp.Time);
                TextBox txtTime = new TextBox
                {
                    Text = $"{timeBytes[0]:X2} {timeBytes[1]:X2}",
                    Width = 60,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Name = "txtTime_" + i
                };
                txtTime.MouseDoubleClick += (s, e) => txtTime.SelectAll();
                table.Controls.Add(txtTime, 10, rowIdx);

                // Column 11: Dir
                byte[] dirBytes = BitConverter.GetBytes(rp.Dir);
                TextBox txtDir = new TextBox
                {
                    Text = $"{dirBytes[0]:X2} {dirBytes[1]:X2}",
                    Width = 60,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Name = "txtDir_" + i
                };
                txtDir.MouseDoubleClick += (s, e) => txtDir.SelectAll();
                table.Controls.Add(txtDir, 11, rowIdx);
            }

            containerPanel.Controls.Add(table);
        }

        /// <summary>
        /// Reads user input from the table into allGuardPoints[routeID].
        /// Returns false if there's a parse error.
        /// </summary>
        private static bool ApplyChangesFromUI(
            Panel containerPanel,
            uint routeID,
            Dictionary<uint, List<RoutePoint>> allGuardPoints)
        {
            var table = containerPanel.Controls.OfType<TableLayoutPanel>().FirstOrDefault();
            if (table == null)
                return true;

            List<RoutePoint> routePoints = allGuardPoints[routeID];

            for (int i = 0; i < routePoints.Count; i++)
            {
                // X, Y, Z, AX, AY, AZ
                TextBox txtX = table.Controls.Find($"txtX_{i}", true).FirstOrDefault() as TextBox;
                TextBox txtY = table.Controls.Find($"txtY_{i}", true).FirstOrDefault() as TextBox;
                TextBox txtZ = table.Controls.Find($"txtZ_{i}", true).FirstOrDefault() as TextBox;
                TextBox txtAX = table.Controls.Find($"txtAX_{i}", true).FirstOrDefault() as TextBox;
                TextBox txtAY = table.Controls.Find($"txtAY_{i}", true).FirstOrDefault() as TextBox;
                TextBox txtAZ = table.Controls.Find($"txtAZ_{i}", true).FirstOrDefault() as TextBox;

                // Action, Move, Time, Dir
                TextBox txtAction = table.Controls.Find($"txtAction_{i}", true).FirstOrDefault() as TextBox;
                TextBox txtMove = table.Controls.Find($"txtMove_{i}", true).FirstOrDefault() as TextBox;
                TextBox txtTime = table.Controls.Find($"txtTime_{i}", true).FirstOrDefault() as TextBox;
                TextBox txtDir = table.Controls.Find($"txtDir_{i}", true).FirstOrDefault() as TextBox;

                if (txtX == null || txtY == null || txtZ == null ||
                    txtAX == null || txtAY == null || txtAZ == null ||
                    txtAction == null || txtMove == null || txtTime == null || txtDir == null)
                {
                    continue;
                }

                // parse main coords
                if (!float.TryParse(txtX.Text, out float newX) ||
                    !float.TryParse(txtY.Text, out float newY) ||
                    !float.TryParse(txtZ.Text, out float newZ) ||
                    !float.TryParse(txtAX.Text, out float newAX) ||
                    !float.TryParse(txtAY.Text, out float newAY) ||
                    !float.TryParse(txtAZ.Text, out float newAZ))
                {
                    MessageBox.Show($"Invalid float at row {i}.", "Error");
                    return false;
                }

                // parse short hex
                if (!TryParseShortHex(txtAction.Text, out short act))
                {
                    MessageBox.Show($"Invalid Action hex at row {i}.", "Error");
                    return false;
                }
                if (!TryParseShortHex(txtMove.Text, out short mov))
                {
                    MessageBox.Show($"Invalid Move hex at row {i}.", "Error");
                    return false;
                }
                if (!TryParseShortHex(txtTime.Text, out short tm))
                {
                    MessageBox.Show($"Invalid Time hex at row {i}.", "Error");
                    return false;
                }
                if (!TryParseShortHex(txtDir.Text, out short dr))
                {
                    MessageBox.Show($"Invalid Dir hex at row {i}.", "Error");
                    return false;
                }

                // store them
                routePoints[i].X = newX;
                routePoints[i].Y = newY;
                routePoints[i].Z = newZ;
                routePoints[i].AX = newAX;
                routePoints[i].AY = newAY;
                routePoints[i].AZ = newAZ;
                routePoints[i].Action = act;
                routePoints[i].Move = mov;
                routePoints[i].Time = tm;
                routePoints[i].Dir = dr;
            }

            return true;
        }

        /// <summary>
        /// Writes updated X, Y, Z, AX, AY, AZ, Action, Move, Time, Dir for each route point
        /// back into the .geom file, respecting your 48-byte format.
        /// </summary>
        private static void SaveUpdatedRoutePointsInternal(
            List<RoutePoint> routePoints,
            FileStream fs,
            BinaryWriter bw)
        {
            foreach (var rp in routePoints)
            {
                // Jump to start of this route's 48-byte chunk
                fs.Seek(rp.FileOffset, SeekOrigin.Begin);

                // The first 8 bytes: int flag, int group_id (unused in this code)
                bw.BaseStream.Seek(8, SeekOrigin.Current);

                // Next 12 bytes: X, Y, Z
                bw.Write(rp.X);
                bw.Write(rp.Y);
                bw.Write(rp.Z);

                // Next 12 bytes: AX, AY, AZ
                bw.Write(rp.AX);
                bw.Write(rp.AY);
                bw.Write(rp.AZ);

                // Next 8 bytes: Action (2), Move (2), Time (2), Dir (2)
                bw.Write(BitConverter.GetBytes(rp.Action));
                bw.Write(BitConverter.GetBytes(rp.Move));
                bw.Write(BitConverter.GetBytes(rp.Time));
                bw.Write(BitConverter.GetBytes(rp.Dir));

                // Last 8 bytes (offset 0x28..0x2F) are reserved/padding:
                // We'll just leave them as-is, so skip them
                bw.BaseStream.Seek(8, SeekOrigin.Current);
            }
        }

        /// <summary>
        /// Helper: parse "AB CD" as two hex bytes => short
        /// </summary>
        private static bool TryParseShortHex(string hexString, out short value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(hexString))
                return false;

            string[] parts = hexString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                return false;

            if (!byte.TryParse(parts[0], NumberStyles.HexNumber, null, out byte b0) ||
                !byte.TryParse(parts[1], NumberStyles.HexNumber, null, out byte b1))
            {
                return false;
            }

            value = BitConverter.ToInt16(new byte[] { b0, b1 }, 0);
            return true;
        }

        /// <summary>
        /// Creates a bold header label.
        /// </summary>
        private static Label HeaderLabel(string text)
        {
            return new Label
            {
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Fill
            };
        }

        /// <summary>
        /// Creates a centered label, optionally with a color.
        /// </summary>
        private static Label CreateCenterLabel(string text, Color? foreColor = null)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = foreColor ?? Color.Black
            };
        }

        /// <summary>
        /// Reads a big-endian uint from the given offset in data.
        /// </summary>
        private static uint ReadUIntBigEndian(byte[] data, int offset)
        {
            byte[] temp = new byte[4];
            Array.Copy(data, offset, temp, 0, 4);
            Array.Reverse(temp);
            return BitConverter.ToUInt32(temp, 0);
        }

        /// <summary>
        /// Reads Snake's position from memory (your existing code).
        /// </summary>
        private static float[] GetSnakePosition()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle != IntPtr.Zero)
            {
                try
                {
                    return XyzManager.Instance.ReadSnakePosition(processHandle);
                }
                finally
                {
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
            }
            return null;
        }
    }
}
