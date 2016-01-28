using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs
{
    class crc16
    {
        ushort polynomial = 0xA001;
        ushort[] table = new ushort[256];
        bool isHighFirst = true;

        public crc16(ushort polynomial, bool isHighFirst)
        {
            this.polynomial = polynomial;
            generate_crc_table(this.polynomial, table, false);
        }

        public crc16(ushort polynomial)
            : this(polynomial, false)
        {
        }
        private uint reflect(uint reflect, uint ch)
        {
            uint i = 0u;
            uint val = 0u;
            if (ch == 0u)
            {
                val = reflect;
                return val;
            }
            else
            {
                for (i = 1u; i < (ch + 1u); ++i)
                {
                    if ((uint)0u != (reflect & (uint)1))
                    {
                        val |= (1u << (int)(ch - i));
                    }
                    reflect >>= 1;
                }
                return val;
            }
        }

        private void generate_crc_table(uint polynomial, ushort[] pCRCTable, bool isHighFirst)
        {
            int i = 0, j = 0;
            uint crc = 0, POLY = 0;
            if (false == isHighFirst)
            {
                POLY = this.reflect(polynomial, 16u);
                for (i = 0; i < 256; i++)
                {
                    crc = (uint)i & 0x000000ffu;
                    for (j = 0; j < 8; j++)
                    {
                        if (0 != (crc & 0x01))
                        {
                            crc = (crc >> 1) ^ POLY;
                        }
                        else
                        {
                            crc >>= 1;
                        }
                    }
                    pCRCTable[i] = (ushort)crc;
                }
            }
            else
            {
                for (i = 0; i < 256; i++)
                {
                    crc = (uint)(i << 8);
                    for (j = 0; j < 8; j++)
                    {
                        if (0 != (crc & 0x00008000))
                        {
                            crc = (crc << 1) ^ polynomial;
                        }
                        else
                        {
                            crc <<= 1;
                        }
                    }
                    pCRCTable[i] = (ushort)crc;
                }
            }
        }

        public ushort ComputeChecksum(byte[] pBuffer)
        {
            uint i = 0;
            ushort x = 0;
            for (i = 0; i < pBuffer.Length; i++)
            {
                x = (ushort)(table[(byte)(((1U << 8) - 1U) & (x ^ pBuffer[i]))] ^ (ushort)((ushort)(x >> 8) & 0xFFFF));
            }
            return x;
        }

        public byte[] ComputeChecksumBytes(byte[] pBuffer)
        {
            ushort crc = ComputeChecksum(pBuffer);
            return BitConverter.GetBytes(crc);
        }
    }
}
