using System;
using System.IO;
using System.IO.Compression;

namespace alaaali
{
    public class FileCompressor
    {
        public void CompressFile(string inputFile, string outputFile)
        {
            // Check if the input file exists
            if (!File.Exists(inputFile))
            {
                throw new FileNotFoundException("Input file not found.", inputFile);
            }

            // Create the output directory if it doesn't exist
            string outputDirectory = Path.GetDirectoryName(outputFile);
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            // Compress the input file
            using (FileStream inputStream = File.OpenRead(inputFile))
            {
                using (FileStream outputStream = File.Create(outputFile))
                {
                    using (GZipStream compressStream = new GZipStream(outputStream, CompressionMode.Compress))
                    {
                        inputStream.CopyTo(compressStream);
                    }
                }
            }
        }
    }
}