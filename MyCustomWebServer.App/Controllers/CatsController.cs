namespace MyCustomWebServer.App.Controllers
{
    using MyCustomWebServer.App.Data;
    using MyCustomWebServer.Controllers;
    using MyCustomWebServer.Http;
    using MyCustomWebServer.Results;

    public class CatsController : Controller
    {

        private readonly IData data;

        public CatsController(IData data)
            => this.data = data;

        public ActionResult All()
        {
            var cats = this.data
                .Cats
                .ToList();

            return View(cats);
        }

        public ActionResult AllHtml()
        {
            var cats = this.data
                .Cats
                .ToList();

            var result = "<h1>All cats in the system:</h1>";

            result += "<ul>";

            foreach (var cat in cats)
            {
                result += "<li>";
                result += cat.Name + "-" + cat.Age;
                result += "</li>";
            }

            result += "</ul>";

            return Html(result);
        }

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Save()
        {
            var name = Request.Form["Name"];
            var age = Request.Form["Age"];

            return Text($"{name} - {age}");
        }
        //public HttpResponse Delete() => View();
    }
}
