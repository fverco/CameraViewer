using System;
using System.Text;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace CameraViewer.Types
{
    /// <summary>
    /// Class used for network related actions.
    /// </summary>
    internal class Network
    {
        /// <summary>
        /// Tests the provided IP address and port number.
        /// </summary>
        /// <param name="ipAddress">The IPv4 address.</param>
        /// <param name="portNumber">The port number.</param>
        /// <returns>A tuple with two Booleans.
        /// <para>ipSuccess is true if the IP address responds to pings.</para>
        /// <para>portSuccess is true if a TCP connection could be established with that port number.</para>
        /// </returns>
        public static (bool ipSuccess, bool portSuccess) TestConnection(string ipAddress, int portNumber)
        {
            // Check if the IP address and port number is provided.
            if (ipAddress != null && ipAddress.Length > 6)
            {
                var pingSender = new Ping();
                byte[] buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                var timeout = 120;
                var pingReply = pingSender.Send(ipAddress, timeout, buffer);

                // Verify that the IP address is valid.
                if (pingReply.Status == IPStatus.Success)
                {
                    var tcpClient = new TcpClient();

                    // Verify that the device's port is open.
                    try
                    {
                        tcpClient.Connect(ipAddress, portNumber);
                        return (ipSuccess: true, portSuccess: true);
                    }
                    catch (Exception)
                    {
                        return (ipSuccess: true, portSuccess: false); ;
                    }
                }
            }

            return (ipSuccess: false, portSuccess: false);
        }

    }
}

