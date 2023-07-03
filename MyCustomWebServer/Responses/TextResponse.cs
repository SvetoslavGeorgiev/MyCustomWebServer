namespace MyCustomWebServer.Responses
{
    using Http;

    public class TextResponse : ContentResponse
    {
        
        public TextResponse(string text)
            : base(text, HttpContentType.PlainText)
        {
        }
    }
}
