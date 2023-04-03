using BlazingTrails.Shared.Features.ManageTrails.Shared;

namespace BlazingTrails.Client.Features.ManageTrails.Shared;

public static class TrailDtoExtension
{
    public static string ToImageUrl(this TrailDto self)
    {
        return !string.IsNullOrWhiteSpace(self.Image)
            ? $"{Constants.ImageUrl}/{self.Image}"
            : $"{Constants.ImagePlaceHolderUrl.Replace("{text_placeholder}", "No+Image+For+Trail")}";
    }
}