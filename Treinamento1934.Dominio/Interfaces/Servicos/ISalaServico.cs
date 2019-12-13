using System;
using System.Collections.Generic;
using Treinamento1934.Dominio.Entidades;
using static Treinamento1934.Dominio.Entidades.Sala;

namespace Treinamento1934.Dominio.Interfaces.Servicos
{
    public interface ISalaServico
    {
        Sala Busca(Guid id);
        List<Sala> SalasReservadas();
        List<Sala> SalasDisponíveis();
        void Cadastrar(Sala sala);
        void AlterarStatus(Guid id, StatusSala status);
    }
}
