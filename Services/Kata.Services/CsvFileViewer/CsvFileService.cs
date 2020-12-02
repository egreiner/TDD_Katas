namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class CsvFileService
    {
        public List<string> ReadSmallFile(string fileName)
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

        public async IAsyncEnumerable<string> ReadFileAsync(string fileName, int start, int end)
        {
            // TIP async enumerable stream
            using var reader = new StreamReader(File.OpenRead(fileName));
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (line?.Trim().Length > 0)
                    yield return line;
            }
        }

        // this is slower than V2...
        public async Task<int> GetFileLengthAsync(string fileName)
        {
            using var reader = new StreamReader(File.OpenRead(fileName));
            var result = 0;
            while (!reader.EndOfStream)
            {
                result++;
                _ = await reader.ReadLineAsync();
            }

            return result;
        }

        public async Task<int> GetFileLengthAsyncV2(string fileName) =>
            await Task.Run(() => 
                File.ReadLines(fileName).Count()
            ).ConfigureAwait(false);


        // TODO WriteFile(string fileName, List<string> csvLines)
        public bool WriteFile(string fileName, List<string> csvLines)
        {
            return false;
        }
    }
}