namespace MyCustomWebServer.Responses
{
    using Http;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string location) 
            : base(HttpStatusCode.FOUND)
        {
            Headers.Add(HttpHeader.Location, new HttpHeader(HttpHeader.Location, location));
        }
    }
}
