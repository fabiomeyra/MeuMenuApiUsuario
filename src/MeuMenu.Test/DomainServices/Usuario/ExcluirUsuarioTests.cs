using MeuMenu.Domain.Interfaces.Notificador;
using MeuMenu.Domain.Interfaces.Repositories;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Services.Base;
using MeuMenu.Domain.Validations;
using MeuMenu.Test.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeuMenu.Test.DomainServices.Usuario;

public class ExcluirUsuarioTests : IClassFixture<ExcluirUsuarioTests.Startup>, IClassFixture<UsuarioTestsFixtures>
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly IUsuarioService _usuarioService;
    private readonly INotificador _notificador;
    private readonly NegocioService _negocioService;
    private readonly UsuarioTestsFixtures _usuarioTestsFixtures;

    public ExcluirUsuarioTests(Startup startup, UsuarioTestsFixtures usuarioTestsFixtures)
    {
        _usuarioTestsFixtures = usuarioTestsFixtures;

        ServiceProvider serviceProvider = startup.ServiceProvider;
        _usuarioRepositoryMock = Mock.Get(serviceProvider.GetService<IUsuarioRepository>()!);
        _usuarioService = serviceProvider.GetService<IUsuarioService>()!;
        _notificador = serviceProvider.GetService<INotificador>()!;
        _negocioService = serviceProvider.GetService<NegocioService>()!;
    }

    [Fact(DisplayName = "Excluir Usuario com Sucesso")]
    [Trait("Categoria", "Excluir Service Test (Excluir)")]
    public async Task Usuario_Excluir_DeveExecutarComSucesso()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioAtualizarInvalido();

        // Act
        await _usuarioService.Excluir(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        _usuarioRepositoryMock.Verify(mock => mock.Excluir(usuario), Times.Once);
        Assert.True(_negocioService.EhValido());
    }

    [Fact(DisplayName = "Excluir Usuario usuarioId não informado")]
    [Trait("Categoria", "Excluir Service Test (Excluir)")]
    public async Task Usuario_Excluir_IdNaoInformado()
    {
        ReiniciarDependencias();

        // Arrange
        var usuario = _usuarioTestsFixtures.RetornaUsuarioInvalido();

        // Act
        await _usuarioService.Excluir(usuario);
        await _negocioService.ExecutarValidacao();

        // Assert
        Assert.False(_negocioService.EhValido());
        _usuarioRepositoryMock.Verify(mock => mock.Excluir(usuario), Times.Once);
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.DeveInformarIdentificadorExluir));
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