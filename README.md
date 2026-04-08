# ICAD - Gerenciador de Tarefas

Projeto desenvolvido para o processo seletivo do ICAD. Aplicação full-stack de gerenciamento de tarefas com autenticação JWT.

## Stack

- **Frontend:** Blazor WebAssembly (.NET 8)
- **Backend:** ASP.NET Core Web API (.NET 8)
- **Banco de Dados:** Microsoft SQL Server
- **ORM:** Entity Framework Core 8
- **Autenticação:** JWT Bearer Token
- **UI:** Bootstrap 5 + Bootstrap Icons

## Funcionalidades

- Login com autenticação via banco de dados (JWT)
- Proteção de rotas (usuário deslogado é redirecionado para o login)
- Listagem de tarefas com accordion Bootstrap
- Filtro de busca por título (com debounce)
- Checkbox para marcar tarefa como concluída
- Tags de dificuldade (Fácil / Médio / Difícil)
- Adicionar, editar e excluir tarefas
- Navbar responsiva com botão de logout em vermelho

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou SQL Server Express / LocalDB)

## Como Executar

### 1. Configurar o Banco de Dados

Edite a connection string em `src/ICADProcessoSeletivo.Api/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ICADTarefas;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> Para usar LocalDB: `Server=(localdb)\\mssqllocaldb;Database=ICADTarefas;Trusted_Connection=True;`

### 2. Executar a API

```bash
cd src/ICADProcessoSeletivo.Api
dotnet run
```

A API será iniciada em `http://localhost:5000`. As migrations e o seed de dados serão aplicados automaticamente.

### 3. Executar o Frontend

```bash
cd src/ICADProcessoSeletivo.Web
dotnet run
```

Acesse `http://localhost:5176` (ou a porta exibida no terminal).

## Credenciais de Acesso

| Usuário | Senha       |
|---------|-------------|
| admin   | Admin@ICAD! |
| joao    | Joao@123    |

## Estrutura do Projeto

```
src/
├── ICADProcessoSeletivo.Api/       # Web API (.NET 8)
│   ├── Controllers/                # AuthController, TasksController, UsersController
│   ├── Data/                       # AppDbContext + Migrations
│   ├── DTOs/                       # Data Transfer Objects
│   └── Models/                     # User, TaskItem
└── ICADProcessoSeletivo.Web/       # Blazor WebAssembly
    ├── Auth/                       # CustomAuthStateProvider
    ├── Layout/                     # NavMenu, MainLayout
    ├── Models/                     # DTOs do cliente
    ├── Pages/                      # Login, Tasks, AddTask, EditTask
    └── Services/                   # AuthService, TaskService, UserService
```
