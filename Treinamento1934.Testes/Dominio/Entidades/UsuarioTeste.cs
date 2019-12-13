using Bogus;
using ExpectedObjects;
using Force.DeepCloner;
using Treinamento1934.Dominio.Entidades;
using Treinamento1934.Testes.Builders.Dominio;
using Xunit;

namespace Treinamento1934.Testes.Dominio
{
    public class UsuarioTeste
    {
        private Faker faker;
        private Usuario usuarioPadrao;

        public UsuarioTeste()
        {
            faker = new Faker();
            usuarioPadrao = UsuarioBuilder.Novo().Build();
        }

        [Fact]
        public void DeveCriarUsuario()
        {
            var usuarioEsperado = new
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Senha = faker.Internet.Password(),
                Inativo = false
            };

            var usuarioNovo = new Usuario(usuarioEsperado.Nome, usuarioEsperado.Email, usuarioEsperado.Senha, usuarioEsperado.Inativo);
            usuarioEsperado.ToExpectedObject().ShouldMatch(usuarioNovo);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("aa")]
        public void NaoDeveCriarUsuarioNomeInvalido(string nomeInvalido)
        {
            Assert.True(UsuarioBuilder.Novo().ComNome(nomeInvalido).Build().Invalid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("teste")]
        [InlineData("@teste.com")]
        [InlineData("teste@teste")]
        [InlineData("teste.com")]
        public void NaoDeveCriarUsuarioEmailInvalido(string emailInvalido)
        {
            Assert.True(UsuarioBuilder.Novo().ComEmail(emailInvalido).Build().Invalid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("aaaaaaaaa")]
        public void NaoDeveCriarUsuarioSenhaInvalida(string senhaInvalida)
        {
            Assert.True(UsuarioBuilder.Novo().ComSenha(senhaInvalida).Build().Invalid);
        }

        [Fact]
        public void DeveAlterarUmNome()
        {
            var usuarioAlterado = usuarioPadrao.DeepClone();

            usuarioAlterado.AlterarNome("Nome Alterado");

            Assert.True(usuarioAlterado.Nome != usuarioPadrao.Nome && !usuarioAlterado.Invalid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("aa")]
        public void NaoDeveAlterarUsuarioNomeInvalido(string nomeInvalido)
        {
            var usuarioAlterado = usuarioPadrao.DeepClone();

            usuarioAlterado.AlterarNome(nomeInvalido);

            Assert.True(usuarioAlterado.Nome != usuarioPadrao.Nome && usuarioAlterado.Invalid);
        }

        [Fact]
        public void DeveAlterarEmailUsuario()
        {
            var usuarioAlterado = usuarioPadrao.DeepClone();

            usuarioAlterado.AlterarEmail(faker.Internet.Email());

            Assert.True(usuarioAlterado.Email != usuarioPadrao.Email && !usuarioAlterado.Invalid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("usuario")]
        [InlineData("@email")]
        [InlineData("usuario@email")]
        [InlineData("usuario.com")]
        [InlineData("usuario@")]
        public void NaoDeveAlterarUsuarioEmailInvalido(string emailInvalido)
        {
            var usuarioAlterado = usuarioPadrao.DeepClone();

            usuarioAlterado.AlterarEmail(emailInvalido);

            Assert.True(usuarioAlterado.Email != usuarioPadrao.Email && usuarioAlterado.Invalid);
        }

        [Fact]
        public void DeveAlterarSenhaUsuario()
        {
            var usuario = usuarioPadrao.DeepClone();

            usuarioPadrao.AlterarSenha(faker.Internet.Password());

            Assert.True(usuario.Senha != usuarioPadrao.Senha && !usuario.Invalid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("aaaaaaaaa")]
        public void NaoDeveAlterarUsuarioSenhaInvalida(string senhaInvalida)
        {
            var usuario = usuarioPadrao.DeepClone();

            usuarioPadrao.AlterarSenha(senhaInvalida);

            Assert.True(usuario.Senha != usuarioPadrao.Senha && usuarioPadrao.Invalid);
        }

        [Fact]
        public void DeveAlterarStatusUsuario()
        {
            var usuarioAlterado = usuarioPadrao.DeepClone();

            usuarioAlterado.AlterarStatus(!usuarioPadrao.Inativo);

            Assert.True(usuarioAlterado.Inativo != usuarioPadrao.Inativo);
        }

    }
}
