namespace MyCustomWebServer.Results
{
    using Http;

    public class TextResult : ContentResult
    {
        
        public TextResult(HttpResponse response, string text)
            : base(response, text, HttpContentType.PlainText)
        {
        }
    }
}
