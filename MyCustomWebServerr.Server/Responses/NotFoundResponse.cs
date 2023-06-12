namespace MyCustomWebServer.Server.Responses
{
    using MyCustomWebServer.Server.Http;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse() 
            : base(HttpStatusCode.NOT_FOUND)
        {
        }
    }
}
