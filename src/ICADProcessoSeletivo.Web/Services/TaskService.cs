using ICADProcessoSeletivo.Web.Models;
using System.Net.Http.Json;

namespace ICADProcessoSeletivo.Web.Services;

public class TaskService
{
    private readonly HttpClient _http;

    public TaskService(HttpClient http) => _http = http;

    public async Task<List<TaskDto>> GetAllAsync(string? search = null)
    {
        var url = string.IsNullOrWhiteSpace(search)
            ? "api/tasks"
            : $"api/tasks?search={Uri.EscapeDataString(search)}";

        return await _http.GetFromJsonAsync<List<TaskDto>>(url) ?? [];
    }

    public async Task<TaskDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<TaskDto>($"api/tasks/{id}");

    public async Task<bool> CreateAsync(CreateTaskDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/tasks", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(int id, UpdateTaskDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/tasks/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ToggleConcluidaAsync(int id, bool concluida)
    {
        var response = await _http.PatchAsJsonAsync($"api/tasks/{id}/concluida", concluida);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/tasks/{id}");
        return response.IsSuccessStatusCode;
    }
}
