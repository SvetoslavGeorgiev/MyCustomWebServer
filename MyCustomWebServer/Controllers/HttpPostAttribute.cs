namespace MyCustomWebServer.Controllers
{
    using Http;
    public class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute()
            : base(HttpMethod.Post)
        {
        }
    }
}
