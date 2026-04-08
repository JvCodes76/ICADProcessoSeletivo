using ICADProcessoSeletivo.Web.Models;
using System.Net.Http.Json;

namespace ICADProcessoSeletivo.Web.Services;

public class UserService
{
    private readonly HttpClient _http;

    public UserService(HttpClient http) => _http = http;

    public async Task<List<UserDto>> GetAllAsync()
        => await _http.GetFromJsonAsync<List<UserDto>>("api/users") ?? [];

    public async Task<UserDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<UserDto>($"api/users/{id}");

    public async Task<(bool Success, string? Error)> CreateAsync(CreateUserDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/users", dto);
        if (response.IsSuccessStatusCode) return (true, null);
        return (false, await GetErrorMessage(response));
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(int id, UpdateUserDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/users/{id}", dto);
        if (response.IsSuccessStatusCode) return (true, null);
        return (false, await GetErrorMessage(response));
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/users/{id}");
        if (response.IsSuccessStatusCode) return (true, null);
        return (false, await GetErrorMessage(response));
    }

    private static async Task<string> GetErrorMessage(HttpResponseMessage response)
    {
        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            return "Você não tem permissão para realizar esta ação.";

        var body = await response.Content.ReadAsStringAsync();
        return string.IsNullOrWhiteSpace(body) ? "Ocorreu um erro inesperado." : body;
    }
}
