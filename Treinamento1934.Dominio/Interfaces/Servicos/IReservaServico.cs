using System;

namespace Treinamento1934.Dominio.Interfaces.Servicos
{
    public interface IReservaServico
    {
        void Reservar(Guid idSala, Guid idUsuario, DateTime dataReserva, DateTime fimReserva);
        void Finalizar(Guid Id);
    }
}
