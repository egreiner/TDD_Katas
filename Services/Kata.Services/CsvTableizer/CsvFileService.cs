namespace Kata.Services.CsvTableizer
{
    using System.Collections.Generic;
    using System.IO;

    public class CsvFileService
    {
        public List<string> ReadFile(string fileName)
        {
            ////return FakeCsvLines.GetLines().ToList();

            var list = new List<string>();
            var reader = new StreamReader(File.OpenRead(fileName));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line?.Trim().Length > 0)
                    list.Add(line);
            }

            return list;
        }

        // TODO WriteFile(string fileName, List<string> csvLines)
        public bool WriteFile(string fileName, List<string> csvLines)
        {
            return false;
        }
    }
}