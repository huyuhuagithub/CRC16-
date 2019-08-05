using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseModule.Helper.ConvertFrom;
namespace XOR运算符
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsIllegalHexadecimal(textBox1.Text))
                {
                   
                    List<byte> af = new List<byte>();
                    string[] arraychar = textBox1.Text.Split(' ');
                    for (int i = 0; i < arraychar.Length; i++)
                    {
                        byte temp = Convert.ToByte(arraychar[i], 16);
                        af.Add(temp);
                    }
                    CRC16 cRC16 = new CRC16();
                    uint tt = cRC16.calc_crc16(af.ToArray(), af.Count);
                    byte[] bytes = BitConverter.GetBytes(tt);

                    string Result = ConvertFrom.ToHexString(bytes);
                    textBox2.Text = textBox1.Text + " " + Result.Substring(0,2) + " " + Result.Substring(3,2) + " " + "AA";
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
    }
}
