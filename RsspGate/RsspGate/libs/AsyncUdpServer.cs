using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RsspGate.libs
{
    class AsyncUdpServer : Gate, IDisposable
    {
        #region Fields

        private UdpClient listener;
        private UdpClient sender;
        private bool disposed = false;

        #endregion

        #region Ctors

        /// <summary>
        /// 异步UDP服务器
        /// </summary>
        /// <param name="listenPort">监听的端口</param>
        public AsyncUdpServer(int listenPort)
            : this(IPAddress.Any, listenPort)
        {
        }

        /// <summary>
        /// 异步UDP服务器
        /// </summary>
        /// <param name="localEP">监听的终结点</param>
        public AsyncUdpServer(IPEndPoint localEP)
            : this(localEP.Address, localEP.Port)
        {
        }

        /// <summary>
        /// 异步UDP服务器
        /// </summary>
        /// <param name="localIPAddress">监听的IP地址</param>
        /// <param name="listenPort">监听的端口</param>
        public AsyncUdpServer(IPAddress localIPAddress, int listenPort)
        {
            Address = localIPAddress;
            Port = listenPort;
            this.Encoding = Encoding.Default;
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// 服务器是否正在运行
        /// </summary>
        public bool IsRunning { get; private set; }
        /// <summary>
        /// 监听的IP地址
        /// </summary>
        public IPAddress Address { get; private set; }
        /// <summary>
        /// 监听的端口
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// 通信使用的编码
        /// </summary>
        public Encoding Encoding { get; set; }

        #endregion

        #region Server

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <returns>异步UDP服务器</returns>
        public override Gate Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                try
                {
                    listener = new UdpClient(new IPEndPoint(this.Address, Port));
                    sender = new UdpClient();
                    listener.Client.ReceiveBufferSize = 1024 * 1024 * 16;
                    //listener.AllowNatTraversal(true);
                    listener.BeginReceive(new AsyncCallback(HandleUdpReceived), listener);
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
            return this;
        }


        /// <summary>
        /// 停止服务器
        /// </summary>
        /// <returns>异步UDP服务器</returns>
        public override Gate Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                listener.Close();
                sender.Close();
            }
            return this;
        }

        #endregion

        #region Receive
        private void HandleUdpReceived(IAsyncResult ar)
        {
            if (IsRunning)
            {
                if (ar.IsCompleted)
                {
                    UdpClient listener = ar.AsyncState as UdpClient;
                    IPEndPoint endPoint = null;
                    try
                    {
                        byte[] receiveBytes = listener.EndReceive(ar, ref endPoint);
                        RaiseDatagramReceived(this, receiveBytes, endPoint);
                        //RasiePlaintextReceived(listener, receiveBytes, endPoint);
                        //listener.BeginReceive(new AsyncCallback(HandleUdpReceived), listener);
                        listener.BeginReceive(new AsyncCallback(HandleUdpReceived), listener);
                    }
                    catch(Exception ex)
                    {
                        if (!(ex is SocketException))
                        {
                            throw ex;
                        }
                    }
                }
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// 接收到数据报文事件
        /// </summary>
        //public event EventHandler<DatagramReceivedEventArgs<byte[]>> DatagramReceived;
        /// <summary>
        /// 接收到数据报文明文事件
        /// </summary>
        public event EventHandler<DatagramReceivedEventArgs<string>> PlaintextReceived;
        /// <summary>
        /// 发送数据报文事件完毕
        /// </summary>
        public event EventHandler<DatagramSendedEventArgs<byte[]>> DatagramSended;

        private void RaiseDatagramSended(DatagramSendedEventArgs<byte[]> args)
        {
            if (DatagramSended != null)
            {
                DatagramSended(this, args);
            }
        }

        //private void RaiseDatagramReceived(UdpClient sender, byte[] datagram, IPEndPoint endpoint)
        //{
        //    if (DatagramReceived != null)
        //    {
        //        DatagramReceived(sender, new DatagramReceivedEventArgs<byte[]>(datagram, endpoint));
        //    }
        //}

        private void RasiePlaintextReceived(UdpClient sender, byte[] datagram, IPEndPoint endpoint)
        {
            if (PlaintextReceived != null)
            {
                string plaintext = this.Encoding.GetString(datagram);
                PlaintextReceived(sender, new DatagramReceivedEventArgs<string>(plaintext, endpoint));
            }
        }
        #endregion

        #region Send

        protected void Send(IPAddress ipaddress, int port, byte[] datagram)
        {
            Send(new IPEndPoint(ipaddress, port), datagram);
        }

        /// <summary>
        /// 发送报文至指定的客户端
        /// </summary>
        /// <param name="ipendpoint">对端地址</param>
        /// <param name="datagram">报文</param>
        protected void Send(IPEndPoint ipendpoint, byte[] datagram)
        {
            if (!IsRunning)
                throw new InvalidProgramException("This UDP server has not been started.");

            if (ipendpoint == null)
                throw new ArgumentNullException("ipendpoint");

            if (datagram == null)
                throw new ArgumentNullException("datagram");

            //listener.Send(datagram, datagram.Length, ipendpoint);
            
            sender.BeginSend(datagram, datagram.Length, ipendpoint, new AsyncCallback(HandleDatagramSendFinish), new DatagramSendedEventArgs<byte[]>(datagram, ipendpoint));
        }

        private void HandleDatagramSendFinish(IAsyncResult ar)
        {
            if (IsRunning)
            {
                if (ar.IsCompleted)
                {
                    DatagramSendedEventArgs<byte[]> args = ar.AsyncState as DatagramSendedEventArgs<byte[]>;
                    sender.EndSend(ar);
                    RaiseDatagramSended(args);
                }
            }
        }

        /// <summary>
        /// 发送报文至指定的客户端
        /// </summary>
        /// <param name="ipendpoint">对端地址</param>
        /// <param name="datagram">报文</param>
        protected void Send(IPEndPoint ipendpoint, string datagram)
        {
            Send(ipendpoint, this.Encoding.GetBytes(datagram));
        }

        protected void Send(IPAddress ipaddress, int port, string datagram)
        {
            Send(new IPEndPoint(ipaddress, port), datagram);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        Stop();

                        if (listener != null)
                        {
                            listener = null;
                        }

                        if (sender != null)
                        {
                            sender = null;
                        }
                    }
                    catch (SocketException ex)
                    {
                        ExceptionHandler.Handle(ex);
                    }
                }

                disposed = true;
            }
        }

        public override void Send(Device device, byte[] data)
        {
            Send(device.IP, device.Port, data);
        }
        #endregion

    }
}
