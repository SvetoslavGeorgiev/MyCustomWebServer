namespace MyCustomWebServer.Http
{
    using Common;

    public class HttpHeader
    {
        public const string ContentType = "Content-Type";
        public const string ContentLength = "Content-Length";
        public const string Cookie = "Cookie";
        public const string Date = "Date";
        public const string Location = "Location";
        public const string Server = "Server";
        public const string SetCookie = "Set-Cookie";

        public HttpHeader(string name, string value)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(value, nameof(value));

            Name = name; 
            Value = value;
        }
        public string Name { get; init; } = null!;
        public string Value { get; init; } = null!;

        public override string ToString()
            => $"{Name}: {Value}";
    }
}
