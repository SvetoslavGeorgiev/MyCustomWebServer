namespace MyCustomWebServer.App.Controllers
{
    using MyCustomWebServer.Controllers;
    using MyCustomWebServer.Http;
    using MyCustomWebServer.Results;

    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            //var user = DBNull.Users.Find(username, password);
            //
            //if (user != null)
            //{
            //    SignIn(user.Id);
            //
            //    return Text("User is authenticated!");
            //}
            //
            // return Text("Invalid Credentials!");

            var someUserId = "MyUserId";

            SignIn(someUserId);

            return Text("User authenticated!");
        }

        public ActionResult Logout()
        { 
            SignOut();

            return Text("User is signed out!");
        }

        public ActionResult AuthenticationCheck()
        {
            if (User.IsAuthenticated)
            {
                return Text($"Authenticated user: {User.Id}");
            }

            return Text("User is not Authenticated!");
        }

        [Authorize]
        public ActionResult AuthorizationCheck()
            => Text($"Current user: {this.User.Id}");

        public ActionResult CookiesCheck()
        {
            const string cookieName = "My-Cookie";

            if (Request.Cookies.Contains(cookieName))
            {
                var cookie = Request.Cookies[cookieName];

                return Text($"Cookie already exist - {cookie}");
            }
            Response.Cookies.Add(cookieName, "My-Value");
            Response.Cookies.Add("My-Second-Cookie", "My-Second-Value");
            return Text("Cookies Set!");
        }

        public ActionResult SessionCheck()
        {
            const string currentDateKey = "CurrentDate";

            if (Request.Session.Contains(currentDateKey))
            {
                var currentDate = Request.Session[currentDateKey];


                return Text($"Stored date: {currentDate}!");
            }

            Request.Session[currentDateKey] = DateTime.UtcNow.ToString();

            return Text("Current date stored");
        }
    }
}
