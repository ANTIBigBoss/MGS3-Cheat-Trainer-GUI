using System.Text;
using System.Globalization;

public class GeomParser
{
    /// <summary>
    /// A small container for our "header info" rows (matching your Python's tuples).
    /// </summary>
    private class HeaderField
    {
        public long Offset { get; set; }
        public int Size { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }

        public HeaderField(long offset, int size, string value, string description, string notes)
        {
            Offset = offset;
            Size = size;
            Value = value;
            Description = description;
            Notes = notes;
        }
    }

    /// <summary>
    /// Chunk entry container for the "CHUNK TABLE".
    /// </summary>
    private class ChunkEntry
    {
        public int Index { get; set; }
        public int Offset { get; set; }
        public ushort Type { get; set; }
        public uint Size { get; set; }
        public uint DataOffset { get; set; }
    }

    /// <summary>
    /// Guard header container (each is 32 bytes in the ROUTES chunk).
    /// </summary>
    private class GuardHeader
    {
        public uint RouteID;      // >I (big-endian)
        public uint PointCount;   // <I (little-endian)
        public uint DataOffset;   // <I (little-endian)
    }

    public static void ParseGeomHeader(string filePath)
    {
        string directory = Path.GetDirectoryName(filePath) ?? "";
        string fileNameNoExt = Path.GetFileNameWithoutExtension(filePath) ?? "geom_file";
        string outputFile = Path.Combine(directory, fileNameNoExt + "_Guard_Routes.txt");

        try
        {
            // Read the fixed 112-byte header
            byte[] rawHeader;
            using (FileStream fs = File.OpenRead(filePath))
            {
                rawHeader = new byte[112];
                int bytesRead = fs.Read(rawHeader, 0, 112);
                if (bytesRead < 112)
                {
                    throw new Exception($"Header is only {bytesRead} bytes, expected 112.");
                }
            }

            using (var writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                WriteRawHeader(rawHeader, writer);

                List<HeaderField> headerInfo = new List<HeaderField>();
                List<ChunkEntry> chunkEntries = new List<ChunkEntry>();

                ParseHeader(rawHeader, headerInfo, chunkEntries, writer);

                ChunkEntry routesChunk = chunkEntries.Find(c => c.Type == 0x0007);
                if (routesChunk == null)
                {
                    writer.WriteLine("\n❌ ROUTES chunk not found in the header. No guard data will be shown.");
                    return;
                }

                ParseRoutesChunk(filePath, routesChunk, writer);
            }

            Console.WriteLine($"Finished parsing. Output written to:\n  {outputFile}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"❌ Error: File not found at '{filePath}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Unexpected error: {ex}");
        }
    }

    /// <summary>
    /// Writes the raw header bytes in lines of 16 bytes each.
    /// </summary>
    private static void WriteRawHeader(byte[] rawHeader, StreamWriter writer)
    {
        writer.WriteLine("### RAW HEADER ###");
        writer.WriteLine("raw_header");

        for (int i = 0; i < rawHeader.Length; i += 16)
        {
            int lineSize = Math.Min(16, rawHeader.Length - i);
            StringBuilder sb = new StringBuilder(lineSize * 3);
            for (int j = 0; j < lineSize; j++)
            {
                sb.AppendFormat("{0:X2} ", rawHeader[i + j]);
            }
            writer.WriteLine(sb.ToString().TrimEnd());
        }
    }

    /// <summary>
    /// Parses the 112-byte main header to fill in:
    /// 1) headerInfo (similar to your Python structure),
    /// 2) chunkEntries (for the chunk table).
    /// Also writes the "FULL HEADER INFORMATION" and "CHUNK TABLE" to the text file.
    /// </summary>
    private static void ParseHeader(
        byte[] rawHeader,
        List<HeaderField> headerInfo,
        List<ChunkEntry> chunkEntries,
        StreamWriter writer)
    {
        float ReadFloatBE(int offset)
        {
            byte[] temp = new byte[4];
            Array.Copy(rawHeader, offset, temp, 0, 4);
            Array.Reverse(temp);
            return BitConverter.ToSingle(temp, 0);
        }

        uint ReadUIntLE(int offset)
        {
            return BitConverter.ToUInt32(rawHeader, offset);
        }

        headerInfo.Add(new HeaderField(0x00, 4,
            rawHeader.AsHexString(0, 4),
            "Signature",
            "Identifies this as an MGS3 .geom file"));

        headerInfo.Add(new HeaderField(0x04, 4,
            rawHeader.AsHexString(4, 4),
            "File Version?",
            "Potential format/version marker"));

        uint chunkCount = ReadUIntLE(0x08);
        headerInfo.Add(new HeaderField(0x08, 4,
            chunkCount.ToString(),
            "Chunk Count",
            "Number of chunks detected"));

        uint fileSize = ReadUIntLE(0x0C);
        headerInfo.Add(new HeaderField(0x0C, 4,
            fileSize.ToString(),
            "File Size",
            "Appears unused here"));

        float baseX = ReadFloatBE(0x10);
        float baseY = ReadFloatBE(0x14);
        float baseZ = ReadFloatBE(0x18);
        float baseW = ReadFloatBE(0x1C);

        // Chunk Entry 0 - GROUPS 0x0
        headerInfo.Add(new HeaderField(0x10, 4,
            baseX.ToString("G17", CultureInfo.InvariantCulture),
            "Base X Offset",
            "Spatial offset (float, Big-Endian)"));

        headerInfo.Add(new HeaderField(0x14, 4,
            baseY.ToString("G17", CultureInfo.InvariantCulture),
            "Base Y Offset",
            "Spatial offset (float, Big-Endian)"));

        headerInfo.Add(new HeaderField(0x18, 4,
            baseZ.ToString("G17", CultureInfo.InvariantCulture),
            "Base Z Offset",
            "Spatial offset (float, Big-Endian)"));

        headerInfo.Add(new HeaderField(0x1C, 4,
            baseW.ToString("G17", CultureInfo.InvariantCulture),
            "Base W Offset",
            "Possibly a scale factor (float, Big-Endian)"));

        // REFS Chunk - 0x01
        headerInfo.Add(new HeaderField(0x20, 4,
            rawHeader.AsHexString(0x20, 4),
            "Type",
            "Chunk Type: 0x01 (REFS)"));
        headerInfo.Add(new HeaderField(0x24, 4,
            rawHeader.AsHexString(0x24, 4),
            "Size",
            ""));
        headerInfo.Add(new HeaderField(0x28, 4,
            rawHeader.AsHexString(0x28, 4),
            "Offset",
            ""));
        headerInfo.Add(new HeaderField(0x2C, 4,
            rawHeader.AsHexString(0x2C, 4),
            "Padding/Unused",
            "Reserved, usually zero"));

        // UNKNOWN Chunk - 0x05
        headerInfo.Add(new HeaderField(0x30, 4,
            rawHeader.AsHexString(0x30, 4),
            "Type",
            "Chunk Type: 0x05 (UNKNOWN)"));
        headerInfo.Add(new HeaderField(0x34, 4,
            rawHeader.AsHexString(0x34, 4),
            "Size",
            ""));
        headerInfo.Add(new HeaderField(0x38, 4,
            rawHeader.AsHexString(0x38, 4),
            "Offset",
            ""));
        headerInfo.Add(new HeaderField(0x3C, 4,
            rawHeader.AsHexString(0x3C, 4),
            "Padding/Unused",
            "Reserved, usually zero"));

        // PROPS Chunk - 0x06
        headerInfo.Add(new HeaderField(0x40, 4,
            rawHeader.AsHexString(0x40, 4),
            "Type",
            "Chunk Type: 0x06 (PROPS)"));
        headerInfo.Add(new HeaderField(0x44, 4,
            rawHeader.AsHexString(0x44, 4),
            "Size",
            ""));
        headerInfo.Add(new HeaderField(0x48, 4,
            rawHeader.AsHexString(0x48, 4),
            "Offset",
            ""));
        headerInfo.Add(new HeaderField(0x4C, 4,
            rawHeader.AsHexString(0x4C, 4),
            "Padding/Unused",
            "Reserved, usually zero"));

        // ROUTES Chunk - 0x07
        headerInfo.Add(new HeaderField(0x50, 4,
            rawHeader.AsHexString(0x50, 4),
            "Type",
            "Chunk Type: 0x07 (ROUTES)"));
        headerInfo.Add(new HeaderField(0x54, 4,
            rawHeader.AsHexString(0x54, 4),
            "Size",
            ""));
        headerInfo.Add(new HeaderField(0x58, 4,
            rawHeader.AsHexString(0x58, 4),
            "Offset",
            ""));
        headerInfo.Add(new HeaderField(0x5C, 4,
            rawHeader.AsHexString(0x5C, 4),
            "Padding/Unused",
            "Reserved, usually zero"));

        // TERMINATOR Chunk - 0x08
        headerInfo.Add(new HeaderField(0x60, 4,
            rawHeader.AsHexString(0x60, 4),
            "Type",
            "Chunk Type: 0x08 (TERMINATOR)"));
        headerInfo.Add(new HeaderField(0x64, 4,
            rawHeader.AsHexString(0x64, 4),
            "Size",
            ""));
        headerInfo.Add(new HeaderField(0x68, 4,
            rawHeader.AsHexString(0x68, 4),
            "Offset",
            ""));
        headerInfo.Add(new HeaderField(0x6C, 4,
            rawHeader.AsHexString(0x6C, 4),
            "Padding/Unused",
            "Reserved, usually zero"));

        if (chunkCount > 8)
        {
            throw new Exception($"Invalid chunk count: {chunkCount}");
        }

        int chunkEntryStart = 0x20;
        int chunkEntrySize = 16;
        for (int i = 0; i < chunkCount; i++)
        {
            int off = chunkEntryStart + (i * chunkEntrySize);
            if (off + 16 > rawHeader.Length)
            {
                writer.WriteLine($"Warning: Incomplete chunk entry at offset 0x{off:X}.");
                break;
            }

            ushort ctype = BitConverter.ToUInt16(rawHeader, off);
            uint csize = BitConverter.ToUInt32(rawHeader, off + 4);
            uint coffset = BitConverter.ToUInt32(rawHeader, off + 8);

            chunkEntries.Add(new ChunkEntry
            {
                Index = i,
                Offset = off,
                Type = ctype,
                Size = csize,
                DataOffset = coffset
            });

            headerInfo.Add(new HeaderField(off, 4,
                rawHeader.AsHexString(off, 4),
                $"Chunk Entry {i} Type",
                $"Chunk Type: 0x{ctype:X4}"));

            headerInfo.Add(new HeaderField(off + 4, 4,
                rawHeader.AsHexString(off + 4, 4),
                $"Chunk Entry {i} Size",
                $"Chunk Size: 0x{csize:X8}"));

            headerInfo.Add(new HeaderField(off + 8, 4,
                rawHeader.AsHexString(off + 8, 4),
                $"Chunk Entry {i} Offset",
                $"Data Offset: 0x{coffset:X8}"));

            headerInfo.Add(new HeaderField(off + 12, 4,
                rawHeader.AsHexString(off + 12, 4),
                $"Chunk Entry {i} Padding",
                "Reserved, usually zero"));
        }

        writer.WriteLine();
        writer.WriteLine("### FULL HEADER INFORMATION ###");
        writer.WriteLine($"{"Offset",-15}{"Size",-15}{"Value",-40}{"Description",-25}{"Notes"}");
        foreach (var hf in headerInfo)
        {
            writer.WriteLine($"{("0x" + hf.Offset.ToString("X")),-15}" +
                             $"{hf.Size,-15}" +
                             $"{hf.Value,-40}" +
                             $"{hf.Description,-25}" +
                             hf.Notes);
        }

        writer.WriteLine();
        writer.WriteLine("### CHUNK TABLE ###");
        writer.WriteLine($"{"Entry Index",-12}{"Offset",-15}{"Type",-8}{"Size",-12}{"Offset (Big-Endian)",-20}{"Chunk Name"}");

        var chunkTypeMap = new Dictionary<ushort, string> {
            { 0x0000, "GROUPS" },
            { 0x0001, "REFS" },
            { 0x0005, "UNKNOWN" },
            { 0x0006, "PROPS" },
            { 0x0007, "ROUTES" },
            { 0x0008, "TERMINATOR" }
        };

        foreach (var ce in chunkEntries)
        {
            string chunkName = chunkTypeMap.ContainsKey(ce.Type)
                ? chunkTypeMap[ce.Type]
                : "INVALID/EMPTY";

            writer.WriteLine($"{ce.Index,-12}" +
                             $"0x{ce.Offset:X}{new string(' ', 13 - ce.Offset.ToString("X").Length)}" +
                             $"0x{ce.Type:X4}{new string(' ', 4)}" +
                             $"0x{ce.Size:X8}{new string(' ', 1)}" +
                             $"0x{ce.DataOffset:X8}{new string(' ', 8 - ce.DataOffset.ToString("X").Length)}  " +
                             $"{chunkName}");
        }
    }

    /// <summary>
    /// Finds and parses the ROUTES chunk:
    /// - Reads 448 bytes (as in your Python code).
    /// - Breaks it into 32-byte guard headers.
    /// - For each guard, parses the route points (48 bytes each).
    /// </summary>
    private static void ParseRoutesChunk(string filePath, ChunkEntry routesChunk, StreamWriter writer)
    {
        writer.WriteLine();
        writer.WriteLine("### ROUTES CHUNK DATA ###");

        byte[] routesData = new byte[448];
        using (FileStream fs = File.OpenRead(filePath))
        {
            fs.Seek(routesChunk.DataOffset, SeekOrigin.Begin);
            int bytesRead = fs.Read(routesData, 0, 448);
        }

        List<GuardHeader> guardHeaders = new List<GuardHeader>();
        for (int i = 0; i < routesData.Length; i += 32)
        {
            if (i + 32 > routesData.Length) break;

            uint route_id = ReadUIntBigEndian(routesData, i + 0);

            uint point_count = BitConverter.ToUInt32(routesData, i + 8);

            uint data_offset = BitConverter.ToUInt32(routesData, i + 16);

            guardHeaders.Add(new GuardHeader
            {
                RouteID = route_id,
                PointCount = point_count,
                DataOffset = data_offset
            });
        }

        writer.WriteLine($"{"Index",-6}{"ID",-12}{"Point Count",-15}{"Data Offset",-15}");
        for (int i = 0; i < guardHeaders.Count; i++)
        {
            var gh = guardHeaders[i];
            writer.WriteLine($"{i,-6}0x{gh.RouteID:X8}   {gh.PointCount,-15}0x{gh.DataOffset:X8}");
        }

        writer.WriteLine();
        writer.WriteLine("### PARSED ROUTE POINTS ###");

        using (FileStream fs = File.OpenRead(filePath))
        using (BinaryReader br = new BinaryReader(fs))
        {
            for (int i = 0; i < guardHeaders.Count; i++)
            {
                var gh = guardHeaders[i];
                writer.WriteLine($"\nGuard #{i} - ID: 0x{gh.RouteID:X8}, Point Count: {gh.PointCount}, Data Offset: 0x{gh.DataOffset:X8}");

                if (gh.DataOffset == 0)
                {
                    writer.WriteLine("No data offset, skipping...");
                    continue;
                }

                fs.Seek((long)gh.DataOffset, SeekOrigin.Begin);

                for (int pointIdx = 0; pointIdx < gh.PointCount; pointIdx++)
                {
                    long currentOffset = fs.Position;
                    byte[] routePoint = br.ReadBytes(48);
                    if (routePoint.Length < 48)
                    {
                        writer.WriteLine($"Incomplete geo_route_point data for Guard #{i}, Point {pointIdx}.");
                        break;
                    }

                    uint flag = BitConverter.ToUInt32(routePoint, 0);
                    uint group_id = BitConverter.ToUInt32(routePoint, 4);

                    float x = BitConverter.ToSingle(routePoint, 8);
                    float y = BitConverter.ToSingle(routePoint, 12);
                    float z = BitConverter.ToSingle(routePoint, 16);
                    float ax = BitConverter.ToSingle(routePoint, 20);
                    float ay = BitConverter.ToSingle(routePoint, 24);
                    float az = BitConverter.ToSingle(routePoint, 28);

                    short action = BitConverter.ToInt16(routePoint, 32);
                    short move = BitConverter.ToInt16(routePoint, 34);
                    short timeVal = BitConverter.ToInt16(routePoint, 36);
                    short dirVal = BitConverter.ToInt16(routePoint, 38);

                    uint reservedA = BitConverter.ToUInt32(routePoint, 40);
                    uint reservedB = BitConverter.ToUInt32(routePoint, 44);

                    writer.WriteLine($"\n  Offset: 0x{currentOffset:X8}");
                    writer.WriteLine($"    Flag: {flag:X2} {flag:X2}");
                    writer.WriteLine($"    Group ID: {group_id:X2} {group_id:X2}");
                    writer.WriteLine($"    Position (Floats): X={x:F6}, Y={y:F6}, Z={z:F6}");
                    writer.WriteLine($"    Position (Hex): {routePoint.AsHexString(8, 12).ToUpper()}");
                    writer.WriteLine($"    Axes (Floats): AX={ax:F6}, AY={ay:F6}, AZ={az:F6}");
                    writer.WriteLine($"    Axes (Hex): {routePoint.AsHexString(20, 12).ToUpper()}");
                    writer.WriteLine($"    Action: {routePoint.AsHexString(32, 2).ToUpper()} ");
                    writer.WriteLine($"    Move: {routePoint.AsHexString(34, 2).ToUpper()} ");
                    writer.WriteLine($"    Time: {routePoint.AsHexString(36, 2).ToUpper()} ({timeVal:X4}, Signed: {timeVal})");
                    writer.WriteLine($"    Direction: {routePoint.AsHexString(38, 2).ToUpper()} ({dirVal:X4}, Signed: {dirVal})");
                    writer.WriteLine($"    Reserved: {routePoint.AsHexString(40, 8).ToUpper()}");
                }
            }
        }
    }

    /// <summary>
    /// Helper for reading a big-endian uint (like Python's struct.unpack('>I'))
    /// </summary>
    private static uint ReadUIntBigEndian(byte[] buffer, int offset)
    {
        byte[] temp = new byte[4];
        Array.Copy(buffer, offset, temp, 0, 4);
        Array.Reverse(temp);
        return BitConverter.ToUInt32(temp, 0);
    }
}

public static class ByteExtensions
{
    /// <summary>
    /// Returns a hex string of the entire byte array, e.g. "00 01 FF ...", trimmed at end.
    /// </summary>
    public static string AsHexString(this byte[] data)
    {
        if (data == null) return "";
        var sb = new StringBuilder(data.Length * 3);
        for (int i = 0; i < data.Length; i++)
        {
            sb.AppendFormat("{0:X2} ", data[i]);
        }
        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Returns a hex string of the specified subrange, e.g. "00 01 FF ...", trimmed at end.
    /// </summary>
    public static string AsHexString(this byte[] data, int offset, int count)
    {
        if (data == null || offset + count > data.Length) return "";
        var sb = new StringBuilder(count * 3);
        for (int i = 0; i < count; i++)
        {
            sb.AppendFormat("{0:X2} ", data[offset + i]);
        }
        return sb.ToString().TrimEnd();
    }

}