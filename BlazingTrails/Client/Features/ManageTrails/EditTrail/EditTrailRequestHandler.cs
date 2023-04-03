using System.Net.Http.Json;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using MediatR;

namespace BlazingTrails.Client.Features.ManageTrails.EditTrail;

public class EditTrailRequestHandler : IRequestHandler<EditTrailRequest, EditTrailRequest.Response>
{
    private readonly HttpClient _httpClient;

    public EditTrailRequestHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<EditTrailRequest.Response> Handle(EditTrailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(EditTrailRequest.RouteTemplate.Replace("{trailId}", request.Trail.Id.ToString()), request, cancellationToken);
            return response.IsSuccessStatusCode ? new EditTrailRequest.Response(true) : new EditTrailRequest.Response(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            
        }

        return new EditTrailRequest.Response(false);

    }
}