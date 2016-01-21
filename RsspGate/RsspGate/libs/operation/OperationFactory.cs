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

        private static Addon GetAddon(string name, dynamic data)
        {
            Addon addon = null;
            switch (name)
            {
                case "static":
                    addon = new addon.StaticData(data);
                    break;
                case "timestamp":
                    addon = new addon.TimeStamp(data);
                    break;
                case "sequence":
                    addon = new addon.Sequence(data);
                    break;
            }
            return addon;
        }


        private static dynamic SpecialInsert(Operation oper, dynamic param)
        {
            if (!param.addon())
            {
                //TODO: show error if not addon part.
            }
            else
            {
                var p = (config.insert)_Position(_Length(param));
                oper.Init(p);

                if (param.addon.function())
                {
                    var aofun = param.addon.function;
                    dynamic aodata = JSON.Parse("{}");
                    if (param.addon.data())
                    {
                        aodata = param.addon.data;
                    }
                    var addon = GetAddon(aofun, aodata);
                    oper.SetValueWidget(addon);
                }
            }
            return param;
        }

        private static dynamic SpecialRemove(Operation oper, dynamic param)
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

        private static dynamic SpecialReverse(Operation oper, dynamic param)
        {
            var p = (config.reverse)_End(_Begin(param));
            oper.Init(p);
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
            if (!param.length())
            {
                param.length = -1;
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

        private static dynamic _AddOn(dynamic param)
        {
            return param;
        }

        private static dynamic _Begin(dynamic param)
        {
            if (!param.begin())
            {
                param.begin = 0;
            }
            else
            {
                if (param.begin.GetType().Name == "String")
                {
                    if(((string)param.begin).Trim().ToLower() == "start" || ((string)param.begin).Trim().ToLower() == "begin")
                    {
                        param.begin = 0;
                    }
                    else if (((string)param.begin).Trim().ToLower() == "end")
                    {
                        param.begin = -1;
                    }
                }
            }
            return param;
        }

        private static dynamic _End(dynamic param)
        {
            if (!param.end())
            {
                param.end = -1;
            }
            else
            {
                if (param.end.GetType().Name == "String")
                {
                    if(((string)param.end).Trim().ToLower() == "start" || ((string)param.end).Trim().ToLower() == "begin")
                    {
                        param.end = 0;
                    }
                    else if (((string)param.end).Trim().ToLower() == "end")
                    {
                        param.end = -1;
                    }
                }
            }
            return param;
        }

        public static void SetParameter(Operation oper, string name, dynamic param)
        {
            switch(name)
            {
                case "insert":
                    SpecialInsert(oper, param);
                    break;
                case "remove":
                    SpecialRemove(oper, param);
                    break;
                case "reverse":
                    SpecialReverse(oper, param);
                    break;
            }
        }

        //public static 
    }
}
