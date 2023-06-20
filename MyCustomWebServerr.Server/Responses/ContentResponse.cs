﻿namespace MyCustomWebServer.Responses
{
    using Common;
    using Http;
    using System.Text;

    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string text, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            Headers.Add("Content-Type", contentType);
            Headers.Add("Content-Length", contentLength);

            Content = text;
        }
    }
}