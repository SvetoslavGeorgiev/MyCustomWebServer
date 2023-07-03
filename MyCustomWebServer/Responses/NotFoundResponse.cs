namespace MyCustomWebServer.Responses
{
    using Http;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse() 
            : base(HttpStatusCode.NOT_FOUND)
        {
        }
    }
}
