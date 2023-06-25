namespace MyCustomWebServer.App.Controllers
{
    using MyCustomWebServer.Controllers;
    using MyCustomWebServer.Http;
    using System.Data.Common;

    public class HomeController : Controller
    {
        public HomeController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Index() => Text("Hello From Svetoslav");

        public HttpResponse ToSoftUni() => Redirect("https://softuni.bg/");


    }
}
