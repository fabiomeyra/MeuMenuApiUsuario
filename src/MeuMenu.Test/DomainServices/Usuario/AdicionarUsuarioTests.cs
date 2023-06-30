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

public class AdicionarUsuarioTests : IClassFixture<AdicionarUsuarioTests.Startup>, IClassFixture<UsuarioTestsFixtures>
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly IUsuarioService _usuarioService;
    private readonly INotificador _notificador;
    private readonly NegocioService _negocioService;
    private readonly UsuarioTestsFixtures _usuarioTestsFixtures;

    public AdicionarUsuarioTests(Startup startup, UsuarioTestsFixtures usuarioTestsFixtures)
    {
        _usuarioTestsFixtures = usuarioTestsFixtures;

        ServiceProvider serviceProvider = startup.ServiceProvider;
        _usuarioRepositoryMock = Mock.Get(serviceProvider.GetService<IUsuarioRepository>()!);
        _usuarioService = serviceProvider.GetService<IUsuarioService>()!;
        _notificador = serviceProvider.GetService<INotificador>()!;
        _negocioService = serviceProvider.GetService<NegocioService>()!;
    }

    [Fact(DisplayName = "Adicionar Usuario com Sucesso")]
    [Trait("Categoria", "Adicionar Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_DeveExecutarComSucesso()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioValido();
        _usuarioRepositoryMock.Setup(x => x.Adicionar(usuario)).ReturnsAsync(() => usuario);

        // Act
        await _usuarioService.Adicionar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(usuario), Times.Once);
        Assert.True(_negocioService.EhValido());
    }

    [Fact(DisplayName = "Adicionar Usuario login não preenchido")]
    [Trait("Categoria", "Adicionar Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_LoginNaoPreenchido()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioInvalido();
        _usuarioRepositoryMock.Setup(x => x.Adicionar(usuario)).ReturnsAsync(() => usuario);

        // Act
        await _usuarioService.Adicionar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        Assert.False(_negocioService.EhValido());
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(usuario), Times.Once);
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.LoginObrigatorio));
    }

    [Fact(DisplayName = "Adicionar Usuario nome não preenchido")]
    [Trait("Categoria", "Adicionar Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_NomeNaoPreenchido()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioInvalido();
        _usuarioRepositoryMock.Setup(x => x.Adicionar(usuario)).ReturnsAsync(() => usuario);

        // Act
        await _usuarioService.Adicionar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        Assert.False(_negocioService.EhValido());
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(usuario), Times.Once);
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.NomeObrigatorio));
    }

    [Fact(DisplayName = "Adicionar Usuario perfil não preenchido")]
    [Trait("Categoria", "Adicionar Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_PerfilNaoPreenchido()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioInvalido();
        _usuarioRepositoryMock.Setup(x => x.Adicionar(usuario)).ReturnsAsync(() => usuario);

        // Act
        await _usuarioService.Adicionar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        Assert.False(_negocioService.EhValido());
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(usuario), Times.Once);
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.DeveInformarPerfilUsuario));
    }

    [Fact(DisplayName = "Adicionar Usuario senha não preenchida")]
    [Trait("Categoria", "Adicionar Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_SenhaNaoPreenchida()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioInvalido();
        _usuarioRepositoryMock.Setup(x => x.Adicionar(usuario)).ReturnsAsync(() => usuario);

        // Act
        await _usuarioService.Adicionar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        Assert.False(_negocioService.EhValido());
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(usuario), Times.Once);
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.SenhaObrigatoria));
    }

    [Fact(DisplayName = "Adicionar Usuario login já cadastrado")]
    [Trait("Categoria", "Adicionar Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_LoginJaCadastrado()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioValido();
        _usuarioRepositoryMock.Setup(x => x.Adicionar(usuario)).ReturnsAsync(() => usuario);
        _usuarioRepositoryMock.Setup(x => x.Obter(y => y.UsuarioLogin == "teste",y => y.UsuarioLogin, true)).ReturnsAsync(() => "teste");

        // Act
        await _usuarioService.Adicionar(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        Assert.False(_negocioService.EhValido());
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(usuario), Times.Once);
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