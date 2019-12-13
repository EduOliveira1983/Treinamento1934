using Bogus;
using Moq;
using Treinamento1934.Dominio.Entidades;
using Treinamento1934.Dominio.Interfaces.Repositorio;
using Treinamento1934.Dominio.Servicos;
using Treinamento1934.Testes.Builders.Dominio;
using Xunit;

namespace Treinamento1934.Testes.Dominio.Servicos
{
    public class SalaServicoTestes
    {
        private Mock<ISalaRepositorio> _salaRepositorio;
        private SalaServico _salaServico;
        Faker faker;
        Sala salaPadrao;

        public SalaServicoTestes()
        {
            faker = new Faker();
            _salaRepositorio = new Mock<ISalaRepositorio>();
            _salaServico = new SalaServico(_salaRepositorio.Object);
            salaPadrao = SalaBuilder.Novo().Build();

        }

        [Fact]
        public void DeveCadastrar()
        {
            _salaServico.Cadastrar(salaPadrao);
            _salaRepositorio.Verify(v => v.Inserir(It.Is<Sala>(s => s.Nome == salaPadrao.Nome &&
                                                                    s.Capacidade == salaPadrao.Capacidade &&
                                                                    s.Status == salaPadrao.Status)
                                                                ));
        }

        [Fact]
        public void NaoDeveCadastrarJaCadastrado()
        {
            _salaRepositorio.Setup(x => x.Buscar(salaPadrao.ID)).Returns(salaPadrao);

            _salaServico.Cadastrar(salaPadrao);

            _salaRepositorio.Verify(x => x.Inserir(It.IsAny<Sala>()), Times.Never);
        }

        [Fact]
        public void DeveAlterarStatus()
        {
            _salaRepositorio.Setup(x => x.Buscar(salaPadrao.ID)).Returns(salaPadrao);

            _salaServico.AlterarStatus(salaPadrao.ID, faker.PickRandomWithout<Sala.StatusSala>(salaPadrao.Status));

            _salaRepositorio.Verify(v => v.Alterar(It.Is<Sala>(s => s.Nome == salaPadrao.Nome &&
                                                                    s.Capacidade == salaPadrao.Capacidade &&
                                                                    s.Status == salaPadrao.Status)));
        }
    }
}
