using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails.Requests;

public record AddTrailRequest(TrailDto Trail) : IRequest<AddTrailRequest.Response>
{
    public const string RouteTemplate = "/api/trails";
    public record Response(int TrailId);
}