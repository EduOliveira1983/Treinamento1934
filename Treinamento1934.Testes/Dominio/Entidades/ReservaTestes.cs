using ExpectedObjects;
using System;
using Treinamento1934.Dominio.Entidades;
using Treinamento1934.Testes.Builders.Dominio;
using Xunit;

namespace Treinamento1934.Testes.Dominio
{
    public class ReservaTestes
    {

        [Fact]
        public void DeveCriarReserva()
        {
            var reservaEsperada = new
            {
                IDSala = Guid.NewGuid(),
                IDUsuario = Guid.NewGuid(),
                DataReserva = DateTime.Now,
                InicioReserva = DateTime.Now.AddHours(2),
                FimReserva = DateTime.Now.AddHours(4)
            };

            var reserva = new Reserva(reservaEsperada.IDSala,
                                      reservaEsperada.IDUsuario,
                                      reservaEsperada.DataReserva,
                                      reservaEsperada.InicioReserva,
                                      reservaEsperada.FimReserva);
            reservaEsperada.ToExpectedObject().ShouldMatch(reserva);
        }

        [Fact]
        public void NaoDeveCriarSalaDataInvalida()
        {
            var dataInvalida = DateTime.Now.AddDays(-1);

            Assert.True(ReservaBuilder.Novo().ComDataReserva(dataInvalida).Build().Invalid);
        }

        [Fact]
        public void NaoDeveCriarSalaInicioReservaMaiorFimReserva()
        {
            var InicioInvalido = DateTime.Now.AddDays(1);

            Assert.True(ReservaBuilder.Novo()
                .ComInicioReserva(InicioInvalido)
                .ComFimReserva(DateTime.Now).Build().Invalid);
        }


    }
}
