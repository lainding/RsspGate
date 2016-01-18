using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation.addon
{
    class StaticData : IGetValue
    {
        private byte[] value;
        private Encoding encoder = avaliableencoder["ascii"];
        private string type = "string";
        private static string[] avaliabletype = { "string", "int", "byte", "long", "short", "double", "float", "uint", "ulong", "ushort" };
        private static Dictionary<string, Encoding> avaliableencoder = new Dictionary<string, Encoding>() {
            { "ascii", Encoding.ASCII },
            { "utf8", Encoding.UTF8 },
            { "unicode", Encoding.Unicode },
            { "utf32", Encoding.UTF32 }
        };
        private bool littleendian = BitConverter.IsLittleEndian;

        public StaticData(dynamic data)
        {
            if (data.type())
            {
                string t = data.type;
                if (avaliabletype.Contains(t.Trim().ToLower()))
                {
                    type = t.Trim().ToLower();
                }
            }
            if (data.encoding())
            {
                string e = data.encoding;
                if (avaliableencoder.Keys.Contains(e.Trim().ToLower()))
                {
                    encoder = avaliableencoder[e.Trim().ToLower()];
                }
            }
            if (data.endian())
            {
                string en = data.endian;
                if (en.Trim().ToLower() == "little")
                {
                    littleendian = true;
                }
                if (en.Trim().ToLower() == "big")
                {
                    littleendian = false;
                }
            }
            if (data.value())
            {
                if (type == "string" && data.value.GetType().Name == "String")
                {
                    value = encoder.GetBytes(data.value);
                }
                if (data.value.GetType().Name == "Double")
                {
                    if (type == "int")
                    {
                        value = BitConverter.GetBytes((int)data.value);
                    }
                    else if (type == "short")
                    {
                        value = BitConverter.GetBytes((short)data.value);
                    }
                    else if (type == "long")
                    {
                        value = BitConverter.GetBytes((long)data.value);
                    }
                    else if (type == "double")
                    {
                        value = BitConverter.GetBytes((double)data.value);
                    }
                    else if (type == "float")
                    {
                        value = BitConverter.GetBytes((float)data.value);
                    }
                    else if (type== "uint")
                    {
                        value = BitConverter.GetBytes((uint)data.value);
                    }
                    else if (type == "ulong")
                    {
                        value = BitConverter.GetBytes((ulong)data.value);
                    }
                    else if (type == "ushort")
                    {
                        value = BitConverter.GetBytes((ushort)data.value);
                    }
                    else
                    {
                        throw new config.ConfigErrorException("Config file staticdata unmatch value and type.");
                    }
                    if (BitConverter.IsLittleEndian != littleendian)
                    {
                        value = value.Reverse().ToArray();
                    }
                }
                //BitConverter.GetBytes()
            }
        }

        public byte[] GetValue()
        {
            return value;
        }
    }
}
