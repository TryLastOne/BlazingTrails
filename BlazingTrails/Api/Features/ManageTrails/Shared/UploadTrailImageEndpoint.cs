using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManageTrails.Shared;

public class UploadTrailImageEndpoint : EndpointBaseAsync.WithRequest<int>
                                        .WithActionResult<string>
{
    private readonly BlazingTrailsContext _dbContext;

    public UploadTrailImageEndpoint(BlazingTrailsContext context)
    {
        _dbContext = context;
    }
    
    [HttpPost(UploadTrailImageRequest.RouteTemplate)]
    public override async Task<ActionResult<string>> HandleAsync([FromRoute] int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await _dbContext.Trails.SingleOrDefaultAsync(trail => trail.Id.Equals(trailId), cancellationToken);
        if (trail is null)
        {
            return BadRequest("Trail does not exist.");
        }

        var file = Request.Form.Files[0];
        if (file.Length == 0)
        {
            return BadRequest("No image found.");
        }

        var fileName = $"{Guid.NewGuid()}.jpg";
        var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);

        var resizeOption = new ResizeOptions
        {
            Mode = ResizeMode.Pad,
            Size = new Size(640, 426)
        };

        using var image = await Image.LoadAsync(file.OpenReadStream(), cancellationToken);
        image.Mutate(x => x.Resize(resizeOption));
        await image.SaveAsJpegAsync(saveLocation, cancellationToken);

        if (!string.IsNullOrWhiteSpace(trail.Image))
        {
            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image));
        }
        
        trail.Image = fileName;
        await _dbContext.SaveChangesAsync(cancellationToken);
        

        return Ok(trail.Image);

    }
}