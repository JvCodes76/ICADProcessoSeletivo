# ICAD Gerenciador de Tarefas

Aplicação web full-stack de gerenciamento de tarefas desenvolvida para o processo seletivo do ICAD.

## Stack

| Camada | Tecnologia |
|--------|-----------|
| Frontend | Blazor WebAssembly (.NET 9) |
| Backend | ASP.NET Core Web API (.NET 9) |
| Banco de dados | Microsoft SQL Server |
| ORM | Entity Framework Core 9 |
| Autenticação | JWT Bearer + BCrypt |
| UI | Bootstrap 5 + Bootstrap Icons |

## Funcionalidades

- Login com autenticação JWT via banco de dados
- Proteção de rotas — usuários não autenticados são redirecionados ao login
- Controle de acesso por perfil (admin / usuário comum) com mensagem de acesso negado
- **Lista de Tarefas** — accordion Bootstrap, busca por título com debounce, checkbox de conclusão, badges de dificuldade e status de atraso
- **Adicionar / Editar / Excluir Tarefas** — título, descrição, data de entrega, responsável e dificuldade
- **Equipe** *(somente admin)* — cadastro, edição e exclusão de membros da equipe
- Navbar responsiva com botão de logout em vermelho

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server 2019+](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (edição Developer ou Express)
- dotnet-ef CLI:

```bash
dotnet tool install --global dotnet-ef
```

## Como Executar

### 1. Configurar a chave JWT

A chave JWT é armazenada fora do repositório via `dotnet user-secrets`. Configure antes de rodar:

```bash
cd src/ICADProcessoSeletivo.Api
dotnet user-secrets set "Jwt:Key" "ICADProcessoSeletivoSuperSecretKey2024!@#"
```

### 2. Configurar o banco de dados

A connection string padrão usa autenticação do Windows com SQL Server local:

```
Server=localhost;Database=ICADTarefas;Trusted_Connection=True;TrustServerCertificate=True;
```

Para alterar, edite `src/ICADProcessoSeletivo.Api/appsettings.Development.json`.

> Para usar LocalDB: `Server=(localdb)\mssqllocaldb;Database=ICADTarefas;Trusted_Connection=True;TrustServerCertificate=True;`

### 3. Aplicar as migrations

```bash
cd src/ICADProcessoSeletivo.Api
dotnet ef database update
```

O banco `ICADTarefas` e os dados iniciais são criados automaticamente.

### 4. Executar a aplicação

Abra dois terminais a partir da raiz do projeto:

**Terminal 1 — API (porta 5000):**
```bash
cd src/ICADProcessoSeletivo.Api
dotnet run --launch-profile http
```

**Terminal 2 — Frontend (porta 5131):**
```bash
cd src/ICADProcessoSeletivo.Web
dotnet run --launch-profile http
```

Acesse **http://localhost:5131**

## Credenciais de Acesso

| Usuário | Senha | Perfil |
|---------|-------|--------|
| admin | Admin@ICAD! | Administrador |
| joao | Joao@123 | Usuário comum |

## Estrutura do Projeto

```
src/
├── ICADProcessoSeletivo.Api/       # Web API (.NET 9)
│   ├── Controllers/                # AuthController, TasksController, UsersController
│   ├── Data/                       # AppDbContext
│   ├── DTOs/                       # Data Transfer Objects
│   ├── Migrations/                 # EF Core Migrations
│   └── Models/                     # User, TaskItem
└── ICADProcessoSeletivo.Web/       # Blazor WebAssembly (.NET 9)
    ├── Auth/                       # CustomAuthStateProvider (JWT)
    ├── Layout/                     # NavMenu, MainLayout
    ├── Models/                     # DTOs do cliente
    ├── Pages/                      # Login, Tasks, AddTask, EditTask, Users, AddUser, EditUser
    └── Services/                   # AuthService, TaskService, UserService
```
