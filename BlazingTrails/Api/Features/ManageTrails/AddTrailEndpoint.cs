using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Api.Persistence.Entitities;
using BlazingTrails.Shared.Features.ManageTrails.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BlazingTrails.Api.Features.ManageTrails;

public class AddTrailEndpoint : EndpointBaseAsync.WithRequest<AddTrailRequest>
                                                 .WithActionResult<int>
{
    private readonly BlazingTrailsContext _dbContext;

    public AddTrailEndpoint(BlazingTrailsContext context)
    {
        _dbContext = context;
    }
    
    [HttpPost(AddTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<int>> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = new())
    {
        var trail = new Trail
        {
            Name = request.Trail.Name,
            Description = request.Trail.Description,
            Location = request.Trail.Location,
            TimeInMinutes = request.Trail.TimeInMinutes,
            Length = request.Trail.Length
        };

        await _dbContext.Trails.AddAsync(trail, cancellationToken);

        var routeInstructions = request.Trail.Routes.Select(x => new RouteInstruction
        {
            Stage = x.Stage,
            Description = x.Description,
            Trail = trail
        });

        await _dbContext.RouteInstructions.AddRangeAsync(routeInstructions, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(trail.Id);
    }
}