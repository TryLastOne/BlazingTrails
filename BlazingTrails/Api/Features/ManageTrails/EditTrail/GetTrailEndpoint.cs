using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.Home;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManageTrails.EditTrail;

public class GetTrailEndpoint : EndpointBaseAsync.WithRequest<int>.WithActionResult<GetTrailsRequest.Response>
{
    private readonly BlazingTrailsContext _dbContext;

    public GetTrailEndpoint(BlazingTrailsContext context)
    {
        _dbContext = context;
    }
    
    [HttpGet(GetTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailsRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = new CancellationToken())
    {
        var trail = await _dbContext.Trails.Include(trail => trail.Routes)
            .SingleOrDefaultAsync(x => x.Id.Equals(trailId), cancellationToken);

        if (trail is null)
        {
            return BadRequest("Trail could not be found.");
        }

        var response = new GetTrailRequest.Response(new GetTrailRequest.Trail(trail.Id, trail.Name, trail.Location, trail.Image, trail.TimeInMinutes, trail.Length, trail.Description, 
                trail.Routes.Select(route=> new GetTrailRequest.RouteInstruction(route.Id, route.Stage, route.Description))));
        return Ok(response);

    }
}