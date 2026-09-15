using BlazorClientes.DTOs;
using BlazorClientes.Models;
using BlazorClientes.Repositories;

namespace BlazorClientes.Services;

public class ClienteService(IClienteRepository repository)
    : IClienteService
{
    public async Task<IReadOnlyList<ClienteResponse>> PesquisarAsync(
        string? pesquisa)
    {
        var clientes =
            await repository.PesquisarAsync(pesquisa);

        return clientes
            .Select(Mapear)
            .ToList();
    }

    public async Task<ClienteResponse?> ObterPorIdAsync(int id)
    {
        var cliente = await repository.ObterPorIdAsync(id);

        return cliente is null
            ? null
            : Mapear(cliente);
    }

    public async Task<ClienteResponse> AdicionarAsync(
        ClienteRequest request)
    {
        var email = NormalizarEmail(request.Email);

        if (await repository.EmailExisteAsync(email))
        {
            throw new InvalidOperationException(
                "Já existe um cliente com esse e-mail.");
        }

        var cliente = new Cliente
        {
            Nome = request.Nome.Trim(),
            Email = email,
            Telefone = request.Telefone.Trim(),
            Ativo = request.Ativo,
            DataCadastro = DateTime.UtcNow
        };

        await repository.AdicionarAsync(cliente);

        return Mapear(cliente);
    }

    public async Task<bool> AtualizarAsync(
        int id,
        ClienteRequest request)
    {
        var cliente = await repository.ObterPorIdAsync(id);

        if (cliente is null)
            return false;

        var email = NormalizarEmail(request.Email);

        if (await repository.EmailExisteAsync(email, id))
        {
            throw new InvalidOperationException(
                "Já existe outro cliente com esse e-mail.");
        }

        cliente.Nome = request.Nome.Trim();
        cliente.Email = email;
        cliente.Telefone = request.Telefone.Trim();
        cliente.Ativo = request.Ativo;

        await repository.AtualizarAsync(cliente);

        return true;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var cliente = await repository.ObterPorIdAsync(id);

        if (cliente is null)
            return false;

        await repository.ExcluirAsync(cliente);

        return true;
    }

    private static string NormalizarEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }

    private static ClienteResponse Mapear(Cliente cliente)
    {
        return new ClienteResponse(
            cliente.Id,
            cliente.Nome,
            cliente.Email,
            cliente.Telefone,
            cliente.Ativo,
            cliente.DataCadastro);
    }
}