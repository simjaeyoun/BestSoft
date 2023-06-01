// See https://aka.ms/new-console-template for more information
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class TCPServer
{
    public static void Main()
    {
        // Set the listening IP address and port
        IPAddress ipAddress = IPAddress.Any; // Listen on all available network interfaces
        int port = 9998;

        // Create a TCP listener
        TcpListener listener = new TcpListener(ipAddress, port);
        listener.Start();

        Console.WriteLine("Server is listening on {0}:{1}", ipAddress, port);

        try
        {
            while (true)
            {
                // Accept client connections
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected: {0}", client.Client.RemoteEndPoint);

                // Handle client communication in a separate thread
                System.Threading.Tasks.Task.Run(() =>
                {
                    // Get the client stream for reading and writing
                    NetworkStream stream = client.GetStream();

                    // Read data from the client
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received from client: {0}", dataReceived);

                    // Process the received data (e.g., perform any necessary operations)

                    // Send response to the client
                    string response = "Hello from the server!";
                    byte[] responseData = Encoding.ASCII.GetBytes(response);
                    stream.Write(responseData, 0, responseData.Length);
                    Console.WriteLine("Sent to client: {0}", response);

                    // Close the client connection
                    client.Close();
                    Console.WriteLine("Client disconnected: {0}", client.Client.RemoteEndPoint);
                });
            }
        }
        finally
        {
            // Stop listening and clean up
            listener.Stop();
        }
    }
}