namespace MyCustomWebServer.Http
{
    using MyCustomWebServer.Common;

    public class HttpSession
    {
        public const string SessionCookieName = "MyCustomWebServerSID";

        private Dictionary<string, string> data;

        public HttpSession(string id) 
        {
            Guard.AgainstNull(id, nameof(id));

            Id = id;
            data = new Dictionary<string, string>();
        }
        public string Id { get; init; } = null!;

        public bool IsNew { get; set; }

        public int Count => data.Count;

        public string this[string key]
        {
            get => data[key];
            set => data[key] = value;
        }
        public bool ContainsKey(string key)
            => data.ContainsKey(key);

        public void Remove(string key)
        {
            if (data.ContainsKey(key))
            {
                data.Remove(key);
            }
        }
    }
}
