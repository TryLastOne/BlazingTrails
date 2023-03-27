using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazingTrails.Client;
using MediatR;

var builder = WebAssemblyHostBuilder.CreateDefault(args); //a type of host, Web assembly, an application run
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after"); //enable us to make modification to the head element

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7033") });
//Add MediatR to service collection and tell MediatR to scan the current assembly for request handlers.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

await builder.Build().RunAsync();

