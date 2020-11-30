namespace Kata.Services.CsvTableizer
{
    using System.Collections.Generic;
    using System.IO;

    public class CsvFileService
    {
        public List<string> ReadFile(string fileName)
        {
            var list = new List<string>();
            var reader = new StreamReader(File.OpenRead(fileName));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                list.Add(line);
            }

            return list;
            ////return FakeCsvLines.GetLines().ToList();
        }

        // TODO WriteFile(string fileName, List<string> csvLines)
        public bool WriteFile(string fileName, List<string> csvLines)
        {
            return false;
        }
    }
}