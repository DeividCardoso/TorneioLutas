using System.Collections.Generic;

namespace TorneioLutas.Domain.Models
{
    public class Lutador
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }


        public List<string> ArtesMarciais { get; set; }


        public int TotalLutas { get; set; }

        public int Derrotas { get; set; }


        public int Vitorias { get; set; }
    }
}
