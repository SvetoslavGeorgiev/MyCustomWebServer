﻿namespace MyCustomWebServer.Http
{
    using MyCustomWebServer.Common;
    using System.Data;

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

        public int Count => data.Count;

        public string this[string key]
        {
            get => data[key];
            set => data[key] = value;
        }
        public bool ContainsKey(string key)
            => data.ContainsKey(key);
    }
}
