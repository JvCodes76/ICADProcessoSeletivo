using ICADProcessoSeletivo.Web.Auth;
using ICADProcessoSeletivo.Web.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace ICADProcessoSeletivo.Web.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly CustomAuthStateProvider _authProvider;

    public AuthService(HttpClient http, AuthenticationStateProvider authProvider)
    {
        _http = http;
        _authProvider = (CustomAuthStateProvider)authProvider;
    }

    public async Task<(bool Success, string? Error)> LoginAsync(string username, string password)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", new LoginDto(username, password));
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (false, error?.Message ?? "Credenciais inválidas.");
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        if (result is null) return (false, "Erro ao processar resposta.");

        await _authProvider.MarkUserAsAuthenticated(result.Token);
        return (true, null);
    }

    public async Task LogoutAsync()
    {
        await _authProvider.MarkUserAsLoggedOut();
    }

    private record ErrorResponse(string Message);
}
