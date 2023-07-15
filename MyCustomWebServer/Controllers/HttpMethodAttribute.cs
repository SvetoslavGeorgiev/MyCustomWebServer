namespace MyCustomWebServer.Controllers
{

    using Http;

    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpMethodAttribute : Attribute
    {
        protected HttpMethodAttribute(HttpMethod httpMethod)
            => HttpMethod = httpMethod;

        public HttpMethod HttpMethod { get; }
    }
}
