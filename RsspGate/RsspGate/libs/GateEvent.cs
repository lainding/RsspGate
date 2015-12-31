using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RsspGate.libs
{
    /// <summary>
    /// 接收到数据报文事件参数
    /// </summary>
    /// <typeparam name="T">报文类型</typeparam>
    public class DatagramReceivedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 接收到数据报文事件参数
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="datagram">报文</param>
        public DatagramReceivedEventArgs(T datagram, IPEndPoint remoteendpoint)
        {
            Datagram = datagram;
            RemoteEndPoint = remoteendpoint;
        }

        /// <summary>
        /// 报文
        /// </summary>
        public T Datagram { get; private set; }

        public IPEndPoint RemoteEndPoint { get; private set; }
    }

    public class DatagramSendedEventArgs<T> : EventArgs
    {
        public DatagramSendedEventArgs(T datagram, IPEndPoint remoteendpoint)
        {
            Datagram = datagram;
            RemoteEndPoint = remoteendpoint;
        }

        public T Datagram { get; private set; }

        public IPEndPoint RemoteEndPoint { get; private set; }

    }

}
