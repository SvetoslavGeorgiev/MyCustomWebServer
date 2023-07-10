namespace MyCustomWebServer.Results
{
    using Http;

    public class ContentResult : ActionResult
    {
        public ContentResult(HttpResponse response, string text, string contentType)
            : base(response) 
            => PrepareContent(text, contentType);
    }
}
