namespace MyCustomWebServer.App.Controllers
{
    using MyCustomWebServer.Controllers;
    using MyCustomWebServer.Http;
    using MyCustomWebServer.Results;
    using Models.Animals;

    public class HomeController : Controller
    {

        public ActionResult Index() => Text("Hello From Svetoslav");

        public ActionResult LocalRedirect() => Redirect("/Animals/Dogs");

        public ActionResult ToSoftUni() => Redirect("https://softuni.bg/");

        public ActionResult StaticFiles() => View();

        public HttpResponse HtmlView() => View(new CatViewModel { Name = "Sharo", Age = 5 });

        public HttpResponse MissingView() => View();

        public ActionResult Error() => throw new InvalidOperationException("Invalid action!");


    }
}
