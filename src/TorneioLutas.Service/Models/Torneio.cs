namespace TorneioLutas.Service.Models
{
    public class Torneio
    {
        public Luta[] Oitavas { get; }
        public Luta[] Quartas { get; }
        public Luta[] Semi { get; }
        public Luta Final { get; set; }
        public Lutador Vencedor { get; set; }

        public Torneio()
        {
            Oitavas = new Luta[8];
            Quartas = new Luta[4];
            Semi = new Luta[2];
        }


    }
}
