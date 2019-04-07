using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Groups
{
    public partial class Form1 : Form
    {
        string path = "null";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            path = openFileDialog1.FileName;
            label1.Text = path;
            UpdateFields(path);
        }
        private void UpdateFields(String path)
        {
            int groupNum = (int)numericUpDown1.Value;
            List<String> Bowl = Read(path);
            int eqDivide = Bowl.Count + (groupNum - (Bowl.Count % groupNum));
            String[,] Micah = new String[groupNum, eqDivide / groupNum];
            Micah = setEmpty(Micah);
            int stop = Math.Abs(Bowl.Count - eqDivide);
            for (int i = 0; i < stop; i++)
            {
                Bowl.Add(" ");
                Micah[i % groupNum, (eqDivide / groupNum) - 1] = " ";
            }
            int[] Prim = new int[groupNum];
            for (int i = 0; i < Prim.Length; i++)
            {
                Prim[i] = 0;
            }
            Random rand = new Random();
            foreach (String Fish in Bowl)
            {
                if (!Fish.Equals(" "))
                {
                    int sect;
                    do
                    {
                        sect = (int)(rand.NextDouble() * 100) % groupNum;
                    } while ((eqDivide / groupNum - Prim[sect] == 0) || ((eqDivide / groupNum - Prim[sect] == 1) && (Micah[sect, (eqDivide / groupNum) - 1].Equals(" "))));

                    Micah[sect, Prim[sect]] = Fish;
                    Prim[sect]++;
                }

            }
            String groups = "";
            for (int Nathan = 0; Nathan < groupNum; Nathan++)
            {
                groups += "Group " + (Nathan + 1) + "\r\n----------\r\n";
                for (int Chris = 0; Chris < (Prim[Nathan]); Chris++)
                {
                    groups += Micah[Nathan, Chris] + "\r\n";
                }
                groups += "\r\n \r\n";

            }
            textBox1.Text = groups;

        }
        public static String[,] setEmpty(String[,] inpt)
        {
            for (int i = 0; i < inpt.GetLength(0); i++)
            {
                for (int j = 0; j < inpt.GetLength(1); j++)
                {
                    inpt[i, j] = "";
                }
            }
            return inpt;
        }
        public static List<String> Read(String file)
        {//READS A FILE
            List<String> Paragraph = new List<String>();
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(file);

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    //Add the line to the list
                    Paragraph.Add(line);
                    //Read the next line
                    line = sr.ReadLine();
                }

                //Close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception at readFile: " + e.Message);
            }
            return Paragraph;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (path.Equals("null"))
            {
                textBox1.Text = textBox1.Text + "Please select a file.\r\n";
            }
            else
            {
                UpdateFields(path);
            }
        }
    }
}
