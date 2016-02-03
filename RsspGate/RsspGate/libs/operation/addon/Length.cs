using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation.addon
{
    class Length : Addon
    {
        private int _initial = 0;
        private Type type = TypeTable["uint"];
        private static string[] AvaliableType = { "uint", "ulong", "ushort", "byte" };


        public Length(dynamic data)
        {
            if (data.type())
            {
                string t = ((string)data.type).Trim().ToLower();
                if (AvaliableType.Contains(t))
                {
                    SetType(data.type);
                    this.type = TypeTable[data.type];
                }
                else
                {
                    throw new config.ConfigErrorException("Length only avaliable uint, ulong, ushort, byte type.");
                }
            }
            else
            {
                SetType("uint");
            }
            if (data.initial() && data.initial.GetType().Name == "Double")
            {
                this._initial = data.initial;
            }
            else
            {
                this._initial = 0;
            }
            if (data.endian())
            {
                SetEndian(data.endian);
            }

            RsspGate.libs.timer.Instance.Regist();
        }
        public override int GetLength(byte[] stream)
        {
            return this.length;
        }

        public override byte[] GetValue(byte[] stream)
        {
            var c = stream.Length + _initial;
            switch (this.type.Name)
            {
                case "Byte":
                    SetValue((Byte)c);
                    break;
                case "UInt32":
                    SetValue((UInt32)c);
                    break;
                case "UInt16":
                    SetValue((UInt16)c);
                    break;
                case "UInt64":
                    SetValue((UInt64)c);
                    break;
                default:
                    SetValue((UInt32)c);
                    break;
            }
            return value;
        }
    }
}
