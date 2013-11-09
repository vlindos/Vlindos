using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vlindos.Webserver
{
    // http://blogs.msdn.com/b/pfxteam/archive/2011/12/15/10248293.aspx
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class ServerSocket
    {
        public ServerSocket(int port) { _port = port; }
        private Socket _serverSocket;
        private int _port;

        private void SetupServerSocket()
        { // Resolving local machine information 
            IPHostEntry localMachineInfo = Dns.GetHostEntry(Dns.GetHostName());
            // Create the socket, bind it, and start listening 
            IPEndPoint myEndpoint = new IPEndPoint(localMachineInfo.AddressList[0], _port);
            _serverSocket = new Socket(myEndpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(myEndpoint);
            _serverSocket.Listen((int)SocketOptionName.MaxConnections);
        }
    }
    class ThreadedServer
    {
        private Socket _serverSocket;
        private int _port;
        public ThreadedServer(int port) { _port = port; }

        private class ConnectionInfo
        {
            public Socket Socket; public Thread Thread;
        }
        private Thread _acceptThread;
        private List<ConnectionInfo> _connections = new List<ConnectionInfo>();
        public void Start()
        {
            SetupServerSocket();
            _acceptThread = new Thread(AcceptConnections);
            _acceptThread.IsBackground = true;
            _acceptThread.Start();
        }
        private void SetupServerSocket()
        { // Resolving local machine information 
            IPHostEntry localMachineInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint myEndpoint = new IPEndPoint(localMachineInfo.AddressList[0], _port);
            // Create the socket, bind it, and start listening 
            _serverSocket = new Socket(myEndpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(myEndpoint); 
            _serverSocket.Listen((int)SocketOptionName.MaxConnections);
        }
        private void AcceptConnections()
        {
            while (true)
            { // Accept a connection 
                Socket socket = _serverSocket.Accept();
                ConnectionInfo connection = new ConnectionInfo();
                connection.Socket = socket; // Create the thread for the receives. 
                connection.Thread = new Thread(ProcessConnection);
                connection.Thread.IsBackground = true;
                connection.Thread.Start(connection); // Store the socket 
                lock (_connections) _connections.Add(connection);
            }
        }

        private void ProcessConnection(object state)
        {
            ConnectionInfo connection = (ConnectionInfo)state;
            byte[] buffer = new byte[255];
            try
            {
                while (true)
                {
                    int bytesRead = connection.Socket.Receive(buffer);
                    if (bytesRead > 0)
                    {
                        lock (_connections)
                        {
                            foreach (ConnectionInfo conn in _connections)
                            {
                                if (conn != connection)
                                {
                                    conn.Socket.Send(buffer, bytesRead, SocketFlags.None);
                                }
                            }
                        }
                    }
                    else if (bytesRead == 0) return;
                }
            }
            catch (SocketException exc)
            {
                Console.WriteLine("Socket exception: " + exc.SocketErrorCode);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception: " + exc);
            }
            finally
            {
                connection.Socket.Close();
                lock (_connections) _connections.Remove(connection);
            }
        }
    }
    class SelectBasedServer { // same SetupServerSocket and ctor as with ThreadedServer 
                       
        private Socket _serverSocket;
        private int _port;
        public SelectBasedServer(int port) { _port = port; }
        private Thread _acceptThread;
        private void SetupServerSocket()
        { // Resolving local machine information 
            IPHostEntry localMachineInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint myEndpoint = new IPEndPoint(localMachineInfo.AddressList[0], _port);
            // Create the socket, bind it, and start listening 
            _serverSocket = new Socket(myEndpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(myEndpoint);
            _serverSocket.Listen((int)SocketOptionName.MaxConnections);
        }
        public void Start()
        {
            Thread selectThread = new Thread(ProcessSockets); 
            selectThread.IsBackground = true; 
            selectThread.Start();
        } 
        private void ProcessSockets() { 
            byte[] buffer = new byte[255]; 
            List<Socket> readSockets = new List<Socket>(); 
            List<Socket> connectedSockets = new List<Socket>();
            try
            {
                SetupServerSocket();
                while (true)
                {
                    // Fill the read list 
                    readSockets.Clear();
                    readSockets.Add(_serverSocket);
                    readSockets.AddRange(connectedSockets); // Wait for something to do 
                    Socket.Select(readSockets, null, null, int.MaxValue);
                    // Process each socket that has something to do foreach (Socket readSocket in readSockets)
                    {
                        if (readSocket == _serverSocket)
                        {
                            // Accept and store the new client's socket 
                            Socket newSocket = readSocket.Accept();
                            connectedSockets.Add(newSocket);
                        }
                        else
                        {
                            // Read and process the data as appropriate 
                            int bytesRead = readSocket.Receive(buffer);
                            if (0 == bytesRead)
                            {
                                connectedSockets.Remove(readSocket);
                                readSocket.Close();
                            }
                            else
                            {
                                foreach (Socket connectedSocket in connectedSockets)
                                {
                                    if (connectedSocket != readSocket)
                                    {
                                        connectedSocket.Send(buffer, bytesRead, SocketFlags.None);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SocketException exc)
            {
                Console.WriteLine("Socket exception: " + exc.SocketErrorCode);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception: " + exc);
            }
            finally
            {
                foreach (Socket s in connectedSockets) 
                    s.Close(); 
                connectedSockets.Clear();
            } 
        } 
    }
}
