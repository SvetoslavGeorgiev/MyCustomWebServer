namespace MyCustomWebServer.Http
{
    using MyCustomWebServer.Common;
    using System.Text;
    using static System.Net.Mime.MediaTypeNames;

    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;

            AddHeader(HttpHeader.Server, "My Web Server");
            AddHeader(HttpHeader.Date, $"{DateTime.UtcNow:r}");
        }
        public HttpStatusCode StatusCode { get; protected set; }

        public byte[] Content { get; protected set; } = null!;

        public bool HasContent => Content != null && Content.Any();

        public IDictionary<string, HttpHeader> Headers { get; } = new Dictionary<string, HttpHeader>();
        public IDictionary<string, HttpCookie> Cookies { get; } = new Dictionary<string, HttpCookie>();

        public static HttpResponse ForError(string message)
            => new HttpResponse(HttpStatusCode.INTERNAL_SERVER_ERRROR)
                .SetContent(message, HttpContentType.PlainText);

        public HttpResponse SetContent(string text, string contentType)
        {
            Guard.AgainstNull(text, nameof(text));
            Guard.AgainstNull(contentType, nameof(contentType));

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            AddHeader(HttpHeader.ContentType, contentType);
            AddHeader(HttpHeader.ContentLength, contentLength);

            Content = Encoding.UTF8.GetBytes(text);

            return this;
        }

        public HttpResponse SetContent(byte[] content, string contentType)
        {
            Guard.AgainstNull(content, nameof(content));
            Guard.AgainstNull(contentType, nameof(contentType));

            AddHeader(HttpHeader.ContentType, contentType);
            AddHeader(HttpHeader.ContentLength, content.Length.ToString());

            Content = content;

            return this;
        }

        public void AddHeader(string name, string value)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(value, nameof(value));

            Headers[name] = new HttpHeader(name, value);
        }

        public void AddCookie(string name, string value)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(value, nameof(value));

            Cookies[name] = new HttpCookie(name, value);
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"HTTP/1.1 {(int)StatusCode} {StatusCode}");

            foreach (var header in Headers.Values)
            {
                result.AppendLine($"{header.ToString()}");
            }

            foreach (var cookie in Cookies.Values)
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
