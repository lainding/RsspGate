using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs
{
    class Rssp1
    {
    }

    class RSD : DataFrame
    {
        public byte ProtocolInteractionsCategory;
        public byte FrameType;
        public UInt16 SourceAddress;
        public UInt16 DestinationAddress;
        public UInt32 LocalCycleCounter;
        public UInt16 DataLength;


        public override void DeSerialize()
        {
            throw new NotImplementedException();
        }

        public override void Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
