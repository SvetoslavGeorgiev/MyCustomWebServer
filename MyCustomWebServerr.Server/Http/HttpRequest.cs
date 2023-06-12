namespace MyCustomWebServer.Server.Http
{
    using System.Xml.Linq;

    public class HttpRequest
    {
        private const string NewLine = "\r\n";

        public HttpMethod Method { get; private set; }
        public string Path { get; private set; } = string.Empty;

        public Dictionary<string, string> Query { get ; private set; }

        public HttpHeaderCollection Headers { get; private set; } = null!;

        public string Body { get; private set; } = null!;

        public static HttpRequest Parse(string requset)
        {
            var lines = requset.Split(NewLine);

            var startLine = lines.First().Split(" ");

            var method = ParseHttpMethod(startLine[0]);
            var url = startLine[1];

            var (path, query) = ParseUrl(url);

            var headers = ParseHttpHeaders(lines.Skip(1));
            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join(NewLine, bodyLines);

            return new HttpRequest
            {
                Method = method,
                Path = path,
                Headers = headers,
                Query = query,
                Body = body
            };
            
        }

        private static HttpMethod ParseHttpMethod(string method)
        {
            return method.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => throw new InvalidOperationException($"Method '{method}' is not supported")
            };

        }

        private static (string, Dictionary<string, string>) ParseUrl(string url)
        {
            var urlParts = url.Split('?');

            var path = urlParts[0];

            var query =  new Dictionary<string, string>();
            if (urlParts.Length > 1)
            {
                var queryParts = urlParts[1].Split("&");

                query = ParseQuery(queryParts);
            }
            
            return (path, query);

            
        }

        private static Dictionary<string, string> ParseQuery(string[] queryParts)
        {

            if (queryParts.Length == 0)
            {
                
            }
            var query = new Dictionary<string, string>();

            foreach (var part in queryParts)
            {
                var partsTokens = part.Split("=");

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

        private static HttpHeaderCollection ParseHttpHeaders(IEnumerable<string> headerLines)
        {
            var headers = new HttpHeaderCollection();

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
    }
}
