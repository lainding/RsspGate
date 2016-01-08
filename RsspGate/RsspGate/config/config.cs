using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using RsspGate.libs;

namespace RsspGate.config
{
    public class gate 
    {
        public string name
        {
            get; set;
        }

        public string ip
        {
            get; set;
        }

        public int port
        {
            get; set;
        }

        public string type
        {
            get; set;
        }

        public gate()
        {
            this.ip = "0.0.0.0";
        }
    }

    public class device
    {
        public string name
        {
            get; set;
        }

        public string ip
        {
            get; set;
        }

        public int port
        {
            get; set;
        }

        public device()
        {
            ip = "127.0.0.1";
        }

    }

    public class operation
    {
        public string name
        {
            get; set;
        }

        public dynamic parameters
        {
            get; set;
        }
    }

    public abstract class parameter
    {
    }
    
    public class route
    {
        public string from
        {
            get; set;
        }

        public string through
        {
            get; set;
        }

        public string by
        {
            get; set;
        }

        public string to
        {
            get; set;
        }

        public List<operation> process
        {
            get; set;
        }
    }

    public class config
    {
        public List<gate> gates
        {
            get; set;
        }
        public List<device> devices
        {
            get; set;
        }
        public List<route> routes
        {
            get; set;
        }

        public static dynamic ReadConfig(string path)
        {
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                dynamic cfg = null;
                try
                {
                    var cstr = reader.ReadToEnd();
                    //var c = JSON.Parse(cstr);
                    cfg = JSON.Parse(cstr);
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
