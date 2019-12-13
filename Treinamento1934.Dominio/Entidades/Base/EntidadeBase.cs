using Flunt.Notifications;
using System;

namespace Treinamento1934.Dominio.Entidades.Base
{
    public abstract class EntidadeBase : Notifiable
    {
        public Guid ID { get; protected set; }

        public EntidadeBase()
        {
            ID = Guid.NewGuid();
        }
    }
}
