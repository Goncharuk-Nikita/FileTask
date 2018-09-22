using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileTask.Services
{
    public class ParallelFileReader
    {
        private readonly FileReader _firstFileReader;
        private readonly FileReader _secondFileReader;

        public ParallelFileReader()
        {
            _firstFileReader = new FileReader("FIRST");
            _secondFileReader = new FileReader("SECOND");
        }



        public void Init(string[] files)
        {
            if (files is null)
            {
                throw new NullReferenceException("File paths cannot be null!");
            }

            foreach (var file in files)
            {
                if (_firstFileReader.FilesWeight <= _secondFileReader.FilesWeight)
                {
                    _firstFileReader.AddFile(file);
                }
                else
                {
                    _secondFileReader.AddFile(file);
                }
            }
        }



        public async Task ReadFiles()
        {
            if (0L.Equals(_firstFileReader.FilesWeight))
            {
                throw new NullReferenceException("Need to initialize first!");
            }

            await Task.WhenAny(
                    _firstFileReader.ReadFiles(),
                    _secondFileReader.ReadFiles())
                .ConfigureAwait(false);
        }
    }
}
