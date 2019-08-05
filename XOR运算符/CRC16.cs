using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOR运算符
{
    public class CRC16
    {
        void InvertUint8(ref byte dBuf, ref byte srcBuf)
        {
            int i;
            // byte[] tmp = new byte[4] { 0, 0, 0, 0};
            // tmp[0] = 0;

            byte tmp = 0;
            for (i = 0; i < 8; i++)
            {
                if (0 != (srcBuf & (1 << i)))
                    tmp |= (byte)(1 << (7 - i));
            }
            dBuf = tmp;
        }

        void InvertUint16(ref ushort dBuf, ref ushort srcBuf)
        {
            int i;
            //ushort[] tmp = new ushort[4]{ 0, 0, 0, 0 };
            ushort tmp = 0;
            // tmp[0] = 0;
            for (i = 0; i < 16; i++)
            {
                if (0 != (srcBuf & (1 << i)))
                    tmp |= (ushort)(1 << (15 - i));
            }
            dBuf = tmp;
        }

         public uint calc_crc16(byte[] data, int DataLen)
        {
            ushort wCRCin = 0xFFFF;
            ushort wCPoly = 0x8005;
            byte wChar = 0;
            int i = 0;

            if (data == null)
            {
                return (wCRCin);
            }

            while (0 != DataLen--)
            {
                //               Console.Write("0x{0:X} ", data[i]);
                wChar = data[i++];
                InvertUint8(ref wChar, ref wChar);
                wCRCin ^= (ushort)(wChar << 8);
                for (int j = 0; j < 8; j++)
                {
                    if (0 != (wCRCin & 0x8000))
                        wCRCin = (ushort)((wCRCin << 1) ^ wCPoly);
                    else
                        wCRCin = (ushort)(wCRCin << 1);
                }
            }
            InvertUint16(ref wCRCin, ref wCRCin);
            return (wCRCin);
        }
    }
}
