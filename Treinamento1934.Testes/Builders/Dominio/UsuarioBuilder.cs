using Bogus;
using System;
using Treinamento1934.Dominio.Entidades;

namespace Treinamento1934.Testes.Builders.Dominio
{
    public class UsuarioBuilder
    {
        private Guid ID;
        private string Nome;
        private string Email;
        private bool Inativo;
        private string Senha;
        Faker faker;

        public UsuarioBuilder()
        {
            faker = new Faker();
            ID = Guid.NewGuid();
            Nome = faker.Person.FullName;
            Email = faker.Person.Email;
            Inativo = false;
            Senha = faker.Internet.Password();
        }

        public static UsuarioBuilder Novo()
        {
            return new UsuarioBuilder();
        }

        public Usuario Build()
        {
            return new Usuario(Nome, Email, Senha, Inativo);
        }

        public UsuarioBuilder ComId(Guid id)
        {
            ID = id;
            return this;
        }

        public UsuarioBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public UsuarioBuilder ComEmail(string email)
        {
            Email = email;
            return this;
        }

        public UsuarioBuilder ComSenha(string senha)
        {
            Senha = senha;
            return this;
        }

        public UsuarioBuilder ComInativo(bool inativo)
        {
            Inativo = inativo;
            return this;
        }
    }
}
