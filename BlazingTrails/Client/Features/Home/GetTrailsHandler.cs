using System.Net.Http.Json;
using BlazingTrails.Shared.Features.Home;
using MediatR;

namespace BlazingTrails.Client.Features.Home;

public class GetTrailsRequestHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response?>
{
    private readonly HttpClient _httpClient;

    public GetTrailsRequestHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<GetTrailsRequest.Response?> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.GetAsync(GetTrailsRequest.RouteTemplate,  cancellationToken);

            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<GetTrailsRequest.Response>(cancellationToken: cancellationToken);
        }
        catch (Exception)
        {
            return default!;
        }
       
    }
}