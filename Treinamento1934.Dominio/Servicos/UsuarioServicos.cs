using Flunt.Notifications;
using System;
using System.Collections.Generic;
using Treinamento1934.Dominio.Entidades;
using Treinamento1934.Dominio.Interfaces.Repositorio;
using Treinamento1934.Dominio.Interfaces.Servicos;

namespace Treinamento1934.Dominio.Servicos
{
    public class UsuarioServico : Notifiable, IUsuarioServico
    {
        private IUsuarioRepositorio _repositorio;

        public UsuarioServico(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void AlterarSenha(string email, string senhaAntiga, string novaSenha, string confirmacaoNovaSenha)
        {
            var usuarioPesquisado = _repositorio.BuscarPorEmail(email);

            if (usuarioPesquisado == null)
            {
                AddNotification("AlterarSenha", "Usuario Não Encontrado!");
            }
            else if (senhaAntiga == novaSenha)
            {
                AddNotification("AlterarSenha", "A nova senha deve ser diferente da antiga");
            }
            else if (novaSenha != confirmacaoNovaSenha)
            {
                AddNotification("AlterarSenha", "A confirmaão da nova senha não deve ser diferente da nova senha");
            }
            else
            {
                usuarioPesquisado.AlterarSenha(senhaAntiga);
                _repositorio.Alterar(usuarioPesquisado);
            }
        }


        public void AlterarStatus(Guid id, bool status)
        {
            var usuarioAlterado = _repositorio.Buscar(id);

            if (usuarioAlterado == null)
            {
                AddNotification("AlterarStatus", "Usuário não encontrado");
            }
            else
            {
                usuarioAlterado.AlterarStatus(status);

                _repositorio.Alterar(usuarioAlterado);
            }
        }

        public void Cadastrar(string nome, string email, string senha, string confirmacaoSenha)
        {

            if (senha != confirmacaoSenha)
            {
                AddNotification("Cadastrar", "A senha não é igual à confirmação!");
                return;
            }

            var usuarioPesquisado = _repositorio.BuscarPorEmail(email);

            if (usuarioPesquisado != null)
            {
                AddNotification("Cadastrar", "Email já cadastrado");
                return;
            }

            _repositorio.Inserir(new Usuario(nome, email, senha, false));
        }

        public List<Usuario> ListarUsuariosAtivos()
        {
            return _repositorio.Listar(x => x.Inativo == false);
        }
    }
}
