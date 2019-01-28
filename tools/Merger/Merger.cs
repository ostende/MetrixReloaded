﻿using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Merger
{
    class Merger
    {
        static void Main(string[] args)
        {
            try
            {
                Config config = Config.Load();

                string skinFilePath = config.getSkinFilePath();
                string skinFileName = skinFilePath + "skin.xml";

                string skinProductionFileName = config.getProductionPath() + "skin.xml";

                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(config.getInputPath());

                String skinFileString = "<skin>";

                foreach (System.IO.FileInfo file in directory.GetFiles())
                {
                    if (file.Name.Contains(".xml") & !file.Name.Equals("skin.xml"))
                    {
                        Console.Write(String.Format("Reading {0}\n", file.Name));

                        String fileString = File.ReadAllText(file.FullName);
                        skinFileString = String.Format("{0}\n{1}", skinFileString, File.ReadAllText(file.FullName));
                    }
                }
                Console.Write("\n-------------------------------------------------------------\n");
                Console.Write(String.Format("Merging xml files in {0}\n", skinFileName));

                skinFileString = String.Format("{0}\n</skin>", skinFileString, FileMode.Create);

                File.WriteAllText(skinFileName, skinFileString);
                File.WriteAllText(skinProductionFileName, skinFileString);

                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Error: {0}\n", ex.Message);
                Console.Write("Error: {0}\n", ex.StackTrace);
                Console.WriteLine();
                Console.Write("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
