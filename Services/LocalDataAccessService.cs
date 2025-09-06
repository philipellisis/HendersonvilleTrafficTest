using HendersonvilleTrafficTest.Services.Interfaces;
using HendersonvilleTrafficTest.Services.Models;
using System.Globalization;
using System.Reflection;

namespace HendersonvilleTrafficTest.Services
{
    public class LocalDataAccessService : IDataAccessService
    {
        private readonly string _csvFilePath;

        public LocalDataAccessService()
        {
            // Get the directory where the executable is located
            var executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            _csvFilePath = Path.Combine(executableDirectory, "Test Sequence Database.csv");
        }

        public async Task<TestSequenceStep[]> GetTestSequenceAsync(string sequenceId)
        {
            if (!File.Exists(_csvFilePath))
            {
                throw new FileNotFoundException($"CSV file not found at: {_csvFilePath}");
            }

            var steps = new List<TestSequenceStep>();
            
            using var reader = new StreamReader(_csvFilePath);
            
            // Read header line
            var headerLine = await reader.ReadLineAsync();
            if (headerLine == null)
            {
                return Array.Empty<TestSequenceStep>();
            }

            // Process data lines
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(line))
                    continue;

                var fields = ParseCsvLine(line);
                if (fields.Length == 0 || fields[0] != sequenceId)
                    continue;

                try
                {
                    var step = new TestSequenceStep
                    {
                        Sequence = GetStringField(fields, 0),
                        Step = GetIntField(fields, 1),
                        StpNam = GetStringField(fields, 2),
                        Relay = GetIntField(fields, 3),
                        VacAct = GetIntField(fields, 4),
                        VacSet = GetDoubleField(fields, 5),
                        VacLcl = GetDoubleField(fields, 6),
                        VacUcl = GetDoubleField(fields, 7),
                        VdcAct = GetIntField(fields, 8),
                        VdcSet = GetDoubleField(fields, 9),
                        VdcLcl = GetDoubleField(fields, 10),
                        VdcUcl = GetDoubleField(fields, 11),
                        FrqAct = GetIntField(fields, 12),
                        FrqSet = GetDoubleField(fields, 13),
                        FrqLcl = GetDoubleField(fields, 14),
                        FrqUcl = GetDoubleField(fields, 15),
                        IAct = GetIntField(fields, 16),
                        ILcl = GetDoubleField(fields, 17),
                        IUcl = GetDoubleField(fields, 18),
                        PAct = GetIntField(fields, 19),
                        PLcl = GetDoubleField(fields, 20),
                        PUcl = GetDoubleField(fields, 21),
                        PFAct = GetIntField(fields, 22),
                        PFLcl = GetDoubleField(fields, 23),
                        PFUcl = GetDoubleField(fields, 24),
                        THDAct = GetIntField(fields, 25),
                        THDLIC = GetDoubleField(fields, 26),
                        THDLSC = GetDoubleField(fields, 27),
                        IntAct = GetIntField(fields, 28),
                        IntLIC = GetDoubleField(fields, 29),
                        IntLSC = GetDoubleField(fields, 30),
                        IntCalib = GetStringField(fields, 31),
                        At = GetDoubleField(fields, 32),
                        Bt = GetDoubleField(fields, 33),
                        ColorAct = GetIntField(fields, 34),
                        X1 = GetDoubleField(fields, 35),
                        Y1 = GetDoubleField(fields, 36),
                        X2 = GetDoubleField(fields, 37),
                        Y2 = GetDoubleField(fields, 38),
                        X3 = GetDoubleField(fields, 39),
                        Y3 = GetDoubleField(fields, 40),
                        X4 = GetDoubleField(fields, 41),
                        Y4 = GetDoubleField(fields, 42),
                        X5 = GetDoubleField(fields, 43),
                        Y5 = GetDoubleField(fields, 44),
                        X6 = GetDoubleField(fields, 45),
                        Y6 = GetDoubleField(fields, 46),
                        Ax = GetDoubleField(fields, 47),
                        Bx = GetDoubleField(fields, 48),
                        Ay = GetDoubleField(fields, 49),
                        By = GetDoubleField(fields, 50),
                        Note = GetStringField(fields, 51)
                    };

                    steps.Add(step);
                }
                catch (Exception ex)
                {
                    // Log parsing error but continue processing other lines
                    Console.WriteLine($"Error parsing CSV line: {ex.Message}");
                    continue;
                }
            }

            return steps.ToArray();
        }

        public async Task<string[]> GetAllTestSequencesAsync()
        {
            if (!File.Exists(_csvFilePath))
            {
                throw new FileNotFoundException($"CSV file not found at: {_csvFilePath}");
            }

            var sequenceIds = new HashSet<string>();

            using var reader = new StreamReader(_csvFilePath);

            // Read header line
            var headerLine = await reader.ReadLineAsync();
            if (headerLine == null)
            {
                return Array.Empty<string>();
            }

            // Process data lines
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(line))
                    continue;

                var fields = ParseCsvLine(line);
                if (fields.Length > 0 && !string.IsNullOrEmpty(fields[0]))
                {
                    sequenceIds.Add(fields[0].Trim());
                }
            }

            return sequenceIds.OrderBy(s => s).ToArray();
        }

        private static string[] ParseCsvLine(string line)
        {
            var fields = new List<string>();
            var inQuotes = false;
            var currentField = "";

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(currentField);
                    currentField = "";
                }
                else
                {
                    currentField += c;
                }
            }

            // Add the last field
            fields.Add(currentField);

            return fields.ToArray();
        }

        private static string GetStringField(string[] fields, int index)
        {
            return index < fields.Length ? fields[index].Trim() : "";
        }

        private static int GetIntField(string[] fields, int index)
        {
            if (index >= fields.Length)
                return 0;

            var value = fields[index].Trim();
            return int.TryParse(value, out var result) ? result : 0;
        }

        private static double GetDoubleField(string[] fields, int index)
        {
            if (index >= fields.Length)
                return 0.0;

            var value = fields[index].Trim();
            return double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result) ? result : 0.0;
        }
    }
}