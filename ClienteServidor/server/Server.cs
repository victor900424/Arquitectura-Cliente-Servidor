using System.Net;
using System.Net.Sockets;
namespace clienteServidor.server
{
    public class Server
    {
        private TcpListener server;

        public async Task StartAsync(int port)
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }

        public void Stop()
        {
            server.Stop();
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
            {
                string message;
                while ((message = await reader.ReadLineAsync()) != null)
                {
                    if (string.Equals(message, "hello server", StringComparison.OrdinalIgnoreCase))
                    {
                        await writer.WriteLineAsync("hello client");
                    }
                    else if (string.Equals(message, "bye", StringComparison.OrdinalIgnoreCase))
                    {
                        await writer.WriteLineAsync("bye");
                        break;
                    }
                    else
                    {
                        await writer.WriteLineAsync("unrecognized message");
                    }
                }
            }

            client.Close();
        }
    }
}