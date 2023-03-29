using System.Net.Http.Json;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using MediatR;

namespace BlazingTrails.Client.Features.ManageTrails.EditTrail;

public class GetTrailRequestHandler : IRequestHandler<GetTrailRequest, GetTrailRequest.Response>
{
    private readonly HttpClient _httpClient;

    public GetTrailRequestHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetTrailRequest.Response?> Handle(GetTrailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<GetTrailRequest.Response>(GetTrailRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString()),  cancellationToken);
        }
        catch (Exception)
        {
            return default!;
        }
    }
}