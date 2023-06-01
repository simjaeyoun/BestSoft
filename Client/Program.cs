// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Net;
using System.Text;

public class TCPClient
{
    public static void Main()
    {
        string ipAdd="";
        try
        {
            // Replace "example.com" with the hostname or domain name of the remote server
            IPHostEntry hostEntry = Dns.GetHostEntry("example.com");

            // Retrieve the IP addresses associated with the host
            IPAddress[] addresses = hostEntry.AddressList;

            // Print the IP addresses
            foreach (IPAddress address in addresses)
            {
                ipAdd = address.ToString();
                break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        // Set the server IP address and port
        IPAddress serverIP = IPAddress.Parse(ipAdd); // Replace with the actual remote server IP address
        int serverPort = 9998;

        // Create a TCP client socket
        TcpClient client = new TcpClient();

        try
        {
            // Connect to the server
            client.Connect(serverIP, serverPort);
            Console.WriteLine("Connected to server: {0}:{1}", serverIP, serverPort);

            // Get the client stream for reading and writing
            NetworkStream stream = client.GetStream();

            // Send data to the server
            string message = "Hello from the client!";
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent to server: {0}", message);

            // Read response from the server
            byte[] responseBuffer = new byte[1024];
            int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
            string response = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
            Console.WriteLine("Received from server: {0}", response);
        }
        finally
        {
            // Close the client socket
            client.Close();
            Console.WriteLine("Client disconnected.");
        }
    }
}