using System.Net.Sockets;
using Google.Protobuf;

var serverIp = "127.0.0.1";
var port = 5010;

try
{
	using var client = new TcpClient();
	Console.WriteLine("Sender: Connecting to " + serverIp + ":" + port);
	client.Connect(serverIp, port);
	using var stream = client.GetStream();
    
	// if you need to convert int value to bytes array
	

	var pos = new Position()
	{
		X = 1, Y = 2, Z = 3
	};
	
	var msg = pos.ToByteArray();
	
	byte[] lengthBytes = BitConverter.GetBytes(msg.Length);
	
	stream.Write(lengthBytes, 0, lengthBytes.Length);
	
	stream.Write(msg, 0, msg.Length);
	
	
	// Or an array
	// stream.Write(messageBytes, 0, messageBytes.Length);
	Console.WriteLine("Sender: Message sent.");
}
catch (Exception ex)
{
	Console.WriteLine("Sender: Exception - " + ex.Message);
}