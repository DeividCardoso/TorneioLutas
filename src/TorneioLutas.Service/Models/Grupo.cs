using System;
using System.Collections.Generic;
using System.Text;

namespace TorneioLutas.Service.Models
{
    public class Grupo
    {
        public List<Lutador> ListaLutadores { get; set; }
        public List<Lutador> LutadoresClassificados { get; set; }


        public Grupo()
        { 
            ListaLutadores = new List<Lutador>();
            LutadoresClassificados = new List<Lutador>();
        }

        public void GetClassification()
        {
            LutadoresClassificados.Clear();
            LutadoresClassificados.Add(ListaLutadores[0]);
            LutadoresClassificados.Add(ListaLutadores[1]);
        }

    }
}
