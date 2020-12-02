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
            return File.ReadLines(fileName).ToList();
        }

        public async Task<IEnumerable<string>> ReadFileAsync(string fileName, int start, int length) =>
            await Task.Run(() =>
                File.ReadLines(fileName).Skip(start).Take(length)
            ).ConfigureAwait(false);


        public async Task<int> GetFileLengthAsync(string fileName) =>
            await Task.Run(() => 
                File.ReadLines(fileName).Count()
            ).ConfigureAwait(false);
    }
}