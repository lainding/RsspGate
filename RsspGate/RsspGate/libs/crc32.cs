using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs
{
    class crc32
    {
        uint polynomial = 0xA001;
        uint[] table = new uint[256];

        public uint ComputeChecksum(byte[] bytes)
        {
            uint crc = 0;
            for (int i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(crc ^ bytes[i]);
                crc = (uint)((crc >> 8) ^ table[index]);
            }
            return crc;
        }

        public byte[] ComputeChecksumBytes(byte[] bytes)
        {
            uint crc = ComputeChecksum(bytes);
            return BitConverter.GetBytes(crc);
        }

        public crc32()
            : this(0xA001)
        {

        }
        public crc32(uint polynomial)
        {
            this.polynomial = polynomial;
            uint value;
            uint temp;
            for (uint i = 0; i < table.Length; ++i)
            {
                value = 0;
                temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (uint)((value >> 1) ^ polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                table[i] = value;
            }
        }
    }
}
