using System;
using System.IO;
using System.Net.Sockets;
class ClientSide
{
    static void Main()
    {
        TcpClient client = null;
        NetworkStream stream = null;
        StreamReader reader = null;
        StreamWriter writer = null;
        try
        {
            client = new TcpClient("127.0.0.1", 8888);
            Console.WriteLine("Connected to Coffee Shop Server!");
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
            string serverMessage;
            while ((serverMessage = reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(serverMessage))
            {
                Console.WriteLine(serverMessage);
                Console.Write("Your Order : ");
                string userInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userInput))
                    break; 
                writer.WriteLine(userInput);
                string response = reader.ReadLine();
                if (response != null){
                    Console.WriteLine($"Server: {response}");}
                else{
                    Console.WriteLine("Server Disconnected  .");
                    break; }}}
        catch (SocketException ex){
            Console.WriteLine($"Connection Error : {ex.Message}");}
        finally {
            reader?.Close();
            writer?.Close();
            stream?.Close();
            client?.Close();}}}
