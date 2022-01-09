using System;
using System.IO;

namespace SPEBINCONV
{
    class Program
    {
        static void SPEtoBIN(string iFile, string oFile)
        {
            try
            {
                byte[] iFileBytes = File.ReadAllBytes(iFile);
                for (int i = 0; i < iFileBytes.Length; i++)
                {
                    iFileBytes[i] = (byte)(iFileBytes[i] - (((i & 1) == 1) ? (16 + i % 16) : 18));
                }
                File.WriteAllBytes(oFile, iFileBytes);
            } 
            catch
            {
                Console.WriteLine($"Unknown error during converting \"{iFile}\" to .spebin");
            }
        }

        static void BINtoSPE(string iFile, string oFile)
        {
            try
            {
                byte[] iFileBytes = File.ReadAllBytes(iFile);
                for (int i = 0; i < iFileBytes.Length; i++)
                {
                    iFileBytes[i] = (byte)(iFileBytes[i] + (((i & 1) == 1) ? (16 + i % 16) : 18));
                }
                File.WriteAllBytes(oFile, iFileBytes);
            }
            catch
            {
                Console.WriteLine($"Unknown error during converting \"{iFile}\" to .spe");
            }
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please input at least one file [.spe; .spebin] to convert!");
            }
            else
            {
                int numFilesTotal = args.Length+1;
                int numFilesProc = 1;
                foreach (string filename in args)
                {
                    if (File.Exists(filename))
                    {
                        string ext = Path.GetExtension(filename).ToUpper();
                        switch (ext)
                        {
                            case ".SPE":
                                Console.WriteLine($"Now converting \"{filename}\" to .spebin ({numFilesProc}/{numFilesTotal})!");
                                SPEtoBIN(filename, Path.ChangeExtension(filename, ".spebin"));
                                Console.WriteLine($"\"{filename}\" converted succesfully!");
                                break;
                            case ".SPEBIN":
                                Console.WriteLine($"Now converting \"{filename}\" to .spe ({numFilesProc}/{numFilesTotal})!");
                                BINtoSPE(filename, Path.ChangeExtension(filename, ".spe"));
                                Console.WriteLine($"\"{filename}\" converted succesfully!");
                                break;
                            default:
                                Console.WriteLine($"\"{filename}\" isn't SPE file [.spe; .spebin] - skipping!");
                                break;
                        }
                        numFilesProc++;
                    }
                    else
                    {
                        Console.WriteLine($"No such file \"{filename}\" - skipping!");
                    }
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
