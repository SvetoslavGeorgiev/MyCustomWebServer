namespace MyCustomWebServer.App.Controllers
{
    using Http;
    using MyCustomWebServer.Controllers;

    public class AnimalsContreller : Controller
    {
        
        public AnimalsContreller(HttpRequest request) 
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


        public HttpResponse Dogs()
        {
            const string nameKey = "Name";

            var query = Request.Query;

            var catName = query.ContainsKey(nameKey)
                ? query[nameKey]
                : "the dogs";

            var result = $"<h1>Hello from {catName}!</h1>";

            return Html(result);
        }
    }
}
