using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation.addon
{
    class StaticData : Addon
    {
        public StaticData(dynamic data)
        {
            if (data.type())
            {
                SetType(data.type);
            }
            if (data.length() && data.length.GetType().Name == "Double")
            {
                SetLength(data.length);
            }
            if (data.encoding())
            {
                SetEncoder(data.encoding);
            }
            if (data.endian())
            {
                SetEndian(data.endian);
            }
            if (data.value())
            {
                if (data.value.GetType().Name == "String" && type == "string")
                {
                    SetValue((string)data.value);
                }
                else if (data.value.GetType().Name == "Double")
                {
                    if (type == "int")
                    {
                        SetValue((int)data.value);
                    }
                    else if (type == "short")
                    {
                        SetValue((short)data.value);
                    }
                    else if (type == "long")
                    {
                        SetValue((long)data.value);
                    }
                    else if (type == "double")
                    {
                        SetValue((double)data.value);
                    }
                    else if (type == "float")
                    {
                        SetValue((float)data.value);
                    }
                    else if (type== "uint")
                    {
                        SetValue((uint)data.value);
                    }
                    else if (type == "ulong")
                    {
                        SetValue((ulong)data.value);
                    }
                    else if (type == "ushort")
                    {
                        SetValue((ushort)data.value);
                    }
                    else
                    {
                        throw new config.ConfigErrorException("Config file staticdata unmatch value and type.");
                    }
                }
                else if (data.value.GetType().Name == "JSON" && data.value.IsArray)
                {
                    try
                    {
                        SetValue((byte[])data.value);
                    }
                    catch(Exception ex)
                    {
                        if (ex is OverflowException)
                        {
                            throw new config.ConfigErrorException("Config file static addon value is not bytes.");
                        }
                    }
                }
            }
        }

        public override byte[] GetValue(byte[] stream)
        {
            return value;
        }

        public override int GetLength(byte[] stream)
        {
            return length;
        }
    }
}
