using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs
{
    class AsyncUdpClient : IDisposable
    {
        #region Fields
        UdpClient sender;
        #endregion

        #region Ctors
        public AsyncUdpClient()
        {
            sender = new UdpClient();
        }
        #endregion

        #region Events
        public event EventHandler<UdpDatagramSendedEventArgs<byte[]>> DatagramSended;
        #endregion

        #region send
        public void Send(IPEndPoint ipendpoint, byte[] datagram)
        {
            if (ipendpoint == null)
                throw new ArgumentNullException("ipendpoint");

            if (datagram == null)
                throw new ArgumentNullException("datagram");
            
            sender.BeginSend(datagram, datagram.Length, ipendpoint, new AsyncCallback(HandleDatagramSendFinish), new UdpDatagramSendedEventArgs<byte[]>(datagram, ipendpoint));
        }
        
        private void HandleDatagramSendFinish(IAsyncResult ar)
        {
            if (ar.IsCompleted)
            {
                UdpDatagramSendedEventArgs<byte[]> args = ar.AsyncState as UdpDatagramSendedEventArgs<byte[]>;
                sender.EndSend(ar);
                RaiseDatagramSended(args);
            }
        }
        private void RaiseDatagramSended(UdpDatagramSendedEventArgs<byte[]> args)
        {
            if (DatagramSended != null)
            {
                DatagramSended(this, args);
            }
        }
        #endregion
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
