namespace MyCustomWebServer.Controllers
{
    using Http;
    using Results;
    using System.Runtime.CompilerServices;

    public abstract class Controller
    {

        protected Controller(HttpRequest request)
        {
            Request = request;
            Response = new HttpResponse(HttpStatusCode.OK);
        }

        protected HttpRequest Request { get; private init; }
        public HttpResponse Response { get; private init; }

        protected ActionResult Text(string text)
            => new TextResult(Response, text);

        protected ActionResult Html(string html)
            => new HtmlResult(Response, html);

        protected ActionResult Redirect(string location)
            => new RedirectResult(Response, location);

        protected ActionResult View([CallerMemberName] string viewName = "")
            => new ViewResult(Response, viewName, GetControllerName(), null);

        protected ActionResult View(string viewName, object model)
            => new ViewResult(Response, viewName, GetControllerName(), model);

        protected ActionResult View(object model, [CallerMemberName] string viewName = "")
            => new ViewResult(Response, viewName, GetControllerName(), model);

        private string GetControllerName()
            => GetType().Name
                .Replace(nameof(Controller), string.Empty);

    }
}
