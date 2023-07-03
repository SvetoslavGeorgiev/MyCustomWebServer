namespace MyCustomWebServer.Controllers
{
    using Http;
    using Responses;
    using System.Runtime.CompilerServices;

    public abstract class Controller
    {

        protected Controller(HttpRequest request)
            => Request = request;

        protected HttpRequest Request { get; private init; }


        protected HttpResponse Text(string text)
            => new TextResponse(text);

        protected HttpResponse Html(string html)
            => new HtmlResponse(html);

        protected HttpResponse Redirect(string location)
            => new RedirectResponse(location);

        protected HttpResponse View([CallerMemberName] string viewName = "")
            => new ViewResponse(viewName, GetControllerName());

        private string GetControllerName()
            => GetType().Name
                .Replace(nameof(Controller), string.Empty);

    }
}
