using FluentValidation;
using MeuMenu.Domain.Enums;
using MeuMenu.Domain.Interfaces.Services;

namespace MeuMenu.Domain.Validations.Usuario;

public class AdicionarUsuarioValidation : AbstractValidator<Models.Usuario.Usuario>
{
    private readonly IUsuarioService _usuarioService;

    public AdicionarUsuarioValidation(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
        ValidarCamposObrigatorios();
        VerificarLoginUnico();
    }

    private void VerificarLoginUnico()
    {
        RuleFor(x => x.UsuarioLogin)
            .MustAsync(async (x, _) =>
            {
                var usuario = await _usuarioService.Obter(y => y.UsuarioLogin == x, y => y.UsuarioLogin);
                return string.IsNullOrWhiteSpace(usuario);
            })
            .When(CamposObrigatoriosPreenchidos)
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.LoginJaExistente));
    }

    private void ValidarCamposObrigatorios()
    {
        RuleFor(x => x.UsuarioId)
            .Must(x => x != Guid.Empty)
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.DeveInformarIdentificadorAtualizar));

        RuleFor(x => x.PerfilId)
            .Must(x => x is > 0 && Enum.IsDefined(typeof(PerfilEnum), x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.DeveInformarPerfilUsuario));

        RuleFor(x => x.UsuarioNome)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.NomeObrigatorio));

        RuleFor(x => x.UsuarioLogin)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.LoginObrigatorio));

        RuleFor(x => x.UsuarioSenha)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.SenhaObrigatoria));
    }

    private bool CamposObrigatoriosPreenchidos(Models.Usuario.Usuario usuario)
    {
        return
            !string.IsNullOrWhiteSpace(usuario.UsuarioNome)
            && !string.IsNullOrWhiteSpace(usuario.UsuarioLogin)
            && !string.IsNullOrWhiteSpace(usuario.UsuarioSenha)
            && usuario.UsuarioId != Guid.Empty
            && usuario.PerfilId is > 0 
            && Enum.IsDefined(typeof(PerfilEnum), usuario.PerfilId);
    }

    private string RetornaMensagemFormatado(string mensage) => $"(Usuário): {mensage}";
}