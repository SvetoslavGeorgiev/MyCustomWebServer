namespace MyCustomWebServer
{
    using Http;
    using Routing;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class HttpServer
    {

        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener serverListener;

        private readonly RoutingTable routingTable;


        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTableConfiguration)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            serverListener = new TcpListener(this.ipAddress, this.port);

            routingTableConfiguration(routingTable = new RoutingTable());
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable)
            : this("127.0.0.1", port, routingTable)
        {
        }

        public HttpServer(Action<IRoutingTable> routingTable)
            : this(8080, routingTable)
        {

        }

        public async Task Start()
        {

            serverListener.Start();

            //await Console.Out.WriteLineAsync($"Server started on port {port}..."); // nice to know this one
            Console.WriteLine($"Server started on port {port}...");
            Console.WriteLine("Listening for requests...");

            while (true)
            {
                var connection = await serverListener.AcceptTcpClientAsync();

                _ = Task.Run(async () =>
                {
                    var networkStream = connection.GetStream();

                    var requestText = await ReedRequest(networkStream);

                    try
                    {
                        var request = HttpRequest.Parse(requestText);

                        var response = routingTable.ExecuteRequest(request);

                        PrepareSesion(request, response);

                        LogPipeline(requestText, response.ToString());

                        await WriteResponse(networkStream, response);
                    }
                    catch (Exception exception)
                    {
                        await ErrorHandler(exception, networkStream);
                    }

                    connection.Close();
                });
            }
        }

        private async Task<string> ReedRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];
            var totalBytes = 0;

            var requestBuilder = new StringBuilder();

            do
            {
                if (totalBytes > 10 * bufferLength)
                {
                    throw new InvalidOperationException("Request is too large!");
                }

                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);
                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                totalBytes += bytesRead;
            }
            while (networkStream.DataAvailable);


            var request = requestBuilder.ToString();
            return request;
        }

        private void PrepareSesion(HttpRequest request, HttpResponse response)
            => response.AddCookie(HttpSession.SessionCookieName, request.Session.Id);

        private async Task ErrorHandler(Exception exception, NetworkStream networkStream)
        {
            var message = $"{exception.Message}{Environment.NewLine}{exception.StackTrace}";
            var errorResponse = HttpResponse.ForError(message);

            await WriteResponse(networkStream, errorResponse);
        }

        private void LogPipeline(string request, string response)
        {
            var separator = new string('-', 50);

            var log = new StringBuilder();

            log.AppendLine();
            log.AppendLine(separator);

            log.AppendLine("REQUEST:");
            log.AppendLine(request);

            log.AppendLine();

            log.AppendLine("RESPONSE:");
            log.AppendLine(response);

            log.AppendLine();

            Console.WriteLine(log);
        }

        private async Task WriteResponse(NetworkStream networkStream, HttpResponse response)
        {
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

            await networkStream.WriteAsync(responseBytes);
        }

    }
}