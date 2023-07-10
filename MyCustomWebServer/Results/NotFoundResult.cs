namespace MyCustomWebServer.Results
{
    using Http;

    public class NotFoundResult : ActionResult
    {
        public NotFoundResult(HttpResponse response)
            : base(response) 
            => StatusCode = HttpStatusCode.NOT_FOUND;
    }
}
