using Bogus;
using ExpectedObjects;
using Force.DeepCloner;
using Treinamento1934.Dominio.Entidades;
using Treinamento1934.Testes.Builders.Dominio;
using Xunit;
using static Treinamento1934.Dominio.Entidades.Sala;

namespace Treinamento1934.Testes.Dominio
{
    public class SalaTestes
    {
        private Faker faker;
        private Sala salaPadrao;

        public SalaTestes()
        {
            faker = new Faker();
            salaPadrao = SalaBuilder.Novo().Build();
        }
        [Fact]
        public void DeveCriarUmaSala()
        {
            var salaEsperada = new
            {
                Nome = "Sala 1",
                Capacidade = 50,
                Status = StatusSala.Disponivel
            };

            var sala = new Sala(salaEsperada.Nome, salaEsperada.Capacidade, salaEsperada.Status);

            salaEsperada.ToExpectedObject().ShouldMatch(sala);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NaoDeveCriarSalaSemNome(string nomeInvalido)
        {
            Assert.True(SalaBuilder.Novo().ComNome(nomeInvalido).Build().Invalid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveCriarSalaSemCapacidade(int capacidadeInvalida)
        {
            Assert.True(SalaBuilder.Novo().ComCapacidade(capacidadeInvalida).Build().Invalid);
        }

        [Fact]
        public void DeveAlterarNomeSala()
        {
            var salaAlterada = salaPadrao.DeepClone(); ;

            salaAlterada.AlterarNome("Nome Alterado");

            Assert.True(salaAlterada.Nome != salaPadrao.Nome && !salaAlterada.Invalid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("aa")]
        public void NaoDeveAlterarSalaNomeInvalido(string nomeInvalido)
        {
            var salaAlterada = salaPadrao.DeepClone();

            salaAlterada.AlterarNome(nomeInvalido);

            Assert.True(salaAlterada.Nome != salaPadrao.Nome && salaAlterada.Invalid);
        }

        //Para Efeitos de Testes, passei a capacidade da sala como 31, uma vez que o Builder retorna
        //entre 5 e 30
        [Theory]
        [InlineData(31)]
        private void DeveAlterarCapacidadeSala(int capacidadeAlterada)
        {
            var salaAlterada = salaPadrao.DeepClone();

            salaAlterada.AlterarCapacidade(capacidadeAlterada);

            Assert.True(salaAlterada.Capacidade != salaPadrao.Capacidade && !salaAlterada.Invalid);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        private void NaoDeveAlterarSalaCapacidadeInvalida(int capacidadeInvalida)
        {
            var salaAlterada = salaPadrao.DeepClone();

            salaAlterada.AlterarCapacidade(capacidadeInvalida);

            Assert.True(salaAlterada.Capacidade != salaPadrao.Capacidade && salaAlterada.Invalid);
        }

        //Para esse teste, assumo que a sala esperada sempre vai ser criada com status disponivel 
        //devido a regra de negócios  e faço a alteração do status com  qualquer valor que seja 
        //justamente diferente de disponível
        [Fact]
        private void DeveAlterarStatusSala()
        {
            var salaAlterada = salaPadrao.DeepClone();

            salaAlterada.AlterarStatus(faker.PickRandomWithout(salaPadrao.Status));

            Assert.True(salaAlterada.Status != salaPadrao.Status && !salaAlterada.Invalid);

        }

    }
}
