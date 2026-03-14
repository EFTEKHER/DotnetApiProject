using GameStore.Api.Dtos;
using GamingSolution.api.Dtos;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGamesEndPoints();
app.Run();
