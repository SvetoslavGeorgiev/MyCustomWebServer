namespace MyCustomWebServer.Controllers
{
    using Http;
    using MyCustomWebServer.Identity;
    using MyCustomWebServer.Results.Views;
    using Results;
    using System.Runtime.CompilerServices;

    public abstract class Controller
    {
        public const string UserSessionKey = "AuthenticatedUserId";

        private UserIdentity userIdentity;

        private IViewEngine viewEngine;

        protected HttpRequest Request { get; init; }

        public HttpResponse Response { get; private init; } = new HttpResponse(HttpStatusCode.OK);

        protected UserIdentity User
        {
            get
            {
                if (this.userIdentity == null)
                {
                    this.userIdentity = this.Request.Session.Contains(UserSessionKey)
                        ? new UserIdentity { Id = this.Request.Session[UserSessionKey] }
                        : new();
                }

                return this.userIdentity;
            }
        }

        protected IViewEngine ViewEngine
        {
            get
            {
                if (this.viewEngine == null)
                {
                    this.viewEngine = this.Request.Services.Get<IViewEngine>()
                        ?? new ParserViewEngine();
                }

                return this.viewEngine;
            }
        }


        protected void SignIn(string userId)
        {
            Request.Session[UserSessionKey] = userId;
            userIdentity = new UserIdentity { Id = userId };
        }

        protected void SignOut()
        {
            Request.Session.Remove(UserSessionKey);
            userIdentity = new();
        }

        protected ActionResult Text(string text)
            => new TextResult(Response, text);

        protected ActionResult Html(string html)
            => new HtmlResult(Response, html);

        protected ActionResult BadRequest()
            => new BadRequestResult(Response);

        protected ActionResult Unauthorized()
            => new UnauthorizedResult(Response);

        protected ActionResult NotFound()
            => new NotFoundResult(Response);

        protected ActionResult Redirect(string location)
            => new RedirectResult(Response, location);

        protected ActionResult View([CallerMemberName] string viewName = "")
            => GetViewResult(viewName, null);

        protected ActionResult View(string viewName, object model)
            => GetViewResult(viewName, model);

        protected ActionResult View(object model, [CallerMemberName] string viewName = "")
            => GetViewResult(viewName, model);

        protected ActionResult Error(string error)
            => this.Error(new[] { error });

        protected ActionResult Error(IEnumerable<string> errors)
            => this.View("./Error", errors);

        private ActionResult GetViewResult(string viewName, object model)
            => new ViewResult(Response, ViewEngine, viewName, GetType().GetControllerName(), model, this.User.Id);

    }
}
