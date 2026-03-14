namespace GameStore.Api.Dtos;
//A DTO is  a contract that defines the shape of the data that is being passed between the client and the server
// a shared agreement  about how data  will be transferred and used
public record GameDto(
int id,
string name,
string Genre,
decimal price,
string ReleaseDate
);