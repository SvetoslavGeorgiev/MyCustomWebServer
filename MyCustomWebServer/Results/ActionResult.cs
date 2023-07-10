namespace MyCustomWebServer.Results
{
    using Http;

    public abstract class ActionResult : HttpResponse
    {
        protected ActionResult(HttpResponse response)
            : base(response.StatusCode)
        {
            Response = response;
            StatusCode = response.StatusCode;
            Content = response.Content;
            PrepareHeaders(response.Headers);
            PrepareCookies(response.Cookies);
        }

        protected HttpResponse Response { get; private init; }

        private void PrepareHeaders(IDictionary<string, HttpHeader> headers)
        {
            foreach (var header in headers.Values)
            {
                AddHeader(header.Name, header.Value);
            }
        }

        private void PrepareCookies(IDictionary<string, HttpCookie> cookies)
        {
            foreach (var cookie in cookies.Values)
            {
                AddCookie(cookie.Name, cookie.Value);
            }
        }
    }
}
