using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
       public static  int counter = 1;
        static void Main(string[] args)
        {
            
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader("levelpack_1.txt");
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
                OutputFile(line);
                counter++;
            }

            file.Close();

            // Suspend the screen.
            Console.ReadLine();
        }

        /*
         public static int[][] level1 = new int[][] {
            new int[] { 00, 00, 00, 00, 00, 00},
            new int[] { 00, -1, -1, -1, -1, 00},
            new int[] { 00, -1, -1, -1, -1, 00},
            new int[] { 00, -1, -1, -1, -1, 00},
            new int[] { 00, -1, -1, -1, -1, 00},
            new int[] { 00, 01, -1, -1, 06, 00},            
            new int[] { 00, 00, 00, 00, 00, 00},
                       };
        */
        public static void OutputFile(string line)
        {
            //5,0,1,5;0,5,10,15,20,21;2,1,6,11,16;7,12,17,22;4,3,8,13,18;9,14,19,24,23
            if (line.Length < 4)
                return;
            int maxCOl = Int32.Parse(line[0].ToString());//bo cai dau tien ra;
            string[][] array = new string[maxCOl + 2][];
            for (int i = 0; i < maxCOl + 2; i++)
            {
                array[i] = new string[maxCOl + 2];                
            }

            for (int i = 0; i < maxCOl + 2; i++)//init bien rong
            {
                for (int j = 0; j < maxCOl + 2; j++)
                {
                    array[i][j] = "00";
                }
            }
            string[] lines = line.Split(';');// tach ra cac hang noi
            int currentvalue = 1;
            for (int i = 1; i < lines.Length; i++)//bo hang noi dau tien
            {
                string[] lines_s2 = lines[i].Split(',');//tach ra cac hang con
                for (int j = 0; j < lines_s2.Length; j++)
                {
                    int index = Int32.Parse(lines_s2[j].ToString());
                    int row = (index) / maxCOl;
                    int col = index - row * maxCOl;
                    if (j == 0)
                        array[row + 1][col +1] ="0" + currentvalue.ToString();
                    else
                        if (j == lines_s2.Length - 1)
                            array[row +1][col+1] =(10+ currentvalue).ToString();
                        else
                            array[row+1][col+1] = "-1";
                }
                currentvalue++;
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("output.txt",true))
            {
                
                file.WriteLine("public static int[][] level"+ Program.counter.ToString() +" = new int[][] {");

                for (int i = 0; i < maxCOl + 2; i++)
                {
                    file.Write("new int[] { ");                    
                    for (int j = 0; j < maxCOl + 2; j++)
                    {

                        file.Write(array[i][j] +",");
                    }

                    file.WriteLine(" },");
                }
                file.WriteLine("      };");
            }


        }
    }

}


