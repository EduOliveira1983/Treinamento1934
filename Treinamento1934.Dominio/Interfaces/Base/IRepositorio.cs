using System;
using System.Collections.Generic;
using Treinamento1934.Dominio.Entidades.Base;

namespace Treinamento1934.Dominio.Interfaces.Base
{
    public interface IRepositorio<Entidade> where Entidade : EntidadeBase
    {
        void Inserir(ref Entidade entidade);
        void Inserir(Entidade entidade);
        void Alterar(Entidade entidade);
        void Excluir(Entidade entidade);
        void Excluir(Guid ID);
        Entidade Buscar(Func<Entidade, bool> where);
        Entidade Buscar(Guid ID);
        List<Entidade> Listar(Func<Entidade, bool> where);
        List<Entidade> ListarTodos();
    }
}
