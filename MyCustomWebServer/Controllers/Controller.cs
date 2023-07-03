namespace MyCustomWebServer.Controllers
{
    using Http;
    using Responses;

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

        protected HttpResponse View()
            => null;

        protected HttpResponse View(string view)
            => new ViewResponse(view);

    }
}
