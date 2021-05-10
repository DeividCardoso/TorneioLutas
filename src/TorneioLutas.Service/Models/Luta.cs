using System;
using System.Collections.Generic;
using System.Text;

namespace TorneioLutas.Service.Models
{
    public class Luta
    {

        public Luta()
        {

        }

        public Luta(Lutador lutador1, Lutador lutador2)
        {
            Lutador1 = lutador1;
            Lutador2 = lutador2;
        }

        public Lutador Lutador1 { get; }
        public Lutador Lutador2 { get; }

        public Lutador GetVencedor()
        {
            if (PercentualVitoria(Lutador1) > PercentualVitoria(Lutador2)) return Lutador1;
            else if (PercentualVitoria(Lutador2) > PercentualVitoria(Lutador1)) return Lutador2;
            else return DesempatarNumeroArtesMarciais(Lutador1, Lutador2);
        }               

        public Lutador GetPerdedor()
        {
            if (GetVencedor() == Lutador1) return Lutador2;
            else return Lutador1;
        }

        private Lutador DesempatarNumeroArtesMarciais(Lutador lutador1, Lutador lutador2)
        {
            if (lutador1.ArtesMarciais.Count > lutador2.ArtesMarciais.Count) 
                return Lutador1;
            else if (lutador2.ArtesMarciais.Count > lutador1.ArtesMarciais.Count) 
                return Lutador2;
            return DesempatarNumeroLutas(Lutador1, Lutador2);
        }

        private Lutador DesempatarNumeroLutas(Lutador lutador1, Lutador lutador2)
        {
            if (lutador2.Lutas > lutador1.Lutas) 
                return Lutador2;
            return Lutador1;
        }

        private int PercentualVitoria(Lutador lutador)
        {
            return (lutador.Vitorias * 100) / lutador.Lutas;
        }

    }
}
