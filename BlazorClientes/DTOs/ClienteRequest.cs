using System.ComponentModel.DataAnnotations;

namespace BlazorClientes.DTOs;

public class ClienteRequest
{
    [Required(ErrorMessage = "Informe o nome.")]
    [StringLength(
        100,
        MinimumLength = 3,
        ErrorMessage = "O nome deve possuir entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o e-mail.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o telefone.")]
    public string Telefone { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;
}