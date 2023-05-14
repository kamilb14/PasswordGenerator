using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordGenerator
{
    public partial class Form1 : Form
    {

        private const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Numbers = "0123456789";
        private const string SpecialCharacters = "!@#$%^&*()_+-=[]{}|;:,./?><";
        private List<string> charLists = new List<string>();
        private readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var pass_len = (int)numericUpDown1.Value;
            var includeLowercase = checkBox2.Checked;
            var includeUppercase = checkBox1.Checked;
            var includeNumbers = checkBox4.Checked;
            var includeSpecialCharacters = checkBox3.Checked;

            var characterPool = string.Empty;

            if (includeLowercase)
            {
                characterPool += LowercaseLetters;
                charLists.Add(LowercaseLetters);
            }
            if (includeUppercase)
            {
                characterPool += UppercaseLetters;
                charLists.Add(UppercaseLetters);
            }
            if (includeNumbers)
            {
                characterPool += Numbers;
                charLists.Add(Numbers);
            }
            if (includeSpecialCharacters)
            {
                characterPool += SpecialCharacters;
                charLists.Add(SpecialCharacters);
            }

            if (charLists.Count != 0)
            {
                var password = GeneratePassword(pass_len, characterPool);
                password = GeneratePassword(pass_len, characterPool);
                richTextBox1.Text = password;
            }
        }


            public string GeneratePassword(int length, string characterPool)
            {
                bool containsAll = false;
                string password = string.Empty;

                do
                {
                    password = string.Empty;
                    var bytes = new byte[length];
                    Rng.GetBytes(bytes);

                    for (var i = 0; i < length; i++)
                    {
                        var characterIndex = bytes[i] % characterPool.Length;
                        password += characterPool[characterIndex];
                    }

                    containsAll = true;
                    foreach (string list in charLists)
                    {
                        if (string.IsNullOrEmpty(list))
                        {
                            continue;
                        }

                        bool containsAtLeastOne = false;
                        foreach (char c in list)
                        {
                            if (password.Contains(c.ToString()))
                            {
                                containsAtLeastOne = true;
                                break;
                            }
                        }

                        if (!containsAtLeastOne)
                        {
                            containsAll = false;
                            break;
                        }
                    }

                } while (!containsAll);

                return password;
            }
        }

}
        
    

