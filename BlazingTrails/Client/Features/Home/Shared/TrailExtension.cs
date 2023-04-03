namespace BlazingTrails.Client.Features.Home.Shared;

public static class TrailExtension
{
    public static string ToImageUrl(this Trail self)
    {
        return !string.IsNullOrWhiteSpace(self.Image)
            ? $"{Constants.ImageUrl}/{self.Image}"
            : $"{Constants.ImagePlaceHolderUrl.Replace("{text_placeholder}", "No+Image+For+Trail")}";
    }
}