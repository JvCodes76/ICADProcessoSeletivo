namespace ICADProcessoSeletivo.Web.Models;

public class TaskDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataEntrega { get; set; }
    public int ResponsavelId { get; set; }
    public string ResponsavelNome { get; set; } = string.Empty;
    public string Dificuldade { get; set; } = string.Empty;
    public string Status { get; set; } = "Backlog";
}

public class CreateTaskDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataEntrega { get; set; } = DateTime.Today.AddDays(7);
    public int ResponsavelId { get; set; }
    public string Dificuldade { get; set; } = string.Empty;
}

public class UpdateTaskDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataEntrega { get; set; }
    public int ResponsavelId { get; set; }
    public string Dificuldade { get; set; } = string.Empty;
    public string Status { get; set; } = "Backlog";
}
