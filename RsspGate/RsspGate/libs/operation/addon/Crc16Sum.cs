using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation.addon
{
    class Crc16Sum : Addon
    {
        private ushort _polynom;
        private int _start;
        private int _end;
        private crc16 _polyobj;
        public Crc16Sum(dynamic data)
        {
            SetType("ushort");
            if (data.polynom() && data.polynom.GetType().Name == "String")
            {
                var p = ((String)data.polynom).Trim().ToLower();
                var r = new System.Text.RegularExpressions.Regex("^0x[a-z0-9]{1,4}|[0-9]*").Match(p);
                if (r.Success)
                {
                    try
                    {
                        _polynom = Convert.ToUInt16(p, 16);
                    }
                    catch(Exception ex)
                    {
                        try
                        {
                            _polynom = Convert.ToUInt16(p);
                        }
                        catch(Exception e)
                        {
                            throw new config.ConfigErrorException("polynom error.");
                        }
                    }
                    if (!Runtime.CRC16Dict.ContainsKey(_polynom))
                    {
                        Runtime.CRC16Dict.Add(_polynom, new crc16(_polynom));
                    }
                    _polyobj = Runtime.CRC16Dict[_polynom];
                }
            }
            else

            if (data.start())
            {
                if (data.start.GetType().Name == "Double")
                {
                    _start = data.start;
                }
                if (data.start.GetType().Name == "String")
                {
                    var ps = ((String)data.start).Trim().ToLower();
                    if (ps == "start" || ps == "begin")
                    {
                        _start = 0;
                    }
                    else
                    {
                        throw new config.ConfigErrorException("crc16 start key words unknown.");
                    }
                }
            }
            else
            {
                _start = 0;
            }

            if (data.end())
            {
                if (data.end.GetType().Name == "Double")
                {
                    _end = data.end;
                }
                if (data.end.GetType().Name == "String")
                {
                    var pe = ((String)data.end).Trim().ToLower();
                    if (pe == "end")
                    {
                        _end = -1;
                    }
                    else
                    {
                        throw new config.ConfigErrorException("crc16 end key words unknown.");
                    }
                }
            }
            else
            {
                _end = -1;
            }
            if (data.endian())
            {
                SetEndian(data.endian);
            }
        }
        public override int GetLength(byte[] stream)
        {
            return length;
        }

        public override byte[] GetValue(byte[] stream)
        {
            ushort sum = _polyobj.ComputeChecksum(stream);
            SetValue(sum);
            return value;
        }
    }
}
