using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs
{
    class RSD : DataFrame
    {
        public byte ProtocolInteractionsCategory;   // 协议交互类别
        public byte FrameType;                      // 帧类型
        public ushort SourceAddress;                // 源地址
        public ushort DestinationAddress;           // 目的地址
        public uint LocalCycleCounter;              // 本地周期计数器
        public ushort DataLength;                   // 安全数据大小
        public uint CRCM1;                          // 安全校验通道1
        public uint CRCM2;                          // 安全校验通道2
        public byte[] Data;                         // 应用数据
        public ushort CRC16;                        // CRC16


        public override void DeSerialize()
        {
            throw new NotImplementedException();
        }

        public override void Serialize()
        {
            throw new NotImplementedException();
        }
    }

    class SSE : DataFrame
    {

        public byte ProtocolInteractionsCategory;   // 协议交互类别
        public byte FrameType;                      // 帧类型
        public ushort SourceAddress;                // 源地址
        public ushort DestinationAddress;           // 目的地址
        public uint LocalCycleCounter;              // 本地周期计数器
        public ulong SEQENQ_1;
        public ulong SEQENQ_2;
        public uint CRC16;

        public override void DeSerialize()
        {
            throw new NotImplementedException();
        }

        public override void Serialize()
        {
            throw new NotImplementedException();
        }
    }

    class SSR : DataFrame
    {
        public byte ProtocolInteractionsCategory;   // 协议交互类别
        public byte FrameType;                      // 帧类型
        public ushort SourceAddress;                // 源地址
        public ushort DestinationAddress;           // 目的地址
        public uint LocalCycleCounter;
        public uint RemoteCycleCounter;
        public uint SEQENQ1;
        public uint SEQENQ2;
        public byte DateVersion;
        public ushort CRC16;

        public override void Serialize()
        {
            throw new NotImplementedException();
        }

        public override void DeSerialize()
        {
            throw new NotImplementedException();
        }
    }
}
