using System.Net.Http.Json;
using BlazingTrails.Shared.Features.Home;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;

namespace BlazingTrails.Client.Features.Home;

public class GetTrailsRequestHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response>
{
    private readonly HttpClient _httpClient;

    public GetTrailsRequestHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<GetTrailsRequest.Response> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(GetTrailsRequest.RouteTemplate,  cancellationToken);

        if (!response.IsSuccessStatusCode) return new GetTrailsRequest.Response(new List<TrailDto>());
        var trails = await response.Content.ReadFromJsonAsync<IEnumerable<TrailDto>>(cancellationToken: cancellationToken);
        
        return new GetTrailsRequest.Response(trails ?? new List<TrailDto>()) ;
    }
}