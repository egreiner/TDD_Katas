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


        public async Task<int> GetFileLengthAsync(string fileName) =>
            await Task.Run(() => 
                File.ReadLines(fileName).Count()
            ).ConfigureAwait(false);
    }
}