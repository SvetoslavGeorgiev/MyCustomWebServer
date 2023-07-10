namespace MyCustomWebServer.App.Controllers
{
    using Http;
    using Models.Animals;
    using MyCustomWebServer.Controllers;
    using MyCustomWebServer.Results;

    public class AnimalsController : Controller
    {
        
        public AnimalsController(HttpRequest request) 
            : base(request)
        {
        }

        public ActionResult Cats()
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


        public ActionResult Dogs() => View(new DogViewModel
        {
            Name = "Rex",
            Age = 3,
            Breed = "German Shepherd"
        });
        public ActionResult Bunnies() => View("Rabbits");
        public ActionResult Turtles() => View("Animals/Wild/Turtles");
    }
}
