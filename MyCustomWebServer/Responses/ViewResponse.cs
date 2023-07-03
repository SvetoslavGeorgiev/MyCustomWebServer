namespace MyCustomWebServer.Responses
{
    using Http;

    public class ViewResponse : HttpResponse
    {
        private readonly string filePath;
        public ViewResponse(string _filePath) 
            : base(HttpStatusCode.OK)
        {
            GetHtml(_filePath);
        }

        private void GetHtml(string filePath)
        {
            if (filePath == null) { }
        }


    }
}
