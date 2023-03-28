using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.ManageTrails;
using BlazingTrails.Shared.Features.ManageTrails.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManageTrails;

public class GetTrailsEndpoint : EndpointBaseAsync.WithRequest<GetTrailsRequest>
                                .WithActionResult<IEnumerable<TrailDto>>
{
    private readonly BlazingTrailsContext _dbContext;

    public GetTrailsEndpoint(BlazingTrailsContext context)
    {
        _dbContext = context;
    }
    
    [HttpGet(GetTrailsRequest.RouteTemplate)]
    public override async Task<ActionResult<IEnumerable<TrailDto>>> HandleAsync(GetTrailsRequest request,
        CancellationToken cancellationToken = new())
    {
        var response = new List<TrailDto>();
        var trails = await _dbContext.Trails.AsQueryable()
                                                     .ToListAsync(cancellationToken: cancellationToken);
        if (trails.Any())
        {
            response.AddRange(trails.Select(trail => new TrailDto
            {
                Id = trail.Id,
                Name = trail.Name,
                Description = trail.Description,
                Location = trail.Location,
                Length = trail.Length,
                TimeInMinutes = trail.TimeInMinutes,
                Routes = trail.Routes.Select(route => new RouteInstructionDto
                {
                    Id = route.Id,
                    Stage = route.Stage,
                    Description = route.Description
                }).ToList()
            }));
        }

        return Ok(response);
    }

}