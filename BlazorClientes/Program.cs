using BlazorClientes.Clients;
using BlazorClientes.Components;
using BlazorClientes.Data;
using BlazorClientes.Repositories;
using BlazorClientes.Services;
using Microsoft.EntityFrameworkCore;

namespace BlazorClientes;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddControllers();

        var databaseProvider =
            builder.Configuration["DatabaseProvider"]
            ?? "InMemory";

        if (databaseProvider.Equals(
                "PostgreSql",
                StringComparison.OrdinalIgnoreCase))
        {
            var connectionString = builder.Configuration
                .GetConnectionString("PostgreSql");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "A conexão PostgreSql não foi configurada.");
            }

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));
        }
        else
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(
                    "BlazorClientesDb"));
        }

        builder.Services.AddScoped<
            IClienteRepository,
            ClienteRepository>();

        builder.Services.AddScoped<
            IClienteService,
            ClienteService>();

        /*
         * Registra o cliente HTTP utilizado pela tela Blazor.
         *
         * A tela injeta IClienteApiClient e esta implementação
         * utiliza HttpClient para chamar ClientesController.
         */
        builder.Services.AddHttpClient<
            IClienteApiClient,
            ClienteApiClient>(httpClient =>
            {
                var apiBaseUrl =
            builder.Configuration["ApiBaseUrl"]
            ?? "https://localhost:7089/";

                httpClient.BaseAddress = new Uri(apiBaseUrl);
            });
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();

        // Mapeia os Controllers da API.
        app.MapControllers();

        // Mapeia os componentes Blazor.
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}