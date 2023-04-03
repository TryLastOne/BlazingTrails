using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.Home.GetTrails;

public class GetTrailsEndpoint : EndpointBaseAsync.WithoutRequest
                                .WithActionResult<GetTrailsRequest.Response>
{
    private readonly BlazingTrailsContext _dbContext;

    public GetTrailsEndpoint(BlazingTrailsContext context)
    {
        _dbContext = context;
    }

    [HttpGet(GetTrailsRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailsRequest.Response>> HandleAsync(CancellationToken cancellationToken = new())
    {
        var trails = await _dbContext.Trails.AsQueryable().ToListAsync(cancellationToken: cancellationToken);
        if (!trails.Any()) return Ok(new GetTrailsRequest.Response(new List<GetTrailsRequest.Trail>()));
        var response = new GetTrailsRequest.Response(trails.Select(trail => new GetTrailsRequest.Trail
        (   trail.Id,
            trail.Name,
            trail.Image,
            trail.Location,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description)));
        return  Ok(response);

    }
}