namespace MyCustomWebServer.Responses
{
    using Common;
    using Http;
    using System.Text;

    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string text, string contentType)
            : base(HttpStatusCode.OK) 
            => PrepareContent(text, contentType);
    }
}
