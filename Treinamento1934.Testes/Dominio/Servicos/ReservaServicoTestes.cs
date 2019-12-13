using Bogus;
using Moq;
using Treinamento1934.Dominio.Entidades;
using Treinamento1934.Dominio.Interfaces.Repositorio;
using Treinamento1934.Dominio.Servicos;
using Treinamento1934.Testes.Builders.Dominio;
using Xunit;

namespace Treinamento1934.Testes.Dominio.Servicos
{
    public class ReservaServicoTestes
    {
        private Faker _faker;
        private Reserva _reservaPadrao;

        private Mock<IReservaRepositorio> _repReserva;
        private ReservaServico _servReserva;

        private Mock<ISalaRepositorio> _repSala;
        private SalaServico _servSala;

        private Mock<IUsuarioRepositorio> _repUsuario;
        private UsuarioServico _servUsuario;
        
        
        public ReservaServicoTestes()
        {
            _faker = new Faker();
            _reservaPadrao = ReservaBuilder.Novo().Build();

            _repSala = new Mock<ISalaRepositorio>();
            _servSala = new SalaServico(_repSala.Object);

            _repUsuario = new Mock<IUsuarioRepositorio>();
            _servUsuario = new UsuarioServico(_repUsuario.Object);

            _repReserva = new Mock<IReservaRepositorio>();
            _servReserva = new ReservaServico(_repReserva.Object, _repSala.Object, _repUsuario.Object);            
        }

        [Fact]
        public void DeveReservar()
        {
            Usuario usuarioValido = UsuarioBuilder.Novo().Build();
            Sala salaValida = SalaBuilder.Novo().Build();

            _repUsuario.Setup(x => x.Buscar(_reservaPadrao.IDUsuario)).Returns(usuarioValido);
            _repSala.Setup(x => x.Buscar(_reservaPadrao.IDSala)).Returns(salaValida);

            _servReserva.Reservar(_reservaPadrao.IDSala, _reservaPadrao.IDUsuario, _reservaPadrao.DataReserva, _reservaPadrao.FimReserva);
            
            _repReserva.Verify(x => x.Inserir(It.IsAny<Reserva>()), Times.AtLeastOnce);
        }

        [Fact]
        public void NaoDeveReservarSalaInvalida()
        {
            Sala salaInvalida = null;
            _repSala.Setup(x => x.Buscar(_reservaPadrao.IDSala)).Returns(salaInvalida);

            _servReserva.Reservar(_reservaPadrao.IDSala, _reservaPadrao.IDUsuario, _reservaPadrao.DataReserva, _reservaPadrao.FimReserva);

            _repReserva.Verify(x => x.Inserir(It.IsAny<Reserva>()), Times.Never);
        }

        [Fact]
        public void NaoDeveReservarUsuarioInvalido()
        {
            Usuario usuarioInvalido = null;
            Sala salaValida = SalaBuilder.Novo().Build();

            _repUsuario.Setup(x => x.Buscar(_reservaPadrao.IDUsuario)).Returns(usuarioInvalido);
            _repSala.Setup(x => x.Buscar(_reservaPadrao.IDSala)).Returns(salaValida);

            _servReserva.Reservar(_reservaPadrao.IDSala, _reservaPadrao.IDUsuario, _reservaPadrao.DataReserva, _reservaPadrao.FimReserva);

            _repReserva.Verify(x => x.Inserir(It.IsAny<Reserva>()), Times.Never);
        }

        [Fact]
        public void NaoDeveReservarDadosReservaInvalido()
        {
            Usuario usuarioValido = UsuarioBuilder.Novo().Build();
            Sala salaValida = SalaBuilder.Novo().Build();

            _repUsuario.Setup(x => x.Buscar(_reservaPadrao.IDUsuario)).Returns(usuarioValido);
            _repSala.Setup(x => x.Buscar(_reservaPadrao.IDSala)).Returns(salaValida);

            _servReserva.Reservar(_reservaPadrao.IDSala, _reservaPadrao.IDUsuario, _reservaPadrao.DataReserva, _reservaPadrao.DataReserva.AddDays(-1));

            _repReserva.Verify(x => x.Inserir(It.IsAny<Reserva>()), Times.Never);
        }

        [Fact]
        public void DeveFinalizarReserva()
        {
            _repReserva.Setup(x => x.Buscar(_reservaPadrao.ID)).Returns(_reservaPadrao);
            _servReserva.Finalizar(_reservaPadrao.ID);

            _repReserva.Verify(x => x.Alterar(It.IsAny<Reserva>()), Times.AtLeastOnce);
        }

        [Fact]
        public void NaoDeveFinalizarReservaInvalida()
        {
            Reserva reservaInvalida = null;
            _repReserva.Setup(x => x.Buscar(_reservaPadrao.ID)).Returns(reservaInvalida);

            _servReserva.Finalizar(_reservaPadrao.ID);

            _repReserva.Verify(x => x.Alterar(It.IsAny<Reserva>()), Times.Never);
        }
        
    }
}
