namespace MyCustomWebServer.Results
{
    using Http;

    public class BadRequestResult : ActionResult
    {
        public BadRequestResult(HttpResponse response)
            : base(response) 
            => StatusCode = HttpStatusCode.BadRequest;
    }
}
