using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS2223_4G_INF_Bunardziu_RubricaCSVMAUI
{
    internal class Contatti
    {
        public string Cognome { get; }
        public string Nome { get; }
        public string Citta { get; }

        public Contatti (string cognome, string nome, string citta)
        {
            Cognome = cognome;
            Nome = nome;
            Citta = citta;
        }

        public Contatti() { }

        public Contatti (string temp)
        {
                string[] temparr = temp.Split(',');//creo un array temporaneo in cui mi salvo il valore restituito da split restituito però è un array quindi lo salvo dentro al mio array di stringhe temporaneo
                Nome = temparr[0];
                Cognome = temparr[1];
                Citta= temparr[2];
        }
    }
}
