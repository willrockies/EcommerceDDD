using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Notifications
{
    public class Notifies
    {
        [NotMapped]
        public string NomePropriedade { get; set; }
        [NotMapped]
        public string mensagem { get; set; }

        [NotMapped]
        public List<Notifies> notificacoes { get; set; }
        public Notifies()
        {
            notificacoes = new List<Notifies>();
        }

        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {

                notificacoes.Add(new Notifies
                {
                    mensagem = "Campo Obrigatorio",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }

        public bool ValidarPropriedadeInt(int valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                notificacoes.Add(new Notifies
                {
                    mensagem = "Campo Obrigatorio",
                    NomePropriedade = NomePropriedade

                });

                return false;
            }
            return true;
        }

        public bool ValidarPropriedadeDecimal(decimal valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                notificacoes.Add(new Notifies
                {
                    mensagem = "Campo Obrigatorio",
                    NomePropriedade = NomePropriedade

                });

                return false;
            }
            return true;
        }
    }
}
