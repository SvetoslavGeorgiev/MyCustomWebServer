namespace MyCustomWebServer.Results
{
    using Http;

    public class RedirectResult : ActionResult
    {
        public RedirectResult(HttpResponse response, string location) 
            : base(response)
        {
            StatusCode = HttpStatusCode.FOUND;
            Headers.Add(HttpHeader.Location, location);
        }
    }
}
