namespace MyCustomWebServer.App.Controllers
{
    using Models.Animals;
    using MyCustomWebServer.Controllers;
    using MyCustomWebServer.Results;

    public class DogsController : Controller
    {
        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(DogFormModel model)
            => Text($"Dog: {model.Name} - {model.Age} - {model.Breed}");
    }
}
