namespace MyCustomWebServer.Responses
{
    using Http;
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string html) 
            : base(html, HttpContentType.Html)
        {
        }
    }
}
