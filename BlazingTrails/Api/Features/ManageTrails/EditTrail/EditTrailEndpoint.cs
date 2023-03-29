using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Api.Persistence.Entitities;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManageTrails.EditTrail;

public class EditTrailEndpoint : EndpointBaseAsync.WithRequest<EditTrailRequest>.WithActionResult<EditTrailRequest.Response>
{
    
    private readonly BlazingTrailsContext _dbContext;

    public EditTrailEndpoint(BlazingTrailsContext context)
    {
        _dbContext = context;
    }

    [HttpPut(EditTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<EditTrailRequest.Response>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var trail = await _dbContext.Trails.Include(x => x.Routes)
            .SingleOrDefaultAsync(x => x.Id.Equals(request.Trail.Id), cancellationToken);
        if (trail is null)
        {
            return BadRequest("Trail could not be found");
        }

        trail.Name = request.Trail.Name;
        trail.Description = request.Trail.Description;
        trail.Location = request.Trail.Location;
        trail.TimeInMinutes = request.Trail.TimeInMinutes;
        trail.Length = request.Trail.Length;
        trail.Routes = request.Trail.Routes.Select(ri => new RouteInstruction
        {
            Stage = ri.Stage,
            Description = ri.Description,
            Trail = trail
        }).ToList();

        if (request.Trail.ImageAction == ImageAction.Remove)
        {
            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image!));
            trail.Image = null;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(true);
    }
}