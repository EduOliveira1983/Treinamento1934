using Treinamento1934.Dominio.Entidades;
using Treinamento1934.Dominio.Interfaces.Base;

namespace Treinamento1934.Dominio.Interfaces.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {

        Usuario BuscarPorEmail(string email);
    }
}
