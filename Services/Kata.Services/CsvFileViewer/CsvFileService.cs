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

        public async Task<IEnumerable<string>> ReadFileAsync(string fileName, int start, int length)
        {
            return await Task.Run(() =>
                File.ReadLines(fileName).Skip(start).Take(length)
            ).ConfigureAwait(false);
        }

        // this version is 10 timex slower...
        public async IAsyncEnumerable<string> ReadFileAsyncV2(string fileName, int start, int length)
        {
            // TIP async enumerable stream
            var counter = 0;
            using var reader = new StreamReader(File.OpenRead(fileName));
            while (counter < (start + length) && !reader.EndOfStream)
            {
                counter++;
                var line = await reader.ReadLineAsync();
                if (counter > start && line?.Trim().Length > 0)
                    yield return line;
            }
        }



        public async Task<int> GetFileLengthAsync(string fileName) =>
            await Task.Run(() => 
                File.ReadLines(fileName).Count()
            ).ConfigureAwait(false);
    }
}