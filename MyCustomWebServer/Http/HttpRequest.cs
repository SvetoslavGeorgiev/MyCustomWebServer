namespace MyCustomWebServer.Http
{
    using Collections;
    using Sevices;
    using System.Net;
    using System.Web;

    public class HttpRequest
    {
        private const string NewLine = "\r\n";

        private static Dictionary<string, HttpSession> Sessions = new();
        public HttpMethod Method { get; private set; }
        public string Path { get; private set; }

        public QueryCollection Query { get ; private set; }

        public FormCollection Form { get; private set; }

        public HeaderCollection Headers { get; private set; } = null!;

        public Collections.CookieCollection Cookies { get; private set; } = null!;

        public HttpSession Session { get; private set; } = null!;

        public string Body { get; private set; } = null!;

        public ServiceCollection Services { get; private set; } = null!;

        public static HttpRequest Parse(string requset, ServiceCollection services)
        {
            var lines = requset.Split(NewLine);

            var startLine = lines.First().Split(" ");
            var method = ParseMethod(startLine[0]);

            var url = startLine[1];

            var (path, query) = ParseUrl(url);

            var headers = ParseHeaders(lines.Skip(1));

            var cookies = ParseCookies(headers);

            var session = GetSession(cookies);

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join(NewLine, bodyLines);

            var form = ParseForm(headers, body);

            return new HttpRequest
            {
                Method = method,
                Path = path,
                Headers = headers,
                Cookies = cookies,
                Session = session,
                Query = query,
                Body = body,
                Form = form,
                Services = services
            };
            
        }

        private static HttpSession GetSession(Collections.CookieCollection cookies)
        {
            var sessionId = cookies.Contains(HttpSession.SessionCookieName)
                ? cookies[HttpSession.SessionCookieName] 
                : Guid.NewGuid().ToString();

            if (!Sessions.ContainsKey(sessionId))
            {
                Sessions[sessionId] = new HttpSession(sessionId) { IsNew = true };
            }

            return Sessions[sessionId];
        }

        private static HttpMethod ParseMethod(string method)
        {
            try
            {
                return (HttpMethod)Enum.Parse(typeof(HttpMethod), method, true);
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Method '{method}' is not supported");
            }
        }

        private static (string, QueryCollection) ParseUrl(string url)
        {
            var urlParts = url.Split('?', 2);

            var path = urlParts[0];


            var query = urlParts.Length > 1
                ? ParseQuery(urlParts[1])
                : new QueryCollection();

            return (path, query);


        }

        private static QueryCollection ParseQuery(string url)
        {

            var query = new QueryCollection();

            var parsedResult = ParseQueryString(url);

            foreach (var (name, value) in parsedResult)
            {
                query.Add(name, value);
            }
            return query;
        }

        private static HeaderCollection ParseHeaders(IEnumerable<string> headerLines)
        {
            var headers = new HeaderCollection();

            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty)
                {
                    break;
                }

                var headerParts = headerLine.Split(":", 2);

                if (headerParts.Length != 2)
                {
                    throw new InvalidOperationException("Request is not valid");
                }

                var headerName = headerParts[0];
                var headerValue = headerParts[1].Trim();

                headers.Add(headerName, headerValue);

            }

            return headers;
        }

        private static Collections.CookieCollection ParseCookies(HeaderCollection headers)
        {
            var cookies = new Collections.CookieCollection();

            if (headers.Contains(HttpHeader.Cookie))
            {
                var cookieHeader = headers[HttpHeader.Cookie];

                var allCookies = cookieHeader.Split(';');

                foreach ( var cookie in allCookies)
                {
                    var cookieParts = cookie.Split("=", 2);

                    var cookieName = cookieParts[0].Trim();
                    var cookieValue = cookieParts[1].Trim();

                    cookies.Add(cookieName, cookieValue);
                }
            }

            return cookies;
        }

        private static FormCollection ParseForm(HeaderCollection headers, string body)
        {
            var result = new FormCollection();
            if (headers.Contains(HttpHeader.ContentType) 
                && headers[HttpHeader.ContentType] == HttpContentType.FormUrlEncoded)
            {
                var parsedResult = ParseQueryString(body);

                foreach (var (name, value) in parsedResult)
                {
                    result.Add(name, value);
                }
            }

            return result;
        }

        private static Dictionary<string, string> ParseQueryString(string queryString)
            => HttpUtility.UrlDecode(queryString)
                .Split('&')
                .Select(part => part.Split('='))
                .Where(part => part.Length == 2)
                .ToDictionary(
                    part => part[0],
                    part => part[1],
                    StringComparer.InvariantCultureIgnoreCase);
    }
}
