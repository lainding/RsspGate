using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;

namespace RsspGate.libs.operation
{
    class OperationFactory
    {
        public static Operation GetOperation(string name)
        {
            switch(name)
            {
                case "direct":
                    return new direct();
                case "insert":
                    return new insert();
                case "remove":
                    return new remove();
                case "reverse":
                    return new reverse();
            }
            return null;
        }

        private static dynamic SpecialInsert(dynamic param)
        {
            if (!param.bytes())
            {
                throw new ConfigErrorException("Config routes insert method miss bytes.");
            }
            else
            {
                param = _Position(_Length(_Bytes(param)));
            }
            return param;
        }

        private static dynamic SpecialRemove(dynamic param)
        {
            if (!param.length())
            {
                throw new ConfigErrorException("Config routes remove method miss length.");
            }
            else
            {
                param = _Position(param);
            }
            return param;
        }

        private static dynamic SpecialReverse(dynamic param)
        {
            return param;
        }

        private static dynamic _Position(dynamic param)
        {
            if (!param.position())
            {
                param.position = 0;
            }
            else
            {
                if (param.position.GetType().Name == "String")
                {
                    if (((string)param.position).ToLower().Trim() == "start" || ((string)param.position).ToLower().Trim() == "begin")
                    {
                        param.position = 0;
                    }
                    else if (((string)param.position).ToLower().Trim() == "end")
                    {
                        param.position = -1;
                    }
                }
            }
            return param;
        }

        private static dynamic _Length(dynamic param)
        {
            if (!param.length() && param.bytes())
            {
                byte[] b = (byte[])param.bytes;
                param.length = b.Count();
            }
            return param;
        }

        private static dynamic _Bytes(dynamic param)
        {
            if (param.bytes())
            {
                if (param.bytes.GetType().Name == "String")
                {
                    param.bytes = Encoding.Default.GetBytes((string)param.bytes);
                }
                if (param.bytes.GetType().Name == "JSON" && param.bytes.IsArray)
                {
                    param.bytes = (byte[])param.bytes;
                }

            }
            return param;
        }

        private static dynamic _Begin(dynamic param)
        {
            if (!param.begin())
            {
                
            }
            return param;
        }
        public static parameter GetParameter(string name, dynamic param)
        {
            switch(name)
            {
                case "direct":
                    return (config.direct)param;
                case "insert":
                    return (config.insert)SpecialInsert(param);
                case "remove":
                    return (config.remove)SpecialRemove(param);
                case "reverse":
                    return (config.reverse)SpecialReverse(param);
            }
            return null;
        }
    }
}
