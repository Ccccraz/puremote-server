using Microsoft.AspNetCore.Mvc;
using puremote_server;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

var endpointDataStore = new Dictionary<string, List<JsonElement>>();

// Register endpoint for adding new GET endpoints
app.MapPost("/register", (EndpointModel model, IEndpointRouteBuilder endpoints) =>
{
    try
    {
        if (string.IsNullOrEmpty(model.Endpoint))
        {
            TypedResults.BadRequest("Endpoint valid or already registered");
            return;
        }

        AddDynamicEndpoints(model.Endpoint, endpoints);

        TypedResults.Ok($"Endpoint /{model.Endpoint} registered");
    }
    catch (Exception ex)
    {
        TypedResults.Problem(ex.Message);
    }
});


void AddDynamicEndpoints(string endpoint, IEndpointRouteBuilder endpoints)
{
    endpoints.MapGet($"/{endpoint}", () =>
    {
        foreach (var data in endpointDataStore[endpoint])
        {
            TypedResults.Ok(data);
        }
    });

    endpoints.MapPost($"/{endpoint}", ([FromBody] JsonElement data) =>
    {
        if (!endpointDataStore.ContainsKey(endpoint))
        {
            endpointDataStore.TryAdd(endpoint, new List<JsonElement>());
        }

        endpointDataStore[endpoint].Add(data);

        TypedResults.Ok($"数据已保存到端点 /{endpoint}");
    });
}

app.Run();
