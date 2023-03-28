using System.Net.Http.Json;
using BlazingTrails.Shared.Features.ManageTrails;
using BlazingTrails.Shared.Features.ManageTrails.Requests;
using MediatR;

namespace BlazingTrails.Client.Features.Home;

public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response>
{
    private readonly HttpClient _httpClient;

    public GetTrailsHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<GetTrailsRequest.Response> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(GetTrailsRequest.RouteTemplate,  cancellationToken);

        if (!response.IsSuccessStatusCode) return new GetTrailsRequest.Response(new List<TrailDto>());
        var trails = await response.Content.ReadFromJsonAsync<IEnumerable<TrailDto>>(cancellationToken: cancellationToken);
        return new GetTrailsRequest.Response(trails);
    }
}