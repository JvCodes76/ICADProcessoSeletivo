namespace ICADProcessoSeletivo.Api.DTOs;

public record LoginDto(string Username, string Password);

public record LoginResponseDto(string Token, string Username, string Name);
