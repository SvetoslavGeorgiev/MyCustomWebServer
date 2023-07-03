namespace MyCustomWebServer.Http
{
    using Common;

    public class HttpCookie
    {
        public HttpCookie(string name, string value)
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
