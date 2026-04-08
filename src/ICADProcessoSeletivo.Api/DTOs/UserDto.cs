namespace ICADProcessoSeletivo.Api.DTOs;

public record UserDto(int Id, string Username, string Name);
public record CreateUserDto(string Username, string Name, string Password);
public record UpdateUserDto(string Name, string? Password);
