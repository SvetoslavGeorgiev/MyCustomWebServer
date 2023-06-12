namespace MyCustomWebServer.Server.Http
{
    using System.Collections;

    public class HttpHeaderCollection : IEnumerable<HttpHeader>
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
            => headers = new Dictionary<string, HttpHeader>();

        public int Count => headers.Count;

        public void Add(string name, string value)
        {
            var header = new HttpHeader(name, value);

            headers.Add(name, header);
        }

        public IEnumerator<HttpHeader> GetEnumerator()
            => headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
