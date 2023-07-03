namespace MyCustomWebServer.App.Controllers
{
    using MyCustomWebServer.Controllers;
    using MyCustomWebServer.Http;

    public class CatsController : Controller
    {
        public CatsController(HttpRequest request) 
            : base(request)
        {
        }


        public HttpResponse Create() => View();

       
        public HttpResponse Save()
        {
            var name = Request.Form["Name"];
            var age = Request.Form["Age"];

            return Text($"{name} - {age}");
        }
        //public HttpResponse Delete() => View();
    }
}
