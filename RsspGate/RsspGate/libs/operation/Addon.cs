using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation
{
    public abstract class Addon
    {
        private static string[] AvaliableType = { "string", "int", "byte", "long", "short", "double", "float", "uint", "ulong", "ushort", "array" };
        private static Dictionary<string, Encoding> AvaliableEncoder = new Dictionary<string, Encoding>()
        {
            { "ascii", Encoding.ASCII },
            { "utf8", Encoding.UTF8 },
            { "unicode", Encoding.Unicode },
            { "utf32", Encoding.UTF32 }
        };
        private static Dictionary<string, int> TypeLength = new Dictionary<string, int>()
        {
            { "int", 4 },
            { "uint", 4 },
            { "short", 2 },
            { "ushort", 2 },
            { "long", 8 },
            { "ulong", 8 }
        };

        protected static Dictionary<string, Type> TypeTable = new Dictionary<string, Type>()
        {
            { "ushort", typeof(ushort) },
            { "string", typeof(string) },
            { "uint", typeof(uint) },
            { "ulong", typeof(ulong) }
        };


        protected byte[] value;
        protected Encoding encoder = AvaliableEncoder["ascii"];
        protected string type;
        protected int length;
        protected bool defineLength = false;
        protected bool IsLittleEndian = BitConverter.IsLittleEndian;


        protected void SetType(string type)
        {
            type = type.Trim().ToLower();
            if (AvaliableType.Contains(type))
            {
                this.type = type;
            }
            if (TypeLength.ContainsKey(type))
            {
                SetLength(TypeLength[type]);
            }
        }

        protected void SetLength(Double length)
        {
            this.length = (int)length;
            defineLength = true;
        }

        protected void SetEncoder(string encode)
        {
            encode = encode.Trim().ToLower();
            if (AvaliableEncoder.Keys.Contains(encode))
            {
                this.encoder = AvaliableEncoder[encode];
            }
        }

        protected void SetEndian(string endian)
        {
            endian = endian.Trim().ToLower();
            if (endian == "little")
            {
                this.IsLittleEndian = true;
            }
            if (endian == "big")
            {
                this.IsLittleEndian = false;
            }
        }

        protected void SetValue(string data)
        {
            if (type == "string")
            {
                this.value = encoder.GetBytes(data);
            }
            else
            {
                throw new config.ConfigErrorException("");
            }
            SetLengthViaValue();
        }

        protected void SetValue(float data)
        {
            if (type == "float")
            {
                this.value = BitConverter.GetBytes(data);
            }
            else
            {
                throw new config.ConfigErrorException("");
            }
            ReverseValue();
            SetLengthViaValue();
        }

        protected void SetValue(double data)
        {
            if (type == "double")
            {
                this.value = BitConverter.GetBytes(data);
            }
            else
            {
                throw new config.ConfigErrorException("");
            }
            ReverseValue();
            SetLengthViaValue();
        }

        protected void SetValue(int data)
        {
            if (type == "int")
            {
                this.value = BitConverter.GetBytes(data);
            }
            else
            {
                throw new config.ConfigErrorException("");
            }
            ReverseValue();
            SetLengthViaValue();
        }

        protected void SetValue(short data)
        {
            if (type == "short")
            {
                this.value = BitConverter.GetBytes(data);
            }
            else
            {
                throw new config.ConfigErrorException("");
            }
            ReverseValue();
            SetLengthViaValue();
        }

        protected void SetValue(long data)
        {
            if (type == "long")
            {
                this.value = BitConverter.GetBytes(data);
            }
            else
            {
                throw new config.ConfigErrorException("");
            }
            ReverseValue();
            SetLengthViaValue();
        }

        protected void SetValue(uint data)
        {
            if (type == "uint")
            {
                this.value = BitConverter.GetBytes(data);
            }
            else
            {
                throw new config.ConfigErrorException("");
            }
            ReverseValue();
            SetLengthViaValue();
        }
        protected void SetValue(ushort data)
        {
            if (type == "ushort")
            {
                this.value = BitConverter.GetBytes(data);
            }
            else
            {
                throw new config.ConfigErrorException("");
            }
            ReverseValue();
            SetLengthViaValue();
        }
        protected void SetValue(ulong data)
        {
            if (type == "ulong")
            {
                this.value = BitConverter.GetBytes(data);
            }
            else
            {
                throw new config.ConfigErrorException("");
            }
            ReverseValue();
            SetLengthViaValue();
        }


        protected void SetValue(byte[] data)
        {
            if (type == "array")
            {
                this.value = data;
            }
            else
            {
                throw new config.ConfigErrorException("");
            }
            ReverseValue();
            SetLengthViaValue();
        }

        private void ReverseValue()
        {
            if (BitConverter.IsLittleEndian != IsLittleEndian)
            {
                this.value = this.value.Reverse().ToArray();
            }
        }

        private void SetLengthViaValue()
        {
            if (!defineLength)
            {
                this.length = this.value.Length;
            }
        }
        public abstract byte[] GetValue();
        public abstract int GetLength();
    }
}
