namespace BlazorClientes.DTOs;

public record ClienteResponse(
    int Id,
    string Nome,
    string Email,
    string Telefone,
    bool Ativo,
    DateTime DataCadastro);