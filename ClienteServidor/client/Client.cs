using System.Net.Sockets;

namespace clienteServidor.client
{
    public class Client
    {
        private TcpClient clientSocket;
        private StreamWriter writer;
        private StreamReader reader;

        public async Task StartConnectionAsync(string ip, int port)
        {
            clientSocket = new TcpClient();
            await clientSocket.ConnectAsync(ip, port);
            NetworkStream stream = clientSocket.GetStream();
            writer = new StreamWriter(stream) { AutoFlush = true };
            reader = new StreamReader(stream);
        }

        public async Task<string> SendMessageAsync(string msg)
        {
            await writer.WriteLineAsync(msg);
            return await reader.ReadLineAsync();
        }

        public void StopConnection()
        {
            reader.Close();
            writer.Close();
            clientSocket.Close();
        }
    }
}