using Flunt.Notifications;
using System;
using System.Collections.Generic;
using Treinamento1934.Dominio.Entidades;
using Treinamento1934.Dominio.Interfaces.Repositorio;
using Treinamento1934.Dominio.Interfaces.Servicos;

namespace Treinamento1934.Dominio.Servicos
{
    public class SalaServico : Notifiable, ISalaServico
    {
        private ISalaRepositorio _salaRepositorio;
        public SalaServico(ISalaRepositorio salaRepositorio)
        {
            _salaRepositorio = salaRepositorio;
        }

        public void AlterarStatus(Guid id, Sala.StatusSala status)
        {
            var sala = _salaRepositorio.Buscar(id);

            if (sala == null)
                AddNotification("AlterarStatus", "Sala não Encontrada");
            else
            {
                sala.AlterarStatus(status);

                if (sala.Invalid)
                {
                    foreach (var notification in sala.Notifications)
                    {
                        AddNotification("AlterarStatus", $"{notification.Property} - {notification.Message}");
                    }
                }
                else
                {
                    _salaRepositorio.Alterar(sala);
                }
            }
        }

        public Sala Busca(Guid id)
        {
            return _salaRepositorio.Buscar(id);
        }

        public void Cadastrar(Sala sala)
        {
            var salapesquisada = _salaRepositorio.Buscar(sala.ID);

            if (salapesquisada != null)
                AddNotification("Cadastrar", "Sala já cadastrada");
            else
                _salaRepositorio.Inserir(sala);
        }

        public List<Sala> SalasDisponíveis()
        {
            return _salaRepositorio.Listar(x => x.Status == Sala.StatusSala.Disponivel);
        }

        public List<Sala> SalasReservadas()
        {
            return _salaRepositorio.Listar(x => x.Status == Sala.StatusSala.Reservada);
        }
    }
}
