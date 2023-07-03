namespace MyCustomWebServer.Http
{
    using MyCustomWebServer.Common;
    using System.Text;

    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;

            Headers.Add(HttpHeader.Server, new HttpHeader(HttpHeader.Server,"My Web Server"));
            Headers.Add(HttpHeader.Date, new HttpHeader(HttpHeader.Date, $"{DateTime.UtcNow:r}"));
        }
        public HttpStatusCode StatusCode { get; protected set; }

        public string Content { get; protected set; } = null!;

        public IDictionary<string, HttpHeader> Headers { get; } = new Dictionary<string, HttpHeader>();

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"HTTP/1.1 {(int)StatusCode} {StatusCode}");

            foreach (var header in Headers.Values)
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

            Headers.Add(HttpHeader.ContentType, new HttpHeader(HttpHeader.ContentType, contentType));
            Headers.Add(HttpHeader.ContentLength, new HttpHeader(HttpHeader.ContentLength, contentLength));

            Content = text;
        }
    }
}
