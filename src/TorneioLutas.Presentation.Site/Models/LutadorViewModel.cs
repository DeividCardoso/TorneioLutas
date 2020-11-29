using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TorneioLutas.Presentation.Site.Models
{
    public class LutadorViewModel
    {
            public int Id { get; set; }

            public string Nome { get; set; }

            public int Idade { get; set; }

            [Display(Name ="Artes Marciais")]
            public List<string> ArtesMarciais { get; set; }

            [Display(Name ="Total de Lutas")]
            public int Lutas { get; set; }

            public int Derrotas { get; set; }

            [Display(Name ="Vitórias")]
            public int Vitorias { get; set; }

            public bool IsSelected { get; set; }
        
    }
}
