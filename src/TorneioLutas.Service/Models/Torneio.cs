using System.Collections.Generic;
using System.Linq;

namespace TorneioLutas.Service.Models
{
    public class Torneio
    {
        public List<Lutador> ListaLutadores { get; set; }

        public Grupo[] Grupos { get; set; }
        public Luta[] Quartas { get; }
        public Luta[] Semi { get; }
        public Luta Final { get; set; }
        public Lutador Campeao { get; set; }
        public Lutador ViceCampeao { get; set; }
        public Lutador Terceiro { get; set; }
        public Validation Validation { get; set; }


        public Torneio(List<Lutador> lutadores)
        {
            ListaLutadores = lutadores;
            Grupos = new Grupo[4];
            Quartas = new Luta[4];
            Semi = new Luta[2];
            Validation = new Validation();
        }




    }
}
