using System.Net;
using System.Net.Sockets;

var port = 5010;
var listener = new TcpListener(IPAddress.Any, port);
listener.Start();
Console.WriteLine("Receiver: Listening on port " + port);

while (true)
{
	using var client = listener.AcceptTcpClient();
	Console.WriteLine("Receiver: Connection accepted.");
	using NetworkStream stream = client.GetStream();
	
	// if you want to read N (where N is small number), do
	// var buffer = new byte[N];
	// var bytesRead = stream.Read(buffer, 0, buffer.Length);
    
	// if you need to convert byte array to int, do
	// var intValue = BitConverter.ToInt32(buffer, 0);
    
	// if you want to read big N, you must do it in chunks
	// var messageBuffer = new byte[Length];
	// var totalRead = 0;
	//while (totalRead < Length)
	// {
	// int read = stream.Read(messageBuffer, totalRead, Length - totalRead);
	// if (read == 0)
	// {
	// break;
	// }
	// totalRead += read;
	// }
	
	var buffer = new byte[4];
	var bytesRead = stream.Read(buffer, 0, buffer.Length);
	var size = BitConverter.ToInt32(buffer, 0);
	
	var messageBuffer = new byte[size];
	 var totalRead = 0;
	while (totalRead < size)
	 {
	 int read = stream.Read(messageBuffer, totalRead, size - totalRead);
	 if (read == 0)
	 {
	 break;
	 }
	 totalRead += read;
	 }

	var pos = Position.Parser.ParseFrom(messageBuffer);
	Console.WriteLine(pos);
}