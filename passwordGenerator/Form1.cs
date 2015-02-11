using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace passwordGenerator
{
    public partial class Form1 : Form
    {
        string[] lines; // store words of the text file

        public Form1()
        {
            InitializeComponent();

            try
            {
                lines = System.IO.File.ReadAllLines(@"rcgc.txt"); // load the words to the array
            }
            catch (Exception e) {
                MessageBox.Show("Text File Is Not Found");
                Environment.Exit(0);
            }
        }

       public static Random rd = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            if (lines.Length < 100) {
                MessageBox.Show("Word count is less than 100");
                Environment.Exit(0);
            }

            int length = 160 + rd.Next(11);
            string randomText = CreateRandomPassword(length);

            if (randomText[randomText.Length - 1] == ' ')
            {
                randomText = randomText.Substring(0, randomText.Length - 1);
            }

            // randomly select the number of words to be inserted
            int wordsToBeInserted = rd.Next(85, 100);

            for (int index = 0; index < wordsToBeInserted; index++)
            {
                //select a word randomly from the list
                randomText += " " + lines[rd.Next(0, lines.Length - 1)];
            }
                        
            gen.Text = randomText;
            System.Windows.Forms.Clipboard.SetText(gen.Text); // copy to the clipboard

            /*select all text*/
            gen.SelectAll();  
            gen.Focus();            
            
        }

        private static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] chars = new char[passwordLength];
           

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                
            }

            for (int index = 0; index < chars.Length; index++)
            {
                if ((index >1  && index < chars.Length - 3) && ((chars[index]) == 'A' || chars[index] == 'a' || chars[index] == 'i' || chars[index] == 'I'))
                {
                    chars[index - 1] = ' ';
                    chars[index + 1] = ' ';
                }
            }

                return new string(chars);
        }
    }
}
