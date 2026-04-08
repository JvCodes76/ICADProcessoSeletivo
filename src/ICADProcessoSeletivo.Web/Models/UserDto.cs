namespace ICADProcessoSeletivo.Web.Models;

public record UserDto(int Id, string Username, string Name);

public record LoginDto(string Username, string Password);

public record LoginResponseDto(string Token, string Username, string Name);
