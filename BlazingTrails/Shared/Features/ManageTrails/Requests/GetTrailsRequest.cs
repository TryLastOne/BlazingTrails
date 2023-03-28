using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails.Requests;

public record GetTrailsRequest : IRequest<GetTrailsRequest.Response>
{
    public const string RouteTemplate = "/api/trails";

    public record Response(IEnumerable<TrailDto> Trails);
}