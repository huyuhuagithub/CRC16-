using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseModule.Helper;
using AbstractEquipment.RS232Equipment;
using System.IO.Ports;
using BaseModule.Helper.ConvertFrom;
using System.Configuration;
namespace XOR运算符
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            QueryToTable();
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
        private unsafe void button1_Click(object sender, EventArgs e)
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

                    //用指针的方式获取 uint 的字节数组
                    List<byte> plist = new List<byte>();
                   
                    byte* Pbyte = (byte*)&tt;
                    for (int i = 0; i < sizeof(uint); ++i)
                    {
                        plist.Add(*Pbyte);
                        Pbyte++;
                    }
                    textBox2.Text = textBox1.Text + " " + string.Format("{0:X2} {1:X2}", plist[0], plist[1]) + " " + "AA";
                    write(textBox2.Text);
                    //byte[] bytes = BitConverter.GetBytes(tt);
                    //string Result = ConvertFrom.ToHexString(plist.ToArray());
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

        private void button2_Click(object sender, EventArgs e)
        {
            RS232Data rS232Data = new RS232Data();
            rS232Data.Description = textBox4.Text;
            rS232Data.TextData = textBox2.Text;
            rS232Data.Select = false;

            if (OLEDBHelper.InsertEntity(rS232Data))
            {
                button2.BackColor = Color.Green;
            }
            else
            {
                button2.BackColor = Color.Red;
            }
            QueryToTable();

        }

        private void QueryToTable()
        {
            dataGridView1.Rows.Clear();
            var datas = OLEDBHelper.GetEntitylist<RS232Data>();
            foreach (var item in datas)
            {
                dataGridView1.Rows.Add(item.id, item.Select, item.Description, item.TextData);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            QueryToTable();
        }
      
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //点击button按钮事件
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column3" && e.RowIndex >= 0)
            { 
                int rowindex = dataGridView1.CurrentCell.RowIndex;
                object obj = dataGridView1.Rows[rowindex].Cells[3].Value;
                write(obj.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InitSP();
            
        }
        static string spCOM = ConfigurationManager.AppSettings[0];
        SerialPort mySerialPort = new SerialPort(spCOM);
        public SerialPort InitSP()
        {
         
            mySerialPort.BaudRate = 115200;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.DataReceived += MySerialPort_DataReceived;
            mySerialPort.DiscardNull = false;
            if (!mySerialPort.IsOpen)
            {
                mySerialPort.Open();
            }
            else
            {
                mySerialPort.Close();
            }
            return mySerialPort;
        }

        public void write(string data)
        {
            if (mySerialPort.IsOpen)
            {
                List<byte> bytelist = ConvertFrom.HexstringToBytesArray(data);
                mySerialPort.Write(bytelist.ToArray(), 0, bytelist.Count);
            }
            else
            {
                MessageBox.Show("串口未打开！");
            }
        }
        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (mySerialPort.IsOpen)
            {
                int tempdatalenth = mySerialPort.BytesToRead;
                byte[] tempbytes = new byte[tempdatalenth];
                mySerialPort.Read(tempbytes, 0, tempdatalenth);
                textBox3.Invoke(new Action(() => { textBox3.Text += ConvertFrom.ToHexString(tempbytes);textBox3.Text += "\r\n"; }));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }
    }
}
