using System.Net;
using System.Net.Http.Json;
using BlazorClientes.DTOs;

namespace BlazorClientes.Clients;

public class ClienteApiClient(HttpClient httpClient)
    : IClienteApiClient
{
    public async Task<IReadOnlyList<ClienteResponse>>
        PesquisarAsync(string? pesquisa)
    {
        var url = "api/clientes";

        if (!string.IsNullOrWhiteSpace(pesquisa))
        {
            url += $"?pesquisa={Uri.EscapeDataString(pesquisa)}";
        }

        var clientes = await httpClient
            .GetFromJsonAsync<List<ClienteResponse>>(url);

        return clientes ?? [];
    }

    public async Task<ClienteResponse?> ObterPorIdAsync(int id)
    {
        var response = await httpClient.GetAsync(
            $"api/clientes/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<ClienteResponse>();
    }

    public async Task<ClienteResponse> AdicionarAsync(
        ClienteRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(
            "api/clientes",
            request);

        await ValidarRespostaAsync(response);

        var cliente = await response.Content
            .ReadFromJsonAsync<ClienteResponse>();

        return cliente ??
            throw new InvalidOperationException(
                "A API não retornou o cliente cadastrado.");
    }

    public async Task AtualizarAsync(
        int id,
        ClienteRequest request)
    {
        var response = await httpClient.PutAsJsonAsync(
            $"api/clientes/{id}",
            request);

        await ValidarRespostaAsync(response);
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var response = await httpClient.DeleteAsync(
            $"api/clientes/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return false;

        await ValidarRespostaAsync(response);

        return true;
    }

    private static async Task ValidarRespostaAsync(
        HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        if (response.StatusCode == HttpStatusCode.Conflict)
        {
            var erro = await response.Content
                .ReadFromJsonAsync<ApiError>();

            throw new InvalidOperationException(
                erro?.Mensagem ?? "Ocorreu um conflito.");
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new InvalidOperationException(
                "Os dados informados são inválidos.");
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new InvalidOperationException(
                "Cliente não encontrado.");
        }

        response.EnsureSuccessStatusCode();
    }

    private sealed class ApiError
    {
        public string Mensagem { get; set; } = string.Empty;
    }
}
