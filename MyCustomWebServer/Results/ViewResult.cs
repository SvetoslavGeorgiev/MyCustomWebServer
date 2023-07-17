namespace MyCustomWebServer.Results
{
    using Http;
    using MyCustomWebServer.Results.Views;

    public class ViewResult : ActionResult
    {
        private const char PathSeparator = '/';
        private readonly string[] ViewFileExtensions = { "html", "cshtml" };

        public ViewResult(
            HttpResponse response,
            IViewEngine viewEngine,
            string viewName, 
            string controllerName, 
            object model,
            string userId)
            : base(response) 
            => GetHtml(viewEngine ,viewName, controllerName, model, userId);

        private void GetHtml(IViewEngine viewEngine,  string viewName, string controllerName, object model, string userId)
        {
            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }

            var (viewPath, viewExists) = FindView(viewName);

            if (!viewExists)
            {
                this.PrepareMissingViewError(viewPath);

                return;
            }

            var viewContent = File.ReadAllText(viewPath);

            var (layoutPath, layoutExists) = FindLayout();

            if (layoutExists)
            {
                var layoutContent = File.ReadAllText(layoutPath);

                viewContent = layoutContent.Replace("@RenderBody()", viewContent);
            }

            viewContent = viewEngine.RenderHtml(viewContent, model, userId);

            this.SetContent(viewContent, HttpContentType.Html);

        }

        private void PrepareMissingViewError(string viewName)
        {
            StatusCode = HttpStatusCode.NOT_FOUND;

            var errorMessage = $"View '{viewName} was not found";

            SetContent(errorMessage, HttpContentType.PlainText);
        }

        private (string, bool) FindView(string viewName)
        {
            string viewPath = null;
            var exists = false;

            foreach (var fileExtension in ViewFileExtensions)
            {
                viewPath = Path.GetFullPath($"./Views/" + viewName.TrimStart(PathSeparator) + $".{fileExtension}");

                if (File.Exists(viewPath))
                {
                    exists = true;
                    break;
                }
            }

            return (viewPath, exists);
        }

        private (string, bool) FindLayout()
        {
            string layoutPath = null;
            bool exists = false;

            foreach (var fileExtension in ViewFileExtensions)
            {
                layoutPath = Path.GetFullPath($"./Views/Layout.{fileExtension}");

                if (File.Exists(layoutPath))
                {
                    exists = true;
                    break;
                }

                layoutPath = Path.GetFullPath($"./Views/Shared/_Layout.{fileExtension}");

                if (File.Exists(layoutPath))
                {
                    exists = true;
                    break;
                }
            }

            return (layoutPath, exists);
        }
    }
}
