using ICADProcessoSeletivo.Web.Models;
using System.Net.Http.Json;

namespace ICADProcessoSeletivo.Web.Services;

public class UserService
{
    private readonly HttpClient _http;

    public UserService(HttpClient http) => _http = http;

    public async Task<List<UserDto>> GetAllAsync()
        => await _http.GetFromJsonAsync<List<UserDto>>("api/users") ?? [];
}
