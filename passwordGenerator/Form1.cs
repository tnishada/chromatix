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
            int minTextFileWords, maxTextFileWords;

            try
            {
                minTextFileWords = Convert.ToInt32(textMin.Text);
                maxTextFileWords = Convert.ToInt32(textMax.Text);

                if (minTextFileWords > maxTextFileWords || textMax.Text == "" || textMin.Text == "" ) {
                    throw new System.InvalidOperationException();
                }
            }
            catch(Exception et) {
                minTextFileWords = 85;
                maxTextFileWords = 100;               
            }
            
            if (lines.Length < maxTextFileWords) {
                MessageBox.Show("Word count is less than "+maxTextFileWords);
                Environment.Exit(0);
            }
            
            int length = 160 + rd.Next(11);
            string randomText = CreateRandomPassword(length);

            if (randomText[randomText.Length - 1] == ' ')
            {
                randomText = randomText.Substring(0, randomText.Length - 1);
            }

            // randomly select the number of words to be inserted
            int wordsToBeInserted = rd.Next(minTextFileWords, maxTextFileWords);

            for (int index = 0; index < wordsToBeInserted; index++)
            {
                //select a word randomly from the list
                randomText += " " + lines[rd.Next(0, lines.Length - 1)];
            }

            // adding 3 special words to 5th 8th 12th spaces
            int whiteSpaces = 0;
            for (int index = 0; index < randomText.Length; index++)
            {
                if (randomText[index] == ' ')
                {
                    whiteSpaces++;

                    if (whiteSpaces == 8)
                    { 
                        randomText = randomText.Substring(0 , index) + " " + textWord1.Text +" "+ randomText.Substring(index+1) ;
                    }

                    if (whiteSpaces == 15)
                    {
                        randomText = randomText.Substring(0 , index) + " " + textWord2.Text + randomText.Substring(index+1); 
                   
                    }

                    if (whiteSpaces == 25)
                    {
                        randomText = randomText.Substring(0, index) + " " + textWord3.Text + randomText.Substring(index + 1);
                   
                    }
                }
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
           
            //generate a random text string
            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                
            }

            //put spaces beside a and i letters
            for (int index = 0; index < chars.Length; index++)
            {
                if ((index >1  && index < chars.Length - 3) && ((chars[index]) == 'A' || chars[index] == 'a' || chars[index] == 'i' || chars[index] == 'I'))
                {
                    chars[index - 1] = ' ';
                    chars[index + 1] = ' ';
                }
            }

            //reduce char length w/o white spaces 
            int counter = 0;
            for (int index = 0; index < chars.Length; index++) 
            {
                if (chars[index] == ' ')
                {
                    counter = 0;
                }
                else {
                    counter++;
                }

                //maximum length of a string componenet without white spaces is 12 chars
                if (counter > 12) 
                {
                    int rdval = rd.Next(2, 11);
                    index = index - rdval;
                    chars[index] = ' ';
                    counter = 0;
                }
            }

           

                return new string(chars);
        }
    }
}
