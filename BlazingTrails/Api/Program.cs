using BlazingTrails.Api.Extensions;
using BlazingTrails.Api.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

//Add Cors policy

const string corsNamePolicy = "BlazingTrailsCors";

builder.Services.AddCors(options =>
{
 if (builder.Environment.IsDevelopment())
 {
  options.AddPolicy(name: corsNamePolicy, policy =>
  {
   policy.WithHeaders("*");
   policy.WithOrigins("https://localhost:7013");
  } );
 }
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("BlazingTrailsContext");
builder.Services.AddDbContext<BlazingTrailsContext>(options => options.UseSqlite(connectionString));
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    //enables the debugging of Blazor WebAssembly code while API project as the start-up project
    app.UseWebAssemblyDebugging(); //For Blazor Assembly configuration
}

app.UseHttpsRedirection();

/*
 * For Blazor Assembly configuration
 */

//enables the API to serve the Blazor application
app.UseBlazorFrameworkFiles();

//enables static files to be served by API
app.UseStaticFiles();

//Serve Images folder to client as static file
var imageDir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Images"));
app.ServeStaticFolder(imageDir, "Images");

app.UseRouting();

/*
 *  End
 */

/*
 * Use Cors for API
 */

app.UseCors(corsNamePolicy);

//app.UseAuthorization();
app.MapControllers();

/*
 * For Blazor Assembly configuration
 */

// if a request doesn't match to a controller, serve the index.html from Blazor project.
app.MapFallbackToFile("index.html");

/*
 *  End
 */

app.Run();