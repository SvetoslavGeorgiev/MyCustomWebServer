namespace MyCustomWebServer.Server.Responses
{
    using MyCustomWebServer.Server.Http;

    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse() 
            : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
