namespace MyCustomWebServer.Server.Responses
{
    using MyCustomWebServer.Server.Common;
    using MyCustomWebServer.Server.Http;
    using System.Text;

    public class TextResponse : HttpResponse
    {
        public TextResponse(string text, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            Headers.Add("Content-Type", contentType);
            Headers.Add("Content-Length", contentLength);

            Content = text;
        }

        public TextResponse(string text)
            : this(text, "text/plain; charset=UTF-8")
        {
        }
    }
}
