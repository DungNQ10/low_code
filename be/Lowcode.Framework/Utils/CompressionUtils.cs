using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Lowcode.Framework.Utils
{
    public static class CompressionUtils
    {
        public static void Compress(List<string> files, string outputFile)
        {
            using (var zip = ZipFile.Open(outputFile, ZipArchiveMode.Create))
            {
                // Create and open a new ZIP file

                foreach (var file in files)
                    // Add the entry for each file
                    zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
            }
        }

        public static void Compress(List<FileCompressionModel> files, string outputFile)
        {
            using (var zip = ZipFile.Open(outputFile, ZipArchiveMode.Create))
            {
                // Create and open a new ZIP file

                foreach (var file in files)
                    // Add the entry for each file
                    zip.CreateEntryFromFile(file.FilePath, file.FileName, CompressionLevel.Optimal);
            }
        }
    }

    public class FileCompressionModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

    }
}