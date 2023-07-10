namespace MyCustomWebServer.App.Controllers
{
    using MyCustomWebServer.Controllers;
    using MyCustomWebServer.Http;
    using MyCustomWebServer.Results;

    public class CatsController : Controller
    {
        public CatsController(HttpRequest request) 
            : base(request)
        {
        }


        public ActionResult Create() => View();

       
        public ActionResult Save()
        {
            var name = Request.Form["Name"];
            var age = Request.Form["Age"];

            return Text($"{name} - {age}");
        }
        //public HttpResponse Delete() => View();
    }
}
