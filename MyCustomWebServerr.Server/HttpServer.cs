namespace MyCustomWebServer.Server
{
    using MyCustomWebServer.Server.Http;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class HttpServer
    {

        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener serverListener;

        public HttpServer(IPAddress ipAddress, int port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            serverListener = new TcpListener(this.ipAddress, this.port);
        }

        public async Task Start()
        {
            //HTTP / 1.1 404 Not Found
            //Date: Sun, 18 Oct 2012 10:36:20 GMT
            //Server: Apache / 2.2.14(Win32)
            //Content - Length: 230
            //Connection: Closed
            //Content - Type: text / html; charset = iso - 8859 - 1


            serverListener.Start();

            //await Console.Out.WriteLineAsync($"Server started on port {port}..."); // mice to know this one
            Console.WriteLine($"Server started on port {port}...");
            Console.WriteLine("Listening for requests...");

            while (true)
            {
                var connection = await serverListener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var requestText = await RSeedRequest(networkStream);

                await Console.Out.WriteLineAsync(requestText);

                var request = HttpRequest.Parse(requestText);

                await WriteResponse(networkStream);

                connection.Close();
            }
        }

        private async Task<string> ReedRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];
            var totalBytes = 0;

            var requestBuilder = new StringBuilder();

            while (networkStream.DataAvailable)
            {

                if (totalBytes > 10 * bufferLength)
                {
                    throw new InvalidOperationException("Request is too large!");
                }

                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);
                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                totalBytes += bytesRead;

            }

            var request = requestBuilder.ToString();
            return request;
        }

        private async Task WriteResponse(NetworkStream networkStream)
        {
            var content = "<h1>Здрасти от Светльо от новия ти сървър</h1>";
            var contentLength = Encoding.UTF8.GetByteCount(content);


            var response = $@"HTTP/1.1 200 OK
Server: My Web Server
Date: {DateTime.UtcNow:r}
Content-Length: {contentLength}
Content-Type: text/html; charset=UTF-8

{content}";

            var responseBytes = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseBytes);
        }

    }
}