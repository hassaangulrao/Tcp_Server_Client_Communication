using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
class CoffeeShopServer
{
    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 8888);
        server.Start();
        Console.WriteLine("Coffee Shop Server started...");
        while (true)
        {
            Socket clientSocket = server.AcceptSocket();
            Console.WriteLine("Client connected...");
            Thread clientThread = new Thread(() => HandleClient(clientSocket));
            clientThread.Start();
        }
    }
    private static void HandleClient(Socket clientSocket)
    {
        NetworkStream stream = null;
        StreamReader reader = null;
        StreamWriter writer = null;
        try
        {
            stream = new NetworkStream(clientSocket);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
            writer.WriteLine("Welcome to the Coffee Shop!");
            writer.WriteLine("Menu:");
            writer.WriteLine("1. Latte - $39.00");
            writer.WriteLine("2. Cold Coffee - $24.00");
            writer.WriteLine("3. Green Tea - $2.50");
            writer.WriteLine("Type what ypu want to order (e.g., 'Espresso'):");
            while (true)
            {
                string clientMessage = reader.ReadLine();
                if (clientMessage == null) break;
                Console.WriteLine($"Client Order: {clientMessage}");
                string response = $"Your {clientMessage} is in progress. Thank you for your patience!";
                writer.WriteLine(response);
            }
        }
        catch (IOException)
        {
            Console.WriteLine("Client disconnected.");
        }
        finally
        {
            reader?.Close();
            writer?.Close();
            stream?.Close();
            clientSocket.Close();
            Console.WriteLine("Connection closed.");
        }
    }
}
