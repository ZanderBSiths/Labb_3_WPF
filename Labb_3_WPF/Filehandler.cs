﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_WPF
{
    public class Filehandler
    {

        public static async Task WriteFile(string text)
        {

            using (StreamWriter writeOrder = new StreamWriter("bokningar.log", true))
            {

                writeOrder.WriteLine(text);
            }
        }

        public static List<string> GetTextsFile()
        {
            if (File.Exists("bokningar.log") == false)
            {
                WriteFile("Bokningar 14 november till 20 november");
            }


            List<string> texts = new List<string>();


            string line = "";



           using (StreamReader stream = new StreamReader("bokningar.log"))
            {
                while ((line = stream.ReadLine()) != null)
                {
                    texts.Add(line);

                }
            }

            return texts;

        }




    }
}
