using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOR运算符
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static Encoding encod;
        private void button1_Click(object sender, EventArgs e)
        {
            button3_Click("F", e);

            //textBox11.Text = "";
            //int Y = 0XFF;

            //int D = Convert.ToInt32(textBox4.Text, 16);
            //int D1 = Convert.ToInt32(textBox5.Text, 16);
            //int D2 = Convert.ToInt32(textBox6.Text, 16);
            //int D3 = Convert.ToInt32(textBox7.Text, 16);
            //int D4 = Convert.ToInt32(textBox8.Text, 16);
            //int D5 = Convert.ToInt32(textBox9.Text, 16);
            //int D6 = Convert.ToInt32(textBox10.Text, 16);
            //int z = D ^ D1 ^ D2 ^ D3 ^ D4 ^ D5 ^ D6 ^ Y;//8A
            //string s = "";
            //s = textBox4.Text + " " + textBox5.Text + " " + textBox6.Text + " " + textBox7.Text + " " + textBox8.Text + " " + textBox9.Text + " " + textBox10.Text;
            //string[] data1 = s.Split(' ', ',');
            //string[] vs = data1.Where(t => t != "00").ToArray();
            //int len = vs.Length + 1;
            //textBox3.Text = len.ToString();
            //foreach (var item in vs)
            //{
            //    textBox11.Text += item+" ";
            //}
            //textBox1.Text =textBox2.Text+" "+textBox3.Text+" "+ textBox11.Text+ Convert.ToString(z, 16).ToUpper();


            //int E = 0X07;
            //int X = 0X00;
            //int Y = 0XFF;
            //int z = X ^ D ^ Y ^ E;//8A
            //textBox1.Text = Convert.ToString(z, 16).ToUpper();

            //bool s = IsIllegalHexadecimal(textBox4.Text);


            //string hexValues = "48 65 6C 6C 6F 20 57 6F 72 6C 64 21";
            //string[] hexValuesSplit = hexValues.Split(' ');
            //foreach (String hex in hexValuesSplit)
            //{
            //    // Convert the number expressed in base-16 to an integer.
            //    int value = Convert.ToInt32(hex, 16);
            //    // Get the character corresponding to the integral value.
            //    string stringValue = Char.ConvertFromUtf32(value);
            //    char charValue = (char)value;
            //    Console.WriteLine("hexadecimal value = {0}, int value = {1}, char value = {2} or {3}",
            //                        hex, value, stringValue, charValue);
            //}
            //string str = textBox4.Text.Replace(" ", "");
            //byte[] byteArray = System.Text.Encoding.Default.GetBytes(textBox4.Text);
            //byte[] vals = { 0x01, 0xAA, 0xB1, 0xDC, 0x10, 0xDD };

            //string str1 = BitConverter.ToString(byteArray);
            //Console.WriteLine(str1);

            //str = BitConverter.ToString(byteArray).Replace("-", "");
            //Console.WriteLine(str1);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = "0123456789ABCDEF".IndexOf(char.ToUpper(e.KeyChar)) < 0;
        }

        public static string StringToHexString(string String)
        {
            string str2 = string.Empty;
            try
            {
                byte[] bytes = encod.GetBytes(String);
                for (int i = 0; i < bytes.Length; i++)
                {
                    str2 = str2 + " " + Convert.ToString(bytes[i], 0x10);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "转换失败", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return str2.Trim();
        }

        public static byte[] ModelToByteArray(string DecimalString, int Model)
        {
            byte[] buffer = null;
            try
            {
                char[] trimChars = new char[] { '%', ' ', '$', '&' };
                DecimalString = DecimalString.Trim(trimChars);
                string[] strArray = DecimalString.Split(trimChars);
                buffer = new byte[strArray.Length];
                for (int i = 0; i < strArray.Length; i++)
                {
                    buffer[i] = Convert.ToByte(strArray[i], Model);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "转换失败", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return buffer;
        }

        public const string PATTERN = @"([^A-Fa-f0-9]|\s+?)+";
        /// <summary>
        /// 判断十六进制字符串hex是否正确
        /// </summary>
        /// <param name="hex">十六进制字符串</param>
        /// <returns>true：不正确，false：正确</returns>
        public bool IsIllegalHexadecimal(string hex)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(hex, PATTERN);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsIllegalHexadecimal(textBox12.Text))
                {
                    int result = 0;
                    List<int> af = new List<int>();
                    af.Add(170);
                    string[] arraychar = textBox12.Text.Split(' ');
                    for (int i = 0; i < arraychar.Length; i++)
                    {
                        af.Add(Convert.ToInt32(arraychar[i], 16));
                        result = af[i] ^ result;
                    }
                    result = result ^ 0xFF;
                    int len = arraychar.Length + 1;
                    textBox3.Text = len.ToString("X");
                    textBox1.Text = textBox2.Text + " " + textBox3.Text + " " + textBox12.Text + " " + Convert.ToString(result, 16).ToUpper().ToString();
                    //SP sP = new SP();
                    //byte[] spbytelist = sP.strToToHexByte(textBox1.Text);

                }
                else
                {
                    MessageBox.Show("请输入正确的HEX");
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }

        }

        public int ReturnintXOR(string text)
        {
            int result = 0;
            List<int> af = new List<int>();
            af.Add(170);
            string[] arraychar = text.Split(' ');
            for (int i = 0; i < arraychar.Length; i++)
            {
                af.Add(Convert.ToInt32(arraychar[i], 16));
                result = af[i] ^ result;
            }
            return result = result ^ 0xFF;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //comboBox2.Items.AddRange(SerialPort.GetPortNames().OrderBy(t => t).ToArray());
            //comboBox2.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //SP sP = new SP();
            //textBox13.Text = sP.testhexString(comboBox2.Text, textBox1.Text);
            //int result = 0;
            //List<int> af = new List<int>();
            //string[] arraychar = textBox12.Text.Split(' ');
            //for (int i = 0; i < arraychar.Length; i++)
            //{
            //    af.Add(Convert.ToInt32(arraychar[i], 16));
            //    result = af[i] ^ result;
            //}
            //Encoding encoder =null;
            //encod = Encoding.ASCII;
            //string sf= encod.GetString(af.ToArray());

            //textBox14.AppendText("jifjejifjejifjejifjejifjejifjejifjejifjejifjejifjejifjejifjejifjejifjejifjejifjejifjejifjejifjejifje\r\n");

        }
    }
}
