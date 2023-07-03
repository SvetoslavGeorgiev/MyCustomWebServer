namespace MyCustomWebServer.Responses
{
    using Http;

    public class ViewResponse : HttpResponse
    {
        private const char PathSeparator = '/';

        public ViewResponse(string viewName, string controllerName)
            : base(HttpStatusCode.OK) => GetHtml(viewName, controllerName);

        private void GetHtml(string viewName, string controllerName)
        {
            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }
            
            var viewPath = Path.GetFullPath("Views/" + viewName + ".cshtml"); 

            if (!File.Exists(viewPath)) 
            {
                PrepareMissingViewError(viewName);
                return;
            }

            var text = File.ReadAllText(viewPath);

            PrepareContent(text, HttpContentType.Html);

        }

        private void PrepareMissingViewError(string viewName)
        {
            StatusCode = HttpStatusCode.NOT_FOUND;

            var errorMessage = $"View '{viewName} was not found";

            PrepareContent(errorMessage, HttpContentType.PlainText);
        }
    }
}
