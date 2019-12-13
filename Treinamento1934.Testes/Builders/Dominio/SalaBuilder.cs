using Bogus;
using System;
using Treinamento1934.Dominio.Entidades;
using static Treinamento1934.Dominio.Entidades.Sala;

namespace Treinamento1934.Testes.Builders.Dominio
{
    public class SalaBuilder
    {
        Faker faker;
        private Guid Id;
        private string Nome;
        private int Capacidade;
        private StatusSala Status;

        public SalaBuilder()
        {
            faker = new Faker();

            Id = Guid.NewGuid();
            Nome = faker.Person.FullName;
            Capacidade = faker.Random.Int(5, 30);
            Status = faker.PickRandomWithout(StatusSala.Bloqueada, StatusSala.Inativa);
        }

        public static SalaBuilder Novo()
        {
            return new SalaBuilder();
        }

        public Sala Build()
        {
            return new Sala(Nome, Capacidade, Status);
        }

        public SalaBuilder ComId(Guid id)
        {
            Id = id;
            return this;
        }

        public SalaBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }


        public SalaBuilder ComCapacidade(int capacidade)
        {
            Capacidade = capacidade;
            return this;
        }

        public SalaBuilder ComStatus(StatusSala status)
        {
            Status = status;
            return this;
        }
    }
}
