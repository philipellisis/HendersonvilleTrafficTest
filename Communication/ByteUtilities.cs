using System.Text;

namespace HendersonvilleTrafficTest.Communication
{
    public static class ByteUtilities
    {
        /// <summary>
        /// Converts a hex string to byte array. Supports formats like "FF 00 AA" or "FF00AA"
        /// </summary>
        public static byte[] HexStringToBytes(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
                return Array.Empty<byte>();

            // Remove spaces and convert to uppercase
            hexString = hexString.Replace(" ", "").Replace("-", "").ToUpper();

            // Ensure even number of characters
            if (hexString.Length % 2 != 0)
                throw new ArgumentException("Hex string must have an even number of characters");

            var bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return bytes;
        }

        /// <summary>
        /// Converts byte array to hex string with optional separator
        /// </summary>
        public static string BytesToHexString(byte[] bytes, string separator = " ")
        {
            if (bytes == null || bytes.Length == 0)
                return string.Empty;

            return string.Join(separator, bytes.Select(b => b.ToString("X2")));
        }

        /// <summary>
        /// Converts byte array to hex string without separators
        /// </summary>
        public static string BytesToHexStringCompact(byte[] bytes)
        {
            return BytesToHexString(bytes, "");
        }

        /// <summary>
        /// Converts string to ASCII byte array
        /// </summary>
        public static byte[] StringToAsciiBytes(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Array.Empty<byte>();

            return Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Converts ASCII byte array to string
        /// </summary>
        public static string AsciiBytesToString(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return string.Empty;

            return Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// Calculates checksum (XOR of all bytes)
        /// </summary>
        public static byte CalculateXorChecksum(byte[] data)
        {
            if (data == null || data.Length == 0)
                return 0;

            byte checksum = 0;
            foreach (byte b in data)
            {
                checksum ^= b;
            }
            return checksum;
        }

        /// <summary>
        /// Calculates checksum (sum of all bytes, truncated to byte)
        /// </summary>
        public static byte CalculateSumChecksum(byte[] data)
        {
            if (data == null || data.Length == 0)
                return 0;

            int sum = 0;
            foreach (byte b in data)
            {
                sum += b;
            }
            return (byte)(sum & 0xFF);
        }

        /// <summary>
        /// Appends checksum to data array
        /// </summary>
        public static byte[] AppendChecksum(byte[] data, ChecksumType type = ChecksumType.Xor)
        {
            if (data == null)
                return Array.Empty<byte>();

            var result = new byte[data.Length + 1];
            Array.Copy(data, result, data.Length);

            result[data.Length] = type switch
            {
                ChecksumType.Xor => CalculateXorChecksum(data),
                ChecksumType.Sum => CalculateSumChecksum(data),
                _ => 0
            };

            return result;
        }

        /// <summary>
        /// Validates checksum in data array (checksum is last byte)
        /// </summary>
        public static bool ValidateChecksum(byte[] dataWithChecksum, ChecksumType type = ChecksumType.Xor)
        {
            if (dataWithChecksum == null || dataWithChecksum.Length < 2)
                return false;

            var data = new byte[dataWithChecksum.Length - 1];
            Array.Copy(dataWithChecksum, data, data.Length);

            var expectedChecksum = type switch
            {
                ChecksumType.Xor => CalculateXorChecksum(data),
                ChecksumType.Sum => CalculateSumChecksum(data),
                _ => (byte)0
            };

            return dataWithChecksum[dataWithChecksum.Length - 1] == expectedChecksum;
        }

        /// <summary>
        /// Finds a pattern in byte array
        /// </summary>
        public static int FindPattern(byte[] data, byte[] pattern, int startIndex = 0)
        {
            if (data == null || pattern == null || pattern.Length == 0 || 
                startIndex < 0 || startIndex >= data.Length)
                return -1;

            for (int i = startIndex; i <= data.Length - pattern.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (data[i + j] != pattern[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Combines multiple byte arrays into one
        /// </summary>
        public static byte[] CombineArrays(params byte[][] arrays)
        {
            if (arrays == null || arrays.Length == 0)
                return Array.Empty<byte>();

            var totalLength = arrays.Sum(arr => arr?.Length ?? 0);
            var result = new byte[totalLength];
            var offset = 0;

            foreach (var array in arrays)
            {
                if (array != null && array.Length > 0)
                {
                    Array.Copy(array, 0, result, offset, array.Length);
                    offset += array.Length;
                }
            }

            return result;
        }
    }

    public enum ChecksumType
    {
        Xor,
        Sum
    }
}