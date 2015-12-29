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
    public class gate 
    {
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

        [DataMember]
        public string type
        {
            get; set;
        }
    }

    [DataContract]
    public class device
    {
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

        [DataMember]
        public int port
        {
            get; set;
        }
    }

    [DataContract]
    public class route
    {
        [DataMember]
        public string from
        {
            get; set;
        }

        [DataMember]
        public string through
        {
            get; set;
        }

        [DataMember]
        public string by
        {
            get; set;
        }

        [DataMember]
        public string to
        {
            get; set;
        }

        [DataMember]
        public List<string> process
        {
            get; set;
        }
    }

    [DataContract]
    public class config
    {
        [DataMember]
        public List<gate> gates
        {
            get; set;
        }
        [DataMember]
        public List<device> devices
        {
            get; set;
        }
        [DataMember]
        public List<route> routes
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
