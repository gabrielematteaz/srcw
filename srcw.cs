using System;
using System.IO;

namespace srcw
{
    internal class Program
    {
        static string[] args;

        static void SearchWord(string file)
        {
            Console.WriteLine("File: " + file);
            StreamReader lettore = new StreamReader(file);
            int rigaN = 0, carattereN;

            while (true)
            {
                string riga = lettore.ReadLine();
                if (riga == null) break;

                carattereN = riga.IndexOf(args[2]);
                if (carattereN > -1)
                    Console.WriteLine("- R:" + rigaN + " C:" + carattereN + " \"" + riga + '\"');

                rigaN++;
            }

            lettore.Close();
        }

        static void ProcessDirectory(string percorso)
        {
            string[] files = Directory.GetFiles(percorso);
            foreach (string file in files)
                SearchWord(file);

            string[] directories = Directory.GetDirectories(percorso);
            foreach (string directory in directories)
                ProcessDirectory(directory);
        }

        static void Main()
        {
            args = Environment.GetCommandLineArgs();
            if (args.Length == 3)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;

                if (File.Exists(args[1]))
                {
                    Console.WriteLine("Occorrenze nel file:");
                    SearchWord(args[1]);
                }
                else if (Directory.Exists(args[1]))
                {
                    Console.WriteLine("Occorrenze nella directory:");
                    ProcessDirectory(args[1]);
                }
                else
                    Console.WriteLine("Il percorso non è né un file né una directory");
            }
            else
            {
                Console.WriteLine("Argomenti insufficienti");
            }
        }
    }
}