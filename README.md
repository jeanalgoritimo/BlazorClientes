 
BlazorClientes
Aplicação web para gerenciamento de clientes desenvolvida com Blazor, ASP.NET Core, API REST e Entity Framework Core. O projeto demonstra uma arquitetura organizada, responsiva e preparada para utilizar PostgreSQL.

Visão geral
O BlazorClientes foi criado como um projeto prático para demonstrar conceitos modernos do ecossistema .NET, incluindo separação de responsabilidades, injeção de dependência, DTOs, Repository Pattern, Clean Code e princípios SOLID.

A aplicação disponibiliza uma interface responsiva para cadastrar, pesquisar, editar e excluir clientes. A tela Blazor consome uma API REST, que centraliza as regras de negócio e o acesso aos dados.

Funcionalidades
Cadastro de clientes em modal;

edição de informações;

exclusão com confirmação;

pesquisa automática por nome, e-mail ou telefone;

validação de campos obrigatórios;

validação de formato de e-mail;

controle de e-mails duplicados;

indicador de cliente ativo ou inativo;

mensagens de sucesso e erro;

interface responsiva;

menu lateral recolhível;

API REST para todas as operações do CRUD;

banco InMemory para desenvolvimento;

configuração preparada para PostgreSQL.

Tecnologias utilizadas
C#;

.NET 8;

Blazor Web App;

ASP.NET Core;

API REST;

Entity Framework Core;

PostgreSQL;

Entity Framework Core InMemory;

Bootstrap;

HTML e CSS;

Git e GitHub.

Arquitetura
O projeto utiliza separação em camadas:

Tela Blazor
    ↓
ClienteApiClient
    ↓ HTTP
ClientesController
    ↓
ClienteService
    ↓
ClienteRepository
    ↓
Entity Framework Core
    ↓
InMemory ou PostgreSQL
Responsabilidades
Camada	Responsabilidade
Components	Interface Blazor e interação com o usuário
Clients	Comunicação HTTP entre o Blazor e a API
Controllers	Recebimento das requisições e respostas HTTP
Services	Regras de negócio da aplicação
Repositories	Consultas e persistência dos dados
Data	Configuração do Entity Framework Core
DTOs	Contratos de entrada e saída da API
Models	Entidades do domínio
Estrutura do projeto
BlazorClientes
├── Clients
│   ├── ClienteApiClient.cs
│   └── IClienteApiClient.cs
├── Components
│   ├── Layout
│   │   ├── MainLayout.razor
│   │   ├── MainLayout.razor.css
│   │   ├── NavMenu.razor
│   │   └── NavMenu.razor.css
│   └── Pages
│       ├── About.razor
│       ├── Clientes.razor
│       └── Home.razor
├── Controllers
│   └── ClientesController.cs
├── Data
│   └── AppDbContext.cs
├── DTOs
│   ├── ClienteRequest.cs
│   └── ClienteResponse.cs
├── Models
│   └── Cliente.cs
├── Repositories
│   ├── ClienteRepository.cs
│   └── IClienteRepository.cs
├── Services
│   ├── ClienteService.cs
│   └── IClienteService.cs
├── appsettings.json
├── appsettings.Development.json
└── Program.cs
Endpoints da API
Método	Endpoint	Descrição
GET	/api/clientes	Lista todos os clientes
GET	/api/clientes?pesquisa=Jean	Pesquisa clientes
GET	/api/clientes/{id}	Consulta um cliente pelo ID
POST	/api/clientes	Cadastra um cliente
PUT	/api/clientes/{id}	Atualiza um cliente
DELETE	/api/clientes/{id}	Exclui um cliente
Exemplo de cadastro
{
  "nome": "Jean Silva",
  "email": "jean@email.com",
  "telefone": "(27) 99999-9999",
  "ativo": true
}
Como executar
Pré-requisitos
Visual Studio 2022 atualizado;

SDK do .NET 8;

PostgreSQL opcional, pois o projeto pode utilizar o banco InMemory.

Execução com banco InMemory
Clone o repositório:

git clone https://github.com/jeanalgoritimo/BlazorClientes.git
Acesse a pasta do projeto:

cd BlazorClientes
Restaure as dependências:

dotnet restore
Confirme a configuração em appsettings.Development.json:

{
  "DatabaseProvider": "InMemory"
}
Execute a aplicação:

dotnet run --project BlazorClientes/BlazorClientes.csproj
Configuração do PostgreSQL
Altere appsettings.Development.json:

{
  "DatabaseProvider": "PostgreSql",
  "ConnectionStrings": {
    "PostgreSql": "Host=localhost;Port=5432;Database=blazor_clientes;Username=postgres;Password=SUA_SENHA"
  }
}
Crie e aplique a migration:

dotnet ef migrations add InitialCreate --project BlazorClientes
dotnet ef database update --project BlazorClientes
Não publique senhas reais no GitHub. Em ambientes reais, utilize variáveis de ambiente, User Secrets ou um cofre de segredos.

Boas práticas aplicadas
Separação de responsabilidades;

Dependency Injection;

Repository Pattern;

DTOs para entrada e saída da API;

consultas assíncronas;

AsNoTracking em consultas somente de leitura;

validação com Data Annotations;

normalização de e-mail;

códigos HTTP apropriados;

tratamento de erros;

CSS Isolation nos componentes Blazor;

interface responsiva;

princípios Clean Code e SOLID.

Fluxo Git sugerido
git checkout -b feature/nova-funcionalidade
git add .
git commit -m "feat: adiciona nova funcionalidade"
git push origin feature/nova-funcionalidade
Depois do push, abra um Pull Request para revisão antes de integrar a alteração à branch principal.

Melhorias futuras
Testes unitários com xUnit e Moq;

testes de integração da API;

paginação dos clientes;

autenticação e autorização;

documentação com Swagger/OpenAPI;

execução do PostgreSQL com Docker Compose;

logs estruturados;

pipeline de integração contínua;

publicação em ambiente de nuvem.

Autor
Jean Paiva da Silva

Desenvolvedor de software com experiência em C#, .NET, ASP.NET Core, APIs REST, bancos de dados, integrações e modernização de aplicações.

LinkedIn

GitHub

Licença
Este projeto está disponível para fins de estudo e demonstração técnica.

