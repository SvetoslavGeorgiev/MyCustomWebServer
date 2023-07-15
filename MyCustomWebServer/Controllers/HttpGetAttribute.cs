namespace MyCustomWebServer.Controllers
{
    using Http;
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute()
            : base(HttpMethod.Get)
        {
        }
    }
}
