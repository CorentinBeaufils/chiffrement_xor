using System;
using System.IO;
using System.Security.Cryptography;

namespace chiffrement_xor
{
    using System;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: EncryptFiles <directoryPath> <keyFilePath>");
                Console.WriteLine("lognuere {0}", args.Length);
                return;
            }

            string directoryPath = args[0];
            string keyFilePath = args[1];

            byte[] xorKey = LoadXorKey(keyFilePath);

            EncryptFile(directoryPath, xorKey);

            Console.WriteLine("Encryption complete.");
        }

        static byte[] LoadXorKey(string keyFilePath)
        {
            try
            {
                return File.ReadAllBytes(keyFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading XOR key: {ex.Message}");
                Environment.Exit(1);
                return null; // This line is not actually reachable, but required for compilation
            }
        }

        static void EncryptFiles(string directoryPath, byte[] xorKey)
        {
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string filePath in files)
            {
                EncryptFile(filePath, xorKey);
            }
        }

        static void EncryptFile(string filePath, byte[] xorKey)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);

                for (int i = 0; i < fileBytes.Length; i++)
                {
                    fileBytes[i] ^= xorKey[i % xorKey.Length];
                }

                File.WriteAllBytes(filePath, fileBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error encrypting file {filePath}: {ex.Message}");
            }
        }
    }

}
