using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileTask.Services
{
    public class FileReader
    {
        public long FilesWeight { get; private set; }


        private readonly string _readerName;

        private readonly List<FileInfo> _fileInfos;



        public FileReader(string readerName)
        {
            _readerName = readerName;

            _fileInfos = new List<FileInfo>();
        }


        public void AddFile(string filePath)
        {
            var fileInfo = new FileInfo(filePath);

            _fileInfos.Add(fileInfo);
            FilesWeight += fileInfo.Length;
        }



        public async Task ReadFiles()
        {
            foreach (var file in _fileInfos)
            {
                var fileText = await ReadFile(file);
                Console.WriteLine($"[{_readerName}] has been read{Environment.NewLine}" +
                                  $"Size: {file.Length}{Environment.NewLine}" +
                                  $"Path: {file}{Environment.NewLine}");
            }
        }

        private async Task<string> ReadFile(FileInfo file)
        {
            using (var stream = file.OpenRead())
            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
