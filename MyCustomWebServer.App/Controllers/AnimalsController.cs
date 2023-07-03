namespace MyCustomWebServer.App.Controllers
{
    using Http;
    using Models.Animals;
    using MyCustomWebServer.Controllers;
    using System.Net.Http.Headers;

    public class AnimalsController : Controller
    {
        
        public AnimalsController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";
            const string ageKey = "Age";

            var query = Request.Query;

            var catName = query.ContainsKey(nameKey)
                ? query[nameKey]
                : "the cats";

            var catAge = query.ContainsKey(ageKey)
                ? int.Parse(query[ageKey])
                : 0;
            var model = new CatViewModel 
            { 
                Name = catName, 
                Age = catAge 
            };


            return View(model);
        }


        public HttpResponse Dogs() => View();
        public HttpResponse Bunnies() => View("Rabbits");
        public HttpResponse Turtles() => View("Animals/Wild/Turtles");
    }
}
