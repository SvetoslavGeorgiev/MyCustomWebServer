namespace MyCustomWebServer.Results
{
    using Http;
    using Http.Collections;

    public abstract class ActionResult : HttpResponse
    {
        protected ActionResult(HttpResponse response)
            : base(response.StatusCode)
        {
            Content = response.Content;
            PrepareHeaders(response.Headers);
            PrepareCookies(response.Cookies);
        }

        private void PrepareHeaders(HeaderCollection headers)
        {
            foreach (var header in headers)
            {
                Headers.Add(header.Name, header.Value);
            }
        }

        private void PrepareCookies(CookieCollection cookies)
        {
            foreach (var cookie in cookies)
            {
                Cookies.Add(cookie.Name, cookie.Value);
            }
        }
    }
}
