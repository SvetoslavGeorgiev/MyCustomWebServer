namespace MyCustomWebServer.Results
{
    using Http;

    public class ViewResult : ActionResult
    {
        private const char PathSeparator = '/';

        public ViewResult(
            HttpResponse response,
            string viewName, 
            string controllerName, 
            object model)
            : base(response) 
            => GetHtml(viewName, controllerName, model);

        private void GetHtml(string viewName, string controllerName, object model)
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

            var viewContent = File.ReadAllText(viewPath);

            if (model != null)
            {
                viewContent = PopulateModel(viewContent, model);
            }

            PrepareContent(viewContent, HttpContentType.Html);

        }

        private void PrepareMissingViewError(string viewName)
        {
            StatusCode = HttpStatusCode.NOT_FOUND;

            var errorMessage = $"View '{viewName} was not found";

            PrepareContent(errorMessage, HttpContentType.PlainText);
        }

        private string PopulateModel(string viewContent, object model)
        {
            var data = model
                .GetType()
                .GetProperties()
                .Select(pr => new
                {
                    Name = pr.Name,
                    Value = pr.GetValue(model)
                });

            foreach (var property in data)
            {
                const string openingBrackets = "{{";
                const string closingBrackets = "}}";

                var name = $"{openingBrackets}{property.Name}{closingBrackets}";

                viewContent = viewContent.Replace($"{name}", property.Value.ToString());
            }

            return viewContent;
        }
    }
}
