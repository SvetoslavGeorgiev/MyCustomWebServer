namespace MyCustomWebServer.Http
{
    using MyCustomWebServer.Common;
    using Collections;
    using System.Text;

    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;

            Headers.Add(HttpHeader.Server, "My Web Server");
            Headers.Add(HttpHeader.Date, $"{DateTime.UtcNow:r}");
        }
        public HttpStatusCode StatusCode { get; protected set; }

        public byte[] Content { get; protected set; } = null!;

        public bool HasContent => Content != null && Content.Any();

        public HeaderCollection Headers { get; } = new();
        public CookieCollection Cookies { get; } = new();

        public static HttpResponse ForError(string message)
            => new HttpResponse(HttpStatusCode.INTERNAL_SERVER_ERRROR)
                .SetContent(message, HttpContentType.PlainText);

        public HttpResponse SetContent(string text, string contentType)
        {
            Guard.AgainstNull(text, nameof(text));
            Guard.AgainstNull(contentType, nameof(contentType));

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            Headers.Add(HttpHeader.ContentType, contentType);
            Headers.Add(HttpHeader.ContentLength, contentLength);

            Content = Encoding.UTF8.GetBytes(text);

            return this;
        }

        public HttpResponse SetContent(byte[] content, string contentType)
        {
            Guard.AgainstNull(content, nameof(content));
            Guard.AgainstNull(contentType, nameof(contentType));

            Headers.Add(HttpHeader.ContentType, contentType);
            Headers.Add(HttpHeader.ContentLength, content.Length.ToString());

            Content = content;

            return this;
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"HTTP/1.1 {(int)StatusCode} {StatusCode}");

            foreach (var header in Headers)
            {
                result.AppendLine($"{header.ToString()}");
            }

            foreach (var cookie in Cookies)
            {
                result.AppendLine($"{HttpHeader.SetCookie}: {cookie}");
            }

            if (HasContent)
            {
                result.AppendLine();
            }

            return result.ToString();
        }

        
    }
}
