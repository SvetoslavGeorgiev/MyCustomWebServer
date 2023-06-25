namespace MyCustomWebServer.Responses
{
    using Http;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string location) 
            : base(HttpStatusCode.FOUND)
        {
            Headers.Add("location", location);
        }
    }
}
