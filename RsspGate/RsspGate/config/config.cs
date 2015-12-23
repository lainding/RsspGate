using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RsspGate.libs;

namespace RsspGate.config
{
    [DataContract]
    public class cinterface
    {
        [DataMember]
        public int index
        {
            get; set;
        }
        [DataMember]
        public string name
        {
            get; set;
        }

        [DataMember]
        public string ip
        {
            get; set;
        }

        [DataMember(IsRequired = true)]
        public int port
        {
            get; set;
        }
    }
    [DataContract]
    public class config
    {
        [DataMember]
        public List<cinterface> interfaces
        {
            get; set;
        }

        public static config ReadConfig(string path)
        {
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                config cfg = null;
                try
                {
                    cfg = JSON.parse<config>(reader.ReadToEnd());
                }
                catch(Exception e)
                {
                    ExceptionHandler.Handle(e);
                }
                return cfg;
            }
            else
            {
                throw new FileNotFoundException(path + " not found.");
            }
        }
    }
}
