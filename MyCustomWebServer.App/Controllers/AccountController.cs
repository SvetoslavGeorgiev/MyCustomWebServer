namespace MyCustomWebServer.App.Controllers
{
    using MyCustomWebServer.Controllers;
    using MyCustomWebServer.Http;
    using MyCustomWebServer.Results;

    public class AccountController : Controller
    {
        public AccountController(HttpRequest request) 
            : base(request)
        {
        }

        public ActionResult ActionWithCookies()
        {
            const string cookieName = "My-Cookie";

            if (Request.Cookies.ContainsKey(cookieName))
            {
                var cookie = Request.Cookies[cookieName];

                return Text($"Cookie already exist - {cookie}");
            }
            Response.AddCookie(cookieName, "My-Value");
            Response.AddCookie("My-Second-Cookie", "My-Second-Value");
            return Text("Cookies Set!");
        }

        public ActionResult ActionWithSession()
        {
            const string currentDateKey = "CurrentDate";

            if (Request.Session.ContainsKey(currentDateKey))
            {
                var currentDate = Request.Session[currentDateKey];


                return Text($"Stored date: {currentDate}!");
            }

            Request.Session[currentDateKey] = DateTime.UtcNow.ToString();

            return Text("Current date stored");
        }
    }
}
