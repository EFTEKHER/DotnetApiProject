using GameStore.Api.Dtos;
using GamingSolution.api.Dtos;

const string GetGameEndpointName = "GetGame";
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = new List<GameDto>
{
    new GameDto(1, "Street Fighter", "Fighting", 19.99M, new DateTime(1972, 7, 15).ToString("yyyy-MM-dd")),
    new GameDto(2, "Mortal Kombat", "Fighting", 19.99M, new DateTime(1972, 7, 15).ToString("yyyy-MM-dd")),
    new GameDto(3, "Super Smash Bros", "Fighting", 19.99M, new DateTime(1972, 7, 15).ToString("yyyy-MM-dd")),
    new GameDto(4, "Pokemon", "RPG", 19.99M, new DateTime(1972, 7, 15).ToString("yyyy-MM-dd")),
    new GameDto(5, "Pokemon", "RPG", 19.99M, new DateTime(1972, 7, 15).ToString("yyyy-MM-dd")),
    new GameDto(6, "Pokemon", "RPG", 19.99M, new DateTime(1972, 7, 15).ToString("yyyy-MM-dd")),
    new GameDto(7, "Pokemon", "RPG", 19.99M, new DateTime(1972, 7, 15).ToString("yyyy-MM-dd")),
    new GameDto(8, "Pokemon", "RPG", 19.99M, new DateTime(1972, 7, 15).ToString("yyyy-MM-dd")),
    new GameDto(9, "Pokemon", "RPG", 19.99M, new DateTime(1972, 7, 15).ToString("yyyy-MM-dd")),
    new GameDto(10, "Pokemon", "RPG", 19.99M, new DateTime(1972, 7, 15).ToString("yyyy-MM-dd"))
};

app.MapGet("/", () => "Hello World!");
//GET /games
app.MapGet("/games", () => games);
//Get /games/1
app.MapGet("/games/{id}", (int id) =>
// games.Find(game => game.id == id)
{
    var game = games.Find(game => game.id == id); 
    return game is null ? Results.NotFound() : Results.Ok(game);
}
).WithName(GetGameEndpointName);

//Post/games
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        id: games.Count + 1,
        name: newGame.name,
        Genre: newGame.genre,
        price: newGame.price,
        ReleaseDate: newGame.releaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.id }, game);
});
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(game => game.id == id);
    if(index == -1)
    {
        return Results.NotFound();
    } 
    games[index] = new GameDto(
    id,

updatedGame.Name,
updatedGame.Genre,
updatedGame.Price,
updatedGame.ReleaseDate
    );

    return Results.NoContent();
});

//Delete / games /1
app.MapDelete("/games/{id}", (int id) =>
{
    games.RemoveAll(game => game.id == id);
    return Results.NoContent();
});


app.Run();
