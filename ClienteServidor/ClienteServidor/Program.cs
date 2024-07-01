using System;
using System.Threading;
using System.Threading.Tasks;
using clienteServidor.server;
using clienteServidor.client;

namespace MainProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Solicitar datos al usuario
            Console.Write("Ingrese el puerto para iniciar el socket: ");
            int port = Convert.ToInt32(Console.ReadLine());

            // Se inicia el servidor
            _ = Task.Run(async () =>
            {
                try
                {
                    Server server = new Server();
                    await server.StartAsync(port);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });

            // se da un tiempo para el inicio del servidor
            Thread.Sleep(1000);

            // Se inicia el cliente
            Client client = new Client();
            try
            {
                await client.StartConnectionAsync("127.0.0.1", port);
                string response = await client.SendMessageAsync("hello server");
                Console.WriteLine($"Server response: {response}");

                response = await client.SendMessageAsync("bye");
                Console.WriteLine($"Server response: {response}");

                client.StopConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
