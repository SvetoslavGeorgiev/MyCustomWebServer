namespace MyCustomWebServer.App.Controllers
{
    using MyCustomWebServer.Controllers;
    using MyCustomWebServer.Http;
    using MyCustomWebServer.Results;

    public class HomeController : Controller
    {
        public HomeController(HttpRequest request) 
            : base(request)
        {
        }

        public ActionResult Index() => Text("Hello From Svetoslav");

        public ActionResult LocalRedirect() => Redirect("/Dogs");

        public ActionResult ToSoftUni() => Redirect("https://softuni.bg/");


    }
}
