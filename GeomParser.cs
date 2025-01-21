using System.Diagnostics;
using System.Text;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public class GeomParser
    {
        private static GeomParser instance;
        private static readonly object lockObj = new object();
        private IntPtr processHandle = IntPtr.Zero; // Keep this handle open for the process


        private GeomParser()
        {
        }

        public static GeomParser Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new GeomParser();
                    }

                    return instance;
                }
            }
        }

        
{
    /// <summary>
    /// Reads and parses an MGS3 .geom file similar to your Python script.
    /// </summary>
    public static void ParseGeomHeader(string filePath)
    {
        try
        {
            using (FileStream fs = File.OpenRead(filePath))
            using (BinaryReader br = new BinaryReader(fs))
            {
                // 1) Read the 112-byte header
                byte[] rawHeader = br.ReadBytes(112);
                if (rawHeader.Length < 112)
                {
                    LoggingManager.Instance.Log("❌ Header is shorter than 112 bytes. Exiting.");
                    return;
                }

                // --- Display Raw Header for Reference ---
                LoggingManager.Instance.Log("### RAW HEADER ###");
                // Convert to hex string
                var sbHex = new StringBuilder();
                for (int i = 0; i < rawHeader.Length; i++)
                {
                    sbHex.AppendFormat("{0:X2} ", rawHeader[i]);
                }
                string hexString = sbHex.ToString().TrimEnd();
                LoggingManager.Instance.Log(hexString);

                // Format into lines of 16 bytes each (16 * "XX " = 48 chars wide)
                for (int i = 0; i < hexString.Length; i += 48)
                {
                    int length = Math.Min(48, hexString.Length - i);
                    LoggingManager.Instance.Log(hexString.Substring(i, length));
                }

                // 2) Parse Explict Byte Ranges
                //    We'll mimic your Python structure. 
                //    In Python, you used struct.unpack('>f') for big-endian floats, '<I' for little-endian ints, etc.

                // Helper to read a big-endian float from byte array
                float ReadFloatBigEndian(byte[] array, int startIndex)
                {
                    // Reverse 4 bytes because BitConverter is little-endian
                    byte[] temp = new byte[4];
                    Array.Copy(array, startIndex, temp, 0, 4);
                    Array.Reverse(temp);
                    return BitConverter.ToSingle(temp, 0);
                }

                // Helper to read a little-endian uint
                uint ReadUIntLittleEndian(byte[] array, int startIndex)
                {
                    // BitConverter.ToUInt32 expects the system endianness
                    // On Windows/.NET, that's little-endian, so we can directly do:
                    return BitConverter.ToUInt32(array, startIndex);
                }

                // Helper to read a little-endian ushort
                ushort ReadUShortLittleEndian(byte[] array, int startIndex)
                {
                    return BitConverter.ToUInt16(array, startIndex);
                }

                // Now parse the known fields
                // 0x00: 4 bytes => Signature (hex)
                string signature = BytesToHex(rawHeader, 0x00, 4, spaced: true);
                // 0x04: 4 bytes => File Version? (hex)
                string fileVersion = BytesToHex(rawHeader, 0x04, 4, spaced: true);

                // 0x08: 4 bytes => Chunk Count (<I)
                uint chunkCount = ReadUIntLittleEndian(rawHeader, 0x08);

                // 0x0C: 4 bytes => File Size (<I)
                uint fileSize = ReadUIntLittleEndian(rawHeader, 0x0C);

                // 0x10: 4 bytes => Base X Offset (>f)
                float baseX = ReadFloatBigEndian(rawHeader, 0x10);
                float baseY = ReadFloatBigEndian(rawHeader, 0x14);
                float baseZ = ReadFloatBigEndian(rawHeader, 0x18);
                float baseW = ReadFloatBigEndian(rawHeader, 0x1C);

                // Chunk entry 0 (REFS) @ 0x20
                string chunk0Type = BytesToHex(rawHeader, 0x20, 4, spaced: true);
                string chunk0Size = BytesToHex(rawHeader, 0x24, 4, spaced: true);
                string chunk0Offset = BytesToHex(rawHeader, 0x28, 4, spaced: true);
                string chunk0Pad = BytesToHex(rawHeader, 0x2C, 4, spaced: true);

                // ... Similarly for chunk entry 1, 2, 3, 4 ...
                string chunk1Type = BytesToHex(rawHeader, 0x30, 4, spaced: true);
                string chunk1Size = BytesToHex(rawHeader, 0x34, 4, spaced: true);
                string chunk1Offset = BytesToHex(rawHeader, 0x38, 4, spaced: true);
                string chunk1Pad = BytesToHex(rawHeader, 0x3C, 4, spaced: true);

                string chunk2Type = BytesToHex(rawHeader, 0x40, 4, spaced: true);
                string chunk2Size = BytesToHex(rawHeader, 0x44, 4, spaced: true);
                string chunk2Offset = BytesToHex(rawHeader, 0x48, 4, spaced: true);
                string chunk2Pad = BytesToHex(rawHeader, 0x4C, 4, spaced: true);

                string chunk3Type = BytesToHex(rawHeader, 0x50, 4, spaced: true);
                string chunk3Size = BytesToHex(rawHeader, 0x54, 4, spaced: true);
                string chunk3Offset = BytesToHex(rawHeader, 0x58, 4, spaced: true);
                string chunk3Pad = BytesToHex(rawHeader, 0x5C, 4, spaced: true);

                string chunk4Type = BytesToHex(rawHeader, 0x60, 4, spaced: true);
                string chunk4Size = BytesToHex(rawHeader, 0x64, 4, spaced: true);
                string chunk4Offset = BytesToHex(rawHeader, 0x68, 4, spaced: true);
                string chunk4Pad = BytesToHex(rawHeader, 0x6C, 4, spaced: true);

                // Print them out if needed:
                LoggingManager.Instance.Log();
                LoggingManager.Instance.Log($"Signature: {signature}");
                LoggingManager.Instance.Log($"FileVersion: {fileVersion}");
                LoggingManager.Instance.Log($"ChunkCount: {chunkCount}");
                LoggingManager.Instance.Log($"FileSize: {fileSize}");
                LoggingManager.Instance.Log($"BaseX Offset: {baseX}");
                LoggingManager.Instance.Log($"BaseY Offset: {baseY}");
                LoggingManager.Instance.Log($"BaseZ Offset: {baseZ}");
                LoggingManager.Instance.Log($"BaseW Offset: {baseW}");

                if (chunkCount > 8)
                {
                    throw new Exception($"Invalid chunk count: {chunkCount} (too large)");
                }

                // 3) Identify the ROUTES chunk offset dynamically (like your Python code)
                //    Each chunk entry is 16 bytes (0x10). The first chunk entry starts at 0x20.
                uint routesOffset = 0;
                int chunkEntryStart = 0x20;
                int chunkEntrySize = 16;

                for (int i = 0; i < chunkCount; i++)
                {
                    int offsetCurrent = chunkEntryStart + (i * chunkEntrySize);
                    // chunk_type = <H => little-endian ushort
                    ushort ctype = ReadUShortLittleEndian(rawHeader, offsetCurrent);
                    // chunk_offset = <I => little-endian uint
                    uint coffset = ReadUIntLittleEndian(rawHeader, offsetCurrent + 8);

                    if (ctype == 0x07) // 0x07 => ROUTES
                    {
                        routesOffset = coffset;
                    }
                }

                if (routesOffset == 0)
                {
                    LoggingManager.Instance.Log("\n❌ ROUTES chunk not found in the header.");
                    return;
                }

                // 4) Parse the ROUTES Chunk
                LoggingManager.Instance.Log("\n### ROUTES CHUNK DATA ###");
                // Seek to routes offset
                fs.Seek(routesOffset, SeekOrigin.Begin);
                // We'll read 448 bytes like the python example
                byte[] routesData = br.ReadBytes(448);

                List<GuardHeader> guardHeaders = new List<GuardHeader>();
                // Each guard header is 32 bytes
                for (int i = 0; i < routesData.Length; i += 32)
                {
                    if (i + 32 > routesData.Length) break;
                    // route_id => >I => big-endian
                    uint routeID = ReadUIntBigEndian(routesData, i + 0);
                    // point_count => <I => little-endian
                    uint pointCount = BitConverter.ToUInt32(routesData, i + 8);
                    // data_offset => <I => little-endian
                    uint dataOffset = BitConverter.ToUInt32(routesData, i + 16);

                    guardHeaders.Add(new GuardHeader
                    {
                        RouteID = routeID,
                        PointCount = pointCount,
                        DataOffset = dataOffset
                    });
                }

                LoggingManager.Instance.Log($"{"Index",-6}{"ID",-12}{"Point Count",-15}{"Data Offset",-15}");
                for (int i = 0; i < guardHeaders.Count; i++)
                {
                    var gh = guardHeaders[i];
                    LoggingManager.Instance.Log($"{i,-6}{ToHex(gh.RouteID),-12}{gh.PointCount,-15}{ToHex(gh.DataOffset),-15}");
                }

                // 5) Parse `geo_route_point` structs (48 bytes each)
                LoggingManager.Instance.Log("\n### PARSED ROUTE POINTS ###");
                for (int i = 0; i < guardHeaders.Count; i++)
                {
                    var gh = guardHeaders[i];
                    LoggingManager.Instance.Log($"\nGuard #{i} - ID: {ToHex(gh.RouteID)}, Point Count: {gh.PointCount}, Data Offset: {ToHex(gh.DataOffset)}");

                    // Seek to the data offset for these route points
                    fs.Seek(gh.DataOffset, SeekOrigin.Begin);

                    for (int pointIndex = 0; pointIndex < gh.PointCount; pointIndex++)
                    {
                        long currentOffset = fs.Position;
                        byte[] routePoint = br.ReadBytes(48);
                        if (routePoint.Length < 48)
                        {
                            LoggingManager.Instance.Log($"⚠️ Incomplete geo_route_point data for Guard #{i}, Point {pointIndex}.");
                            break;
                        }

                        // Parse the struct fields
                        // flag, group_id => <II => little-endian
                        uint flag = BitConverter.ToUInt32(routePoint, 0);
                        uint groupID = BitConverter.ToUInt32(routePoint, 4);

                        // x, y, z, ax, ay, az => <f => little-endian
                        float x = BitConverter.ToSingle(routePoint, 8);
                        float y = BitConverter.ToSingle(routePoint, 12);
                        float z = BitConverter.ToSingle(routePoint, 16);
                        float ax = BitConverter.ToSingle(routePoint, 20);
                        float ay = BitConverter.ToSingle(routePoint, 24);
                        float az = BitConverter.ToSingle(routePoint, 28);

                        // action, move, time, dir => <hhhh => little-endian
                        short action = BitConverter.ToInt16(routePoint, 32);
                        short move = BitConverter.ToInt16(routePoint, 34);
                        short timeVal = BitConverter.ToInt16(routePoint, 36);
                        short dirVal = BitConverter.ToInt16(routePoint, 38);

                        // reserved => <II => little-endian
                        uint reservedA = BitConverter.ToUInt32(routePoint, 40);
                        uint reservedB = BitConverter.ToUInt32(routePoint, 44);

                        // Print them exactly as requested
                        LoggingManager.Instance.Log($"\n  Offset: 0x{currentOffset:X8}");
                        LoggingManager.Instance.Log($"    Flag: {flag:X2} {flag:X2}");
                        LoggingManager.Instance.Log($"    Group ID: {groupID:X2} {groupID:X2}");
                        LoggingManager.Instance.Log($"    Position (Floats): X={x:F6}, Y={y:F6}, Z={z:F6}");
                        LoggingManager.Instance.Log($"    Position (Hex): {BytesToHex(routePoint, 8, 12, spaced: true).ToUpper()}");
                        LoggingManager.Instance.Log($"    Axes (Floats): AX={ax:F6}, AY={ay:F6}, AZ={az:F6}");
                        LoggingManager.Instance.Log($"    Axes (Hex): {BytesToHex(routePoint, 20, 12, spaced: true).ToUpper()}");
                        LoggingManager.Instance.Log($"    Action: {BytesToHex(routePoint, 32, 2, spaced: true).ToUpper()} ");
                        LoggingManager.Instance.Log($"    Move: {BytesToHex(routePoint, 34, 2, spaced: true).ToUpper()} ");
                        LoggingManager.Instance.Log($"    Time: {BytesToHex(routePoint, 36, 2, spaced: true).ToUpper()} ({timeVal:X4}, Signed: {timeVal})");
                        LoggingManager.Instance.Log($"    Direction: {BytesToHex(routePoint, 38, 2, spaced: true).ToUpper()} ({dirVal:X4}, Signed: {dirVal})");
                        LoggingManager.Instance.Log($"    Reserved: {BytesToHex(routePoint, 40, 8, spaced: true).ToUpper()}");
                    }
                }
            } // end using
        }
        catch (FileNotFoundException)
        {
            LoggingManager.Instance.Log($"❌ Error: File not found at '{filePath}'");
        }
        catch (Exception ex)
        {
            LoggingManager.Instance.Log($"❌ Unexpected error: {ex}");
        }
    }

    // A small helper struct to hold guard header info
    private struct GuardHeader
    {
        public uint RouteID;
        public uint PointCount;
        public uint DataOffset;
    }

    /// <summary>
    /// Converts a byte subarray to a hex string, optionally with spaces between bytes.
    /// </summary>
    private static string BytesToHex(byte[] data, int index, int length, bool spaced = false)
    {
        if (index + length > data.Length) return "";
        var sb = new StringBuilder(length * (spaced ? 3 : 2));
        for (int i = 0; i < length; i++)
        {
            sb.AppendFormat(spaced ? "{0:X2} " : "{0:X2}", data[index + i]);
        }
        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Reads a big-endian uint from a byte array.
    /// Python's struct.unpack('>I') equivalent.
    /// </summary>
    private static uint ReadUIntBigEndian(byte[] data, int index)
    {
        // Reverse the 4 bytes to match big-endian
        byte[] temp = new byte[4];
        Array.Copy(data, index, temp, 0, 4);
        Array.Reverse(temp);
        return BitConverter.ToUInt32(temp, 0);
    }

    /// <summary>
    /// Converts a uint to a hex string (e.g., 0xABCDEFFF).
    /// </summary>
    private static string ToHex(uint value)
    {
        return "0x" + value.ToString("X");
    }
}
