﻿namespace MyCustomWebServer.App
{
    using System.Threading.Tasks;
    using MyCustomWebServer.App.Controllers;
    using MyCustomWebServer;
    using MyCustomWebServer.Controllers;

    public class StartUp
    {
        static async Task Main(string[] args)
            => await new HttpServer(routes => routes
            .MapGet<HomeController>("/", c => c.Index())
            .MapGet<HomeController>("/Softuni", c => c.ToSoftUni())
            .MapGet<HomeController>("/ToDogs", c => c.LocalRedirect())
            .MapGet<AnimalsContreller>("/Cats", c => c.Cats())
            .MapGet<AnimalsContreller>("/Dogs", c => c.Dogs()))
            .Start();
    }
}