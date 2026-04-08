using ICADProcessoSeletivo.Api.Data;
using ICADProcessoSeletivo.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Aplicar migrations e seed automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    // Garante que o admin sempre tem IsAdmin = true, mesmo em bancos já populados
    var existingAdmin = db.Users.FirstOrDefault(u => u.Username == "admin");
    if (existingAdmin != null && !existingAdmin.IsAdmin)
    {
        existingAdmin.IsAdmin = true;
        db.SaveChanges();
    }

    if (!db.Users.Any())
    {
        var admin = new User
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@ICAD!"),
            Name = "Administrador",
            IsAdmin = true
        };
        db.Users.Add(admin);

        var dev = new User
        {
            Username = "joao",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Joao@123"),
            Name = "João Silva"
        };
        db.Users.Add(dev);

        db.SaveChanges();

        var adminId = db.Users.First(u => u.Username == "admin").Id;
        var devId = db.Users.First(u => u.Username == "joao").Id;

        db.Tasks.AddRange(
            new TaskItem
            {
                Titulo = "Configurar ambiente de desenvolvimento",
                Descricao = "Instalar as ferramentas necessárias: Visual Studio, SQL Server, Node.js.",
                DataEntrega = DateTime.Now.AddDays(3),
                ResponsavelId = adminId,
                Dificuldade = "Fácil",
                Concluida = true
            },
            new TaskItem
            {
                Titulo = "Implementar autenticação JWT",
                Descricao = "Criar endpoint de login e proteger as rotas da API com JWT Bearer.",
                DataEntrega = DateTime.Now.AddDays(7),
                ResponsavelId = devId,
                Dificuldade = "Médio",
                Concluida = false
            },
            new TaskItem
            {
                Titulo = "Desenvolver tela de listagem de tarefas",
                Descricao = "Criar a página Blazor com accordion Bootstrap mostrando detalhes de cada tarefa.",
                DataEntrega = DateTime.Now.AddDays(10),
                ResponsavelId = devId,
                Dificuldade = "Difícil",
                Concluida = false
            }
        );
        db.SaveChanges();
    }
}

app.Run();
