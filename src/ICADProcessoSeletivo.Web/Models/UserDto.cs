namespace ICADProcessoSeletivo.Web.Models;

public record UserDto(int Id, string Username, string Name);
public record CreateUserDto(string Username, string Name, string Password);
public record UpdateUserDto(string Name, string? Password);

public record LoginDto(string Username, string Password);

public record LoginResponseDto(string Token, string Username, string Name);
