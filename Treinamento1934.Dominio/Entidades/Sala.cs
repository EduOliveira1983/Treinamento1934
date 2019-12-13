using Flunt.Validations;
using System;
using Treinamento1934.Dominio.Entidades.Base;
using Treinamento1934.Dominio.Properties;


namespace Treinamento1934.Dominio.Entidades
{
    public class Sala : EntidadeBase
    {
        #region Enums

        public enum StatusSala
        {
            Disponivel,
            Reservada,
            Inativa,
            Bloqueada
        };

        #endregion

        #region Propriedades

        public string Nome { get; private set; }
        public int Capacidade { get; private set; }
        public StatusSala Status { get; private set; }

        #endregion

        #region Contrutores

        public Sala(string nome, int capacidade, StatusSala status) : base()
        {
            SetNome(nome);
            SetCapacidade(capacidade);
            SetStatus(status);
        }

        #endregion

        #region Métodos

        #region SettersPrivados

        private void SetNome(string nome)
        {

            AddNotifications(new Contract()
                .HasMinLen(nome, 3, "Nome", Resources.NomeMenorTresCaracteres)
                .IsNotNullOrEmpty(nome, "Nome", Resources.NomeInvalido));

            Nome = nome;
        }

        private void SetCapacidade(int capacidade)
        {
            AddNotifications(new Contract()
                .IsGreaterOrEqualsThan(capacidade, 1, "Capacidade", Resources.CapacidadeInvalida));

            Capacidade = capacidade;
        }

        private void SetStatus(StatusSala status)
        {
            if (!Enum.TryParse(typeof(StatusSala), status.ToString(), out var retorno))
                AddNotification("Status", Resources.StatusInvalido);

            Status = status;
        }

        #endregion

        #region Publicos

        public void AlterarNome(string nomeAlterado)
        {
            SetNome(nomeAlterado);
        }

        public void AlterarCapacidade(int capacidadeAlterada)
        {
            SetCapacidade(capacidadeAlterada);
        }

        public void AlterarStatus(StatusSala statusAlterado)
        {
            SetStatus(statusAlterado);
        }

        #endregion

        #endregion

    }
}
