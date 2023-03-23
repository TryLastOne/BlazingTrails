using BlazingTrails.Api.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("BlazingTrailsContext");
builder.Services.AddDbContext<BlazingTrailsContext>(options => options.UseSqlite(connectionString));
builder.Services.AddControllers();

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
app.UseRouting();

/*
 *  End
 */

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