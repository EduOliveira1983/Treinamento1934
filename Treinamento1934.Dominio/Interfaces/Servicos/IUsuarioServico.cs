using System;
using System.Collections.Generic;
using Treinamento1934.Dominio.Entidades;

namespace Treinamento1934.Dominio.Interfaces.Servicos
{
    public interface IUsuarioServico
    {
        void Cadastrar(string nome, string email, string senha, string confirmacaoSenha);
        void AlterarSenha(string email, string senhaAntiga, string novaSenha, string confirmacaoNovaSenha);
        void AlterarStatus(Guid id, bool status);
        List<Usuario> ListarUsuariosAtivos();
    }
}
