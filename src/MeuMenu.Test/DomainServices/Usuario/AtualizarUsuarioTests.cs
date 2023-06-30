using System;
using System.Linq.Expressions;
using MeuMenu.Domain.Interfaces.Notificador;
using MeuMenu.Domain.Interfaces.Repositories;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Services.Base;
using MeuMenu.Test.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using MeuMenu.Domain.Validations;
using Xunit;

namespace MeuMenu.Test.DomainServices.Usuario;

public class AtualizarUsuarioTests : IClassFixture<AtualizarUsuarioTests.Startup>, IClassFixture<UsuarioTestsFixtures>
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly IUsuarioService _usuarioService;
    private readonly INotificador _notificador;
    private readonly NegocioService _negocioService;
    private readonly UsuarioTestsFixtures _usuarioTestsFixtures;

    public AtualizarUsuarioTests(Startup startup, UsuarioTestsFixtures usuarioTestsFixtures)
    {
        _usuarioTestsFixtures = usuarioTestsFixtures;

        ServiceProvider serviceProvider = startup.ServiceProvider;
        _usuarioRepositoryMock = Mock.Get(serviceProvider.GetService<IUsuarioRepository>()!);
        _usuarioService = serviceProvider.GetService<IUsuarioService>()!;
        _notificador = serviceProvider.GetService<INotificador>()!;
        _negocioService = serviceProvider.GetService<NegocioService>()!;
    }

    [Fact(DisplayName = "Atualizar Usuario com Sucesso")]
    [Trait("Categoria", "Atualizar Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_DeveExecutarComSucesso()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioAtualizarValido();
        _usuarioRepositoryMock.Setup(x => x.Atualizar(usuario)).ReturnsAsync(() => usuario);

        // Act
        await _usuarioService.Atualizar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(usuario), Times.Once);
        Assert.True(_negocioService.EhValido());
    }

    [Fact(DisplayName = "Atualizar Usuario login não preenchido")]
    [Trait("Categoria", "Atualizar Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_LoginNaoPreenchido()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioAtualizarInvalido();
        _usuarioRepositoryMock.Setup(x => x.Atualizar(usuario)).ReturnsAsync(() => usuario);

        // Act
        await _usuarioService.Atualizar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        Assert.False(_negocioService.EhValido());
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(usuario), Times.Once);
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.LoginObrigatorio));
    }

    [Fact(DisplayName = "Atualizar Usuario nome não preenchido")]
    [Trait("Categoria", "Atualizar Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_NomeNaoPreenchido()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioAtualizarInvalido();
        _usuarioRepositoryMock.Setup(x => x.Atualizar(usuario)).ReturnsAsync(() => usuario);

        // Act
        await _usuarioService.Atualizar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        Assert.False(_negocioService.EhValido());
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(usuario), Times.Once);
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.NomeObrigatorio));
    }

    [Fact(DisplayName = "Atualizar Usuario perfil não preenchido")]
    [Trait("Categoria", "Atualizar Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_PerfilNaoPreenchido()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioAtualizarInvalido();
        _usuarioRepositoryMock.Setup(x => x.Atualizar(usuario)).ReturnsAsync(() => usuario);

        // Act
        await _usuarioService.Atualizar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        Assert.False(_negocioService.EhValido());
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(usuario), Times.Once);
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.DeveInformarPerfilUsuario));
    }

    [Fact(DisplayName = "Atualizar Usuario senha não preenchida")]
    [Trait("Categoria", "Atualizar Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_SenhaNaoPreenchida()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioAtualizarInvalido();
        _usuarioRepositoryMock.Setup(x => x.Atualizar(usuario)).ReturnsAsync(() => usuario);

        // Act
        await _usuarioService.Atualizar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        Assert.False(_negocioService.EhValido());
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(usuario), Times.Once);
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.SenhaObrigatoria));
    }

    [Fact(DisplayName = "Atualizar Usuario login já cadastrado")]
    [Trait("Categoria", "Atualizar Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_LoginJaCadastrado()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioAtualizarValido();
        _usuarioRepositoryMock.Setup(x => x.Atualizar(usuario)).ReturnsAsync(() => usuario);
        _usuarioRepositoryMock.Setup(x => x.Obter(It.IsAny<Expression<Func<Domain.Models.Usuario, bool>>>(), y => y.UsuarioLogin, true)).ReturnsAsync(() => "teste");

        // Act
        await _usuarioService.Atualizar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        Assert.False(_negocioService.EhValido());
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(usuario), Times.Once);
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.LoginJaExistente));
    }

    private void ReiniciarDependencias()
    {
        _usuarioRepositoryMock.Reset();
        _negocioService.LimparNotificacoes();
        _negocioService.LimparValidacoes();
    }

    public class Startup : BaseStartup
    {
        public override IServiceCollection OnConfigureServices(IServiceCollection services)
        {
            return services
                .AddScoped(_ => Mock.Of<IUsuarioRepository>());
        }
    }
}