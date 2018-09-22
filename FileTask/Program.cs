using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileTask.Services;

namespace FileTask
{
    public class Program
    {
        private static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
            Console.ReadKey();
        }

        private static async Task MainAsync(string[] args)
        {
            var fileTask = new ParallelFileReader();

            var filePaths = GetFiles();
            fileTask.Init(filePaths);

            await fileTask.ReadFiles();
        }

        private static string[] GetFiles()
        {
            const string fileDirectoryTemplate = @"{0}\Files";

            var rootDirectory = Environment.CurrentDirectory;

            return Directory.GetFiles(
                string.Format(fileDirectoryTemplate, rootDirectory));
        }
    }
}
