using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.config
{
    [DataContract]
    public class inter
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
}
