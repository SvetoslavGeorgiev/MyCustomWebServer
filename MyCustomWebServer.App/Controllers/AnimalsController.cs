﻿namespace MyCustomWebServer.App.Controllers
{
    using Http;
    using MyCustomWebServer.Controllers;

    public class AnimalsController : Controller
    {
        
        public AnimalsController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";

            var query = Request.Query;

            var catName = query.ContainsKey(nameKey)
                ? query[nameKey]
                : "the cats";

            var result = $"<h1>Hello from {catName}!</h1>";

            return Html(result);
        }


        public HttpResponse Dogs() => View();
        public HttpResponse Bunnies() => View("Rabbits");
        public HttpResponse Turtles() => View("Animals/Wild/Turtles");
    }
}