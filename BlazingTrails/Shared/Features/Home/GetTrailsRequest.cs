using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;

namespace BlazingTrails.Shared.Features.Home;

public record GetTrailsRequest : IRequest<GetTrailsRequest.Response>
{
    public const string RouteTemplate = "/api/trails";

    public record Response(IEnumerable<TrailDto> Trails);
}