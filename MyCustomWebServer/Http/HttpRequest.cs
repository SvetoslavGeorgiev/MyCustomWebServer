namespace MyCustomWebServer.Http
{
    public class HttpRequest
    {
        private const string NewLine = "\r\n";

        private static Dictionary<string, HttpSession> Sessions = new();
        public HttpMethod Method { get; private set; }
        public string Path { get; private set; } = string.Empty;

        public IReadOnlyDictionary<string, string> Query { get ; private set; }

        public IReadOnlyDictionary<string, string> Form { get; private set; }

        public IReadOnlyDictionary<string, HttpHeader> Headers { get; private set; } = null!;

        public IReadOnlyDictionary<string, HttpCookie> Cookies { get; private set; } = null!;

        public HttpSession Session { get; private set; } = null!;

        public string Body { get; private set; } = null!;

        public static HttpRequest Parse(string requset)
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
                Form = form
            };
            
        }

        private static HttpSession GetSession(Dictionary<string, HttpCookie> cookies)
        {
            var sessionId = cookies.ContainsKey(HttpSession.SessionCookieName)
                ? cookies[HttpSession.SessionCookieName].Value 
                : Guid.NewGuid().ToString();

            if (!Sessions.ContainsKey(sessionId))
            {
                Sessions[sessionId] = new HttpSession(sessionId);
            }

            return Sessions[sessionId];
        }

        private static HttpMethod ParseMethod(string method) 
            => method.ToUpper() switch
        {
            "GET" => HttpMethod.Get,
            "POST" => HttpMethod.Post,
            "PUT" => HttpMethod.Put,
            "DELETE" => HttpMethod.Delete,
            _ => throw new InvalidOperationException($"Method '{method}' is not supported")
        };

        private static (string, Dictionary<string, string>) ParseUrl(string url)
        {
            var urlParts = url.Split('?', 2);

            var path = urlParts[0];

            var query =  new Dictionary<string, string>();
            if (urlParts.Length > 1)
            {
                var queryParts = urlParts[1].Split("&", 2);

                query = ParseQuery(queryParts);
            }
            
            return (path, query);

            
        }

        private static Dictionary<string, string> ParseQuery(string[] queryParts)
        {

            var query = new Dictionary<string, string>();

            foreach (var part in queryParts)
            {
                var partsTokens = part.Split("=", 2);

                if (partsTokens.Length == 2 && !query.ContainsKey(partsTokens[0]))
                {
                    query.Add(partsTokens[0], string.Empty);
                }
                if (partsTokens.Length == 2)
                {
                    query[partsTokens[0]] = partsTokens[1];
                }
            }

            return query;
        }

        private static Dictionary<string, HttpHeader> ParseHeaders(IEnumerable<string> headerLines)
        {
            var headers = new Dictionary<string, HttpHeader>();

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

                headers.Add(headerName, new HttpHeader(headerName, headerValue));
            }

            return headers;
        }

        private static Dictionary<string, HttpCookie> ParseCookies(Dictionary<string, HttpHeader> headers)
        {
            var cookies = new Dictionary<string, HttpCookie>();

            var cookieHeader = headers.Values.FirstOrDefault(h => h.Name.Equals(HttpHeader.Cookie));

            if (cookieHeader != null)
            {
                var allCookies = cookieHeader
                    .Value
                    .Split(';');

                foreach ( var cookie in allCookies)
                {
                    var cookieParts = cookie.Split("=", 2);

                    var cookieName = cookieParts[0].Trim();
                    var cookieValue = cookieParts[1].Trim();

                    cookies.Add(cookieName, new HttpCookie (cookieName, cookieValue));
                }
            }

            return cookies;
        }

        private static Dictionary<string, string> ParseForm(Dictionary<string, HttpHeader> headers, string body)
        {
            var result = new Dictionary<string, string>();
            if (headers.ContainsKey(HttpHeader.ContentType) 
                && headers[HttpHeader.ContentType].Value == HttpContentType.FormUrlEncoded)
            {
                result = ParseQuery(body.Split("&", 2));
            }

            return result;
        }
    }
}
