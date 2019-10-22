using BaseModule.Helper.ConvertFrom;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOR运算符
{
    public class SP
    {
        static string spCOM = ConfigurationManager.AppSettings[0];
        static int spBaudRate = int.Parse(ConfigurationManager.AppSettings[1]);
        SerialPort mySerialPort = new SerialPort(spCOM);
        List<string> strlist = new List<string>();
        byte[] tempbytes;
        int t;
        public event Action<string> showMessageEvent;

        public bool InitSP()
        {
            if (mySerialPort.IsOpen)
            {
                mySerialPort.Close();
                return false;
            }
            mySerialPort.PortName = spCOM;
            mySerialPort.BaudRate = spBaudRate;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.DataReceived += MySerialPort_DataReceived;
            mySerialPort.Open();
            return true;
        }

        public void writesp(string data)
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
        public void closesp()
        {
            if (mySerialPort.IsOpen)
            {
                mySerialPort.Close();
            }

        }

        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (mySerialPort.IsOpen)
            {
                strlist.Clear();
                int tempdatalenth = mySerialPort.BytesToRead;
                tempbytes = new byte[tempdatalenth];
                mySerialPort.Read(tempbytes, 0, tempdatalenth);
                strlist.Add(ConvertFrom.ToHexString(tempbytes));
            }
            if (tempbytes.Length > 0)
            {
                foreach (var item in strlist)
                {
                    showMessageEvent(item);
                }

            }
            else
            {
                return;
            }
        }

    }
}
