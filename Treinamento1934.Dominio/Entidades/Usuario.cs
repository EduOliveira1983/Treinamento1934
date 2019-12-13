using Flunt.Validations;
using Treinamento1934.Dominio.Entidades.Base;
using Treinamento1934.Dominio.Properties;

namespace Treinamento1934.Dominio.Entidades
{
    public class Usuario : EntidadeBase
    {
        #region Propriedades
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public bool Inativo { get; private set; }

        #endregion

        #region Construtor
        public Usuario(string nome, string email, string senha, bool inativo) : base()
        {
            SetNome(nome);
            SetEmail(email);
            SetSenha(senha);
            SetStatus(inativo);
        }
        #endregion

        #region Métodos

        #region SettersPrivados

        private void SetNome(string nome)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, "Nome", Resources.NomeInvalido)
                .HasMinLengthIfNotNullOrEmpty(nome, 3, "Nome", Resources.NomeMenorTresCaracteres));

            Nome = nome;
        }

        private void SetEmail(string email)
        {
            AddNotifications(new Contract()
                .IsEmail(email, "Email", Resources.EmailInvalido)
                );

            Email = email;
        }

        private void SetSenha(string senha)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(senha, "Senha", Resources.SenhaInvalida)
                .HasMinLengthIfNotNullOrEmpty(senha, 10, "Senha", Resources.SenhaMenorDezCaracteres));

            Senha = senha;
        }

        private void SetStatus(bool inativo)
        {
            Inativo = inativo;
        }

        #endregion

        #region Publicos

        public void AlterarNome(string nomeAlterado)
        {
            SetNome(nomeAlterado);
        }
        public void AlterarEmail(string emailAlterado)
        {
            SetEmail(emailAlterado);
        }

        public void AlterarSenha(string senhaAlterada)
        {
            SetSenha(senhaAlterada);
        }

        public void AlterarStatus(bool inativoAlterado)
        {
            SetStatus(inativoAlterado);
        }

        #endregion

        #endregion

    }
}
