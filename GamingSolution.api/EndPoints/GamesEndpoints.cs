
using GameStore.Api.Dtos;

public static class GamesEndpoints
{

    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games =
[
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
];

public static void MapGamesEndPoints(this WebApplication app)
    {
        var group=app.MapGroup("/games");
        app.MapGet("/", () => "Hello World!");
        //GET /games
        group.MapGet("/", () => games);
        //Get /games/1
        group.MapGet("/{id}", (int id) =>
        // games.Find(game => game.id == id)
        {
            var game = games.Find(game => game.id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }
        ).WithName(GetGameEndpointName);

        //Post/games
        group.MapPost("/", (GamingSolution.api.Dtos.CreateGameDto newGame) =>
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
        group.MapPut("/games/{id}", (int id, GamingSolution.api.Dtos.UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.id == id);
            if (index == -1)
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


    }

}
