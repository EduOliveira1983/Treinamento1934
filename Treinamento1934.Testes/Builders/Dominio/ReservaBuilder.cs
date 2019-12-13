using Bogus;
using System;
using Treinamento1934.Dominio.Entidades;

namespace Treinamento1934.Testes.Builders.Dominio
{
    public class ReservaBuilder
    {
        public ReservaBuilder()
        {
            Faker faker = new Faker();
            this.ID = faker.Random.Guid();
            this.IDSala = faker.Random.Guid();
            this.IDUsuario = faker.Random.Guid();
            this.DataReserva = DateTime.Now;
            this.InicioReserva = DateTime.Now.AddHours(2);
            this.FimReserva = DateTime.Now.AddHours(2);
        }

        public Guid ID { get; private set; }
        public Guid IDSala { get; private set; }
        public Guid IDUsuario { get; private set; }
        public DateTime DataReserva { get; private set; }
        public DateTime InicioReserva { get; private set; }
        public DateTime FimReserva { get; private set; }

        public static ReservaBuilder Novo()
        {
            return new ReservaBuilder();
        }

        public Reserva Build()
        {
            return new Reserva(IDSala, IDUsuario, DataReserva, InicioReserva, FimReserva);
        }

        public ReservaBuilder ComId(Guid id)
        {
            ID = id;
            return this;
        }

        public ReservaBuilder ComIdSala(Guid idSala)
        {
            IDSala = idSala;
            return this;
        }

        public ReservaBuilder ComIdUsuario(Guid idUsuario)
        {
            IDUsuario = idUsuario;
            return this;
        }

        public ReservaBuilder ComDataReserva(DateTime dataReserva)
        {
            DataReserva = dataReserva;
            return this;
        }

        public ReservaBuilder ComInicioReserva(DateTime inicioReserva)
        {
            InicioReserva = inicioReserva;
            return this;
        }

        public ReservaBuilder ComFimReserva(DateTime fimReserva)
        {
            FimReserva = fimReserva;
            return this;
        }
    }
}
