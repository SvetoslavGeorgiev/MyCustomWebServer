namespace MyCustomWebServer.Controllers
{
    using Http;
    using MyCustomWebServer.Identity;
    using Results;
    using System.Runtime.CompilerServices;

    public abstract class Controller
    {
        public const string UserSessionKey = "AuthenticatedUserId";

        protected Controller(HttpRequest request)
        {
            Request = request;

            User = Request.Session.ContainsKey(UserSessionKey)
                ? new UserIdentity { Id = Request.Session[UserSessionKey] } 
                : new();
        }

        protected HttpRequest Request { get; private init; }

        public HttpResponse Response { get; private init; } = new HttpResponse(HttpStatusCode.OK);

        protected UserIdentity User { get; private set; }

        protected void SignIn(string userId)
        {
            Request.Session[UserSessionKey] = userId;
            User = new UserIdentity { Id = userId };
        }

        protected void SignOut()
        {
            Request.Session.Remove(UserSessionKey);
            User = new();
        }

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
