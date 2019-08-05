using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOR运算符
{
    class SP
    {
        SerialPort sp = new SerialPort()
        {
            //PortName = "COM9",
            BaudRate = 115200,
            Parity = Parity.None,
            DataBits = 8,
            StopBits = StopBits.One,
            ReadBufferSize = 21474836,
            //NewLine = "\r"
        };
        public void testString(string portname, string command)
        {
            data(Setcommand, portname, command);
        }

        public string testhexString(string portname, string command)
        {
            string sffa = null;
            sffa = data(Sethexcommand, portname, command);
            return sffa;
        }
        //public void testhexString(string portname, string command)
        //{
        //    data(Sethexcommand, portname, command);
        //}
        //public void testhexString(string portname, string command)
        //{
        //    data(Setcommand, portname, command);
        //}
        private string data(Action<string> action, string portname, string command)
        {
            string sf = null;
            try
            {
                sp.PortName = portname;
                sp.Open();
                action(command);
                sf = ReadHexByte();
                sp.Close();
            }
            catch (Exception e)
            {
                sp.Close();
                //throw new InvalidOperationException(e);
                //toolStripStatusLabel1.Text = e.Message;
            }
            if (sf != null)
            {
                return sf;
            }
            return "";
        }

        private void data1(Func<string> action, string portname, string command)
        {
            try
            {
                sp.PortName = portname;
                //TaskFactory tf = new TaskFactory();
                //tf.StartNew(() =>
                //{
                sp.Open();
                action();
                ReadHexByte();
                sp.Close();
                //}
                //);
            }
            catch (Exception e)
            {
                //throw new InvalidOperationException(e);
                //toolStripStatusLabel1.Text = e.Message;
            }

        }
        private void Setcommand(string command)
        {
            try
            {
                sp.WriteLine(command);
                //this.textBox1.Invoke(new Action(() => { this.textBox1.Text = command; }));
            }
            catch (System.InvalidOperationException e)
            {

                //toolStripStatusLabel1.Text = e.Message;
            }

        }

        private void Sethexcommand(string command)
        {
            try
            {
                byte[] combyte = strToHexByte1(command);
                sp.Write(combyte, 0, combyte.Length);
                //this.textBox1.Invoke(new Action(() => { this.textBox1.Text = command; }));
            }
            catch (System.InvalidOperationException e)
            {

                //toolStripStatusLabel1.Text = e.Message;
            }

        }
        private void SetHEXcommand(string hexcommand)
        {
            try
            {
                char[] trimChars = new char[] { '%', ' ', '$', '&' };
                hexcommand = hexcommand.Trim(trimChars);
                //List<int> af = new List<int>();
                string[] arraychar = hexcommand.Split(' ');
                byte[] bytes = new byte[arraychar.Length];
                for (int i = 0; i < arraychar.Length; i++)
                {
                    bytes[i] = Convert.ToByte(arraychar[i], 0x10);
                }

                sp.WriteLine(hexcommand);

                //this.textBox1.Invoke(new Action(() => { this.textBox1.Text = command; }));
            }
            catch (System.InvalidOperationException e)
            {

                //toolStripStatusLabel1.Text = e.Message;
            }

        }

        public string HexStringToString(string HexString)
        {
            string str2 = string.Empty;
            Encoding encod;
            encod = Encoding.ASCII;
            try
            {
                char[] trimChars = new char[] { '%', ' ', '$', '&' };
                HexString = HexString.Trim(trimChars);
                string[] strArray = HexString.Split(trimChars);
                byte[] bytes = new byte[strArray.Length];
                for (int i = 0; i < strArray.Length; i++)
                {
                    bytes[i] = Convert.ToByte(strArray[i], 0x10);
                }
                str2 = encod.GetString(bytes);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "转换失败", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return str2;
        }


        public string HexToStr(string mHex) // 返回十六进制代表的字符串
        {
            mHex = mHex.Replace(" ", "");
            if (mHex.Length <= 0) return "";
            byte[] vBytes = new byte[mHex.Length / 2];
            for (int i = 0; i < mHex.Length; i += 2)
                if (!byte.TryParse(mHex.Substring(i, 2), NumberStyles.HexNumber, null, out vBytes[i / 2]))
                    vBytes[i / 2] = 0;
            return ASCIIEncoding.Default.GetString(vBytes);
        } /* HexToStr */


        public byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        private byte[] strToHexByte1(string hexString)
        {
            List<byte> af = new List<byte>();
            string[] arraychar = hexString.Split(' ');
            for (int i = 0; i < arraychar.Length; i++)
            {
                af.Add(Convert.ToByte(arraychar[i], 16));
            }
            return af.ToArray();
        }

        private string ReadHexByte()
        {
            string datastring = sp.ReadExisting();
            return datastring;
        }
    }
}
