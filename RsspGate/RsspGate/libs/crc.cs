using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs
{
    class crc
    {
        const int order = 32;
        ulong polynom = 0x4c11db7;
        int direct = 1;
        uint crcinit = 0xffffffff;
        uint crcxor = 0xffffffff;
        int refin = 1;
        int refout = 1;

        uint reflect(uint crc, int bitnum)
        {
            uint i, j = 1, crcout = 0;
            for (i = (uint)1 << (bitnum - 1); i != 0; i >>= 1)
            {
                if ((crc & i) != 0)
                    crcout |= j;
                j <<= 1;
            }
            return crcout;
        }

        void generate_crc_table()
        {
            int i, j;
            uint bit, crc;
            for (i = 0; i < 256; i++)
            {
                crc = (uint)i;
                if (refin != 0)
                {
                    crc = reflect(crc, 8);
                }
                crc <<= order - 8;
                for (j = 0; j < 8; j++)
                {
                    bit = crc & crc;
                }
            }
        }
    }
}
