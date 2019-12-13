using Flunt.Notifications;
using System;
using System.Text;
using Treinamento1934.Dominio.Entidades;
using Treinamento1934.Dominio.Interfaces.Repositorio;
using Treinamento1934.Dominio.Interfaces.Servicos;

namespace Treinamento1934.Dominio.Servicos
{
    public class ReservaServico : Notifiable, IReservaServico
    {
        private IReservaRepositorio _repReserva;
        private ISalaRepositorio _repSala;
        private IUsuarioRepositorio _repUsuario;

        public ReservaServico(IReservaRepositorio repReserva, ISalaRepositorio repSala, IUsuarioRepositorio repUsuario)
        {
            _repReserva = repReserva;
            _repSala = repSala;
            _repUsuario = repUsuario;
        }

        public void Finalizar(Guid id)
        {
            var reserva = _repReserva.Buscar(id);

            if (reserva == null)
                AddNotification("Finalizar", "Reserva não encontrada");
            else
            {
                reserva.FinalizarReserva();
                _repReserva.Alterar(reserva);
            }
        }

        public void Reservar(Guid idSala, Guid idUsuario, DateTime dataReserva, DateTime fimReserva)
        {
            var sala = _repSala.Buscar(idSala);
            var usuario = _repUsuario.Buscar(idUsuario);

            if (sala == null)
                AddNotification("Reservar", "Sala não encontrada");
            else if (usuario == null)
                AddNotification("Reservar", "Usuário não encontrado");
            else
            {
                var reserva = new Reserva(idSala, idUsuario, DateTime.Now, dataReserva, fimReserva);
                if (reserva.Invalid)
                {
                    StringBuilder mensagens = new StringBuilder();
                    foreach (Notification item in reserva.Notifications)
                    {
                        mensagens.AppendLine($"Propriedade {item.Property} - Mensagem {item.Message}");
                    }
                    AddNotification("Reservar", $"Dados da Reserva Inválidos: {mensagens.ToString()}");
                }
                else
                    _repReserva.Inserir(reserva);
            }
        }
    }
}
