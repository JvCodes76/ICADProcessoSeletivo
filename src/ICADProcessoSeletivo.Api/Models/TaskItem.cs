namespace ICADProcessoSeletivo.Api.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataEntrega { get; set; }
    public int ResponsavelId { get; set; }
    public User Responsavel { get; set; } = null!;
    public string Dificuldade { get; set; } = string.Empty;
    public bool Concluida { get; set; } = false;
}
