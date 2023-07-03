namespace MyCustomWebServer.Http
{
    using MyCustomWebServer.Common;
    using System.Net.Mime;
    using System.Text;
    using static System.Net.Mime.MediaTypeNames;

    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;

            Headers.Add("Server", "My Web Server");
            Headers.Add("Date", $"{DateTime.UtcNow:r}");
        }
        public HttpStatusCode StatusCode { get; protected set; }

        public string Content { get; protected set; } = null!;

        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"HTTP/1.1 {(int)StatusCode} {StatusCode}");

            foreach (var header in Headers)
            {
                result.AppendLine($"{header.ToString()}");
            }

            if (!string.IsNullOrEmpty(Content))
            {
                result.AppendLine();

                result.Append(Content);
            }

            return result.ToString();
        }

        protected void PrepareContent(string text, string contentType)
        {
            Guard.AgainstNull(text, nameof(text));
            Guard.AgainstNull(contentType, nameof(contentType));

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            Headers.Add("Content-Type", contentType);
            Headers.Add("Content-Length", contentLength);

            Content = text;
        }
    }
}
