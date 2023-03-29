using Microsoft.Extensions.FileProviders;

namespace BlazingTrails.Api.Extensions;

public static class WebAppExtensions
{
    public static void ServeStaticFolder(this WebApplication? application, DirectoryInfo directoryInfo, string requestPath)
    {
        if (application is null) return;
        
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        application.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(directoryInfo.FullName),
            RequestPath = new PathString(requestPath.StartsWith("/") ? requestPath : requestPath.Insert(0, "/"))
        });
    }
}