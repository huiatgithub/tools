using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace decodeCloudMusic
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                PrintUsage();
                return;
            }

            DecodeMusicFile(args[0]);
        }

        private static void PrintUsage()
        {
            StringBuilder usage = new StringBuilder();

            usage.AppendLine("Usage: ");
            usage.AppendLine("  This tool is used to decode wangyiyun cached music file. The decoded file will be created in the same location as encoded file. \n");
            usage.AppendLine("  decodeCloudMusic.exe <cached music file>");

            Console.WriteLine(usage.ToString());
        }

        private static void DecodeMusicFile(string encodedFile)
        {
            if (!File.Exists(encodedFile))
            {
                Console.WriteLine($"{encodedFile} does NOT exist!");
                return;
            }

            string decodedMusicFile = string.Format(@"{0}\{1}_decoded{2}", 
                Path.GetDirectoryName(encodedFile), 
                Path.GetFileNameWithoutExtension(encodedFile), 
                Path.GetExtension(encodedFile));

            using (BinaryReader reader = new BinaryReader(File.OpenRead(encodedFile)))
            {
                using (BinaryWriter writer = new BinaryWriter(File.Create(decodedMusicFile)))
                {
                    try
                    {
                        while (true)
                        {
                            byte oneByte = reader.ReadByte();
                            oneByte = (byte)(oneByte ^ 0xa3);
                            writer.Write(oneByte);
                        }
                    }
                    catch (EndOfStreamException)
                    {
                    }
                }
            }
        }
    }
}
