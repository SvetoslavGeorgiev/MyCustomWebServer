namespace MyCustomWebServer.Controllers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeAttribute : Attribute
    {
    }
}
