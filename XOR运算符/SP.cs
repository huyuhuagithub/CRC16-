using BaseModule.Helper.ConvertFrom;
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
    public class SP
    {
        SerialPort mySerialPort = new SerialPort("COM6");
        public List<string> strlist = new List<string>();
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
            return mySerialPort;
        }

        public void write(string data)
        {
            //strlist = null;
            List<byte> bytelist = ConvertFrom.HexstringToBytesArray(data);
            mySerialPort.Write(bytelist.ToArray(), 0, bytelist.Count);
        }
        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (mySerialPort.IsOpen)
            {
                int tempdatalenth = mySerialPort.BytesToRead;
                byte[] tempbytes = new byte[tempdatalenth];
                mySerialPort.Read(tempbytes, 0, tempdatalenth);
                strlist.Add(ConvertFrom.ToHexString(tempbytes));
            }
        }
    }
}
