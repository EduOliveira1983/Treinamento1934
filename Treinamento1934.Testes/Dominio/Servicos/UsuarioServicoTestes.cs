using Bogus;
using Moq;
using Treinamento1934.Dominio.Entidades;
using Treinamento1934.Dominio.Interfaces.Repositorio;
using Treinamento1934.Dominio.Servicos;
using Treinamento1934.Testes.Builders.Dominio;
using Xunit;

namespace Treinamento1934.Testes.Dominio.Servicos
{
    public class UsuarioServicoTestes
    {
        private Faker faker;
        private Mock<IUsuarioRepositorio> _repositorio;
        private UsuarioServico _usuarioServico;
        private Usuario usuarioPadrao;

        public UsuarioServicoTestes()
        {
            faker = new Faker();
            _repositorio = new Mock<IUsuarioRepositorio>();
            _usuarioServico = new UsuarioServico(_repositorio.Object);

            usuarioPadrao = UsuarioBuilder.Novo().Build();
        }

        [Fact]
        public void DeveCadastrar()
        {
            _usuarioServico.Cadastrar(usuarioPadrao.Nome, usuarioPadrao.Email, usuarioPadrao.Senha, usuarioPadrao.Senha);
            _repositorio.Verify(x => x.Inserir(It.IsAny<Usuario>()),
                                Times.AtLeastOnce);
        }

        [Fact]
        public void NaoDeveCadastrarSenhaConfirmacaoDiferente()
        {
            _usuarioServico.Cadastrar(usuarioPadrao.Nome, usuarioPadrao.Email, usuarioPadrao.Senha, faker.Internet.Password());
            Assert.True(_usuarioServico.Invalid);
        }

        [Fact]
        public void NaoDeveCadastrarEmailExistente()
        {
            var usuarioExistente = UsuarioBuilder.Novo().Build();

            _repositorio.Setup(x => x.BuscarPorEmail(usuarioPadrao.Email)).Returns(usuarioPadrao);

            _usuarioServico.Cadastrar(usuarioPadrao.Nome, usuarioPadrao.Email, usuarioPadrao.Senha, usuarioPadrao.Senha);

            _repositorio.Verify(x => x.Inserir(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public void DeveAlterarSenha()
        {

            string novaSenha = faker.Internet.Password();
            _repositorio.Setup(x => x.BuscarPorEmail(usuarioPadrao.Email)).Returns(usuarioPadrao);

            _usuarioServico.AlterarSenha(usuarioPadrao.Email, usuarioPadrao.Senha, novaSenha, novaSenha);
            _repositorio.Verify(x => x.Alterar(It.IsAny<Usuario>()), Times.AtLeastOnce);
        }

        [Fact]
        public void NaoDeveAlterarSenhaEmailInexistente()
        {
            Usuario usuarioInexistente = null;
            _repositorio.Setup(x => x.BuscarPorEmail(usuarioPadrao.Email)).Returns(usuarioInexistente);

            _usuarioServico.AlterarSenha(usuarioPadrao.Email, usuarioPadrao.Senha, usuarioPadrao.Senha, usuarioPadrao.Senha);
            _repositorio.Verify(x => x.Alterar(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public void NaoDeveAlterarSenhasIguais()
        {
            _repositorio.Setup(x => x.BuscarPorEmail(usuarioPadrao.Email)).Returns(usuarioPadrao);

            _usuarioServico.AlterarSenha(usuarioPadrao.Email, usuarioPadrao.Senha, usuarioPadrao.Senha, usuarioPadrao.Senha);
            _repositorio.Verify(x => x.Alterar(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public void NaoDeveAlterarSenhaNovaConfirmacaoDiferentes()
        {
            string novaSenha = faker.Internet.Password();
            _repositorio.Setup(x => x.BuscarPorEmail(usuarioPadrao.Email)).Returns(usuarioPadrao);

            _usuarioServico.AlterarSenha(usuarioPadrao.Email, usuarioPadrao.Senha, novaSenha, usuarioPadrao.Senha);

            _repositorio.Verify(x => x.Alterar(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public void DeveAlterarStatus()
        {
            _repositorio.Setup(x => x.Buscar(usuarioPadrao.ID)).Returns(usuarioPadrao);
            _usuarioServico.AlterarStatus(usuarioPadrao.ID, !usuarioPadrao.Inativo);
        }

        [Fact]
        public void NaoDeveAlterarStatusUsuarioInvalido()
        {
            Usuario usuarioInvalido = null;
            _repositorio.Setup(x => x.Buscar(usuarioPadrao.ID)).Returns(usuarioInvalido);
            _usuarioServico.AlterarStatus(usuarioPadrao.ID, !usuarioPadrao.Inativo);

            _repositorio.Verify(x => x.Inserir(It.IsAny<Usuario>()), Times.Never);
        }

    }


}
