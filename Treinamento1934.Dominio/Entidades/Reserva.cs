using Flunt.Notifications;
using System;
using Treinamento1934.Dominio.Entidades.Base;
using Treinamento1934.Dominio.Properties;

namespace Treinamento1934.Dominio.Entidades
{
    public class Reserva : EntidadeBase
    {
        public Guid IDSala { get; private set; }
        public Guid IDUsuario { get; private set; }
        public DateTime DataReserva { get; private set; }
        public DateTime InicioReserva { get; private set; }
        public DateTime FimReserva { get; private set; }
        public bool Finalizada { get; private set; }

        public Reserva(Guid idSala, Guid idUsuario, DateTime inicioReserva, DateTime fimReserva) : this(idSala, idUsuario, DateTime.Now, inicioReserva, fimReserva)
        {                
        }

        public Reserva(Guid idSala, Guid idUsuario, DateTime dataReserva, DateTime inicioReserva, DateTime fimReserva): base()
        {
            if (dataReserva < DateTime.Now.AddDays(-1))
                AddNotification(new Notification("dataReserva", Resources.DataReservaInvalida));

            if (inicioReserva > fimReserva)
                AddNotification(new Notification("InicioReserva", Resources.DataReservaInvalida));

            if (fimReserva < inicioReserva)
                AddNotification(new Notification("FimReserva", "A fim da reserva não pode ser menor que o inicio da reserva"));
                        
            IDSala = idSala;
            IDUsuario = idUsuario;
            DataReserva = dataReserva;
            InicioReserva = inicioReserva;
            FimReserva = fimReserva;
            Finalizada = false;
        }

        public void FinalizarReserva()
        {
            Finalizada = true;
        }

        
    }
}
