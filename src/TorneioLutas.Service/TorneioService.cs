using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneioLutas.Service.Helpers;
using TorneioLutas.Service.Models;

namespace TorneioLutas.Service
{
    public class TorneioService : ITorneioService
    {
        private readonly string apiUrl;
 
        public TorneioService(IConfiguration configuration)
        {
            apiUrl = configuration.GetSection("lutadoresAPI:url").Value;
        }


        public async Task<List<Lutador>> GetAllLutadores()
        {
            var lutadores = await HttpHelper.GetAsync<List<Lutador>>(apiUrl);
            return lutadores;
        }

        public Torneio GetTorneio(List<Lutador> lutadores)
        {
            Torneio torneio = new Torneio(lutadores);

            if (torneio.ListaLutadores.Count != 20) { 
                torneio.Validation.Sucess = false;
                torneio.Validation.ErrorMessage = $"Torneio deve iniciar com 20 lutadores, você selecionou {torneio.ListaLutadores.Count()}";
                return torneio;
            }

            torneio.ListaLutadores = torneio.ListaLutadores.OrderBy(l => l.Idade).ToList();
            
            DistribuiLutadoresNosGrupos(ref torneio);
            FaseDeGrupos(ref torneio);
            QuartasDeFinal(ref torneio);
            SemiFinais(ref torneio);
            Final(ref torneio);                                

            torneio.Campeao = torneio.Final.GetVencedor();
            torneio.ViceCampeao = torneio.Final.GetPerdedor();
            torneio.Terceiro = new Luta(torneio.Semi[0].GetPerdedor(), torneio.Semi[1].GetPerdedor()).GetVencedor();

            torneio.Validation.Sucess = true;
            return torneio;
        }


        private void DistribuiLutadoresNosGrupos(ref Torneio torneio)
        {
            int qtdLutadores = 5;

            for(int i = 0; i < torneio.Grupos.Length; i++)
            {
                torneio.Grupos[i] = new Grupo();
                for(int j = 0; j < qtdLutadores; j++)
                {
                    var lutador = torneio.ListaLutadores[j + (i * qtdLutadores)];
                    torneio.Grupos[i].ListaLutadores.Add(lutador);
                }
            }
        }

        private void FaseDeGrupos(ref Torneio torneio)
        {
            for(int i = 0; i < torneio.Grupos.Length; i++)
            {
                 torneio.Grupos[i].GetClassification();
            }
        }

        private void QuartasDeFinal(ref Torneio torneio)
        {
            //1A x 2B
            torneio.Quartas[0] = new Luta(torneio.Grupos[0].LutadoresClassificados[0], torneio.Grupos[1].LutadoresClassificados[1]);

            //2A x 1B
            torneio.Quartas[1] = new Luta(torneio.Grupos[0].LutadoresClassificados[1], torneio.Grupos[1].LutadoresClassificados[0]);

            //1C x 2D
            torneio.Quartas[2] = new Luta(torneio.Grupos[2].LutadoresClassificados[0], torneio.Grupos[3].LutadoresClassificados[1]);

            //2C x 1D
            torneio.Quartas[3] = new Luta(torneio.Grupos[2].LutadoresClassificados[1], torneio.Grupos[3].LutadoresClassificados[0]);
        }

        private void SemiFinais(ref Torneio torneio)
        {
            for (int i = 0; i < torneio.Quartas.Count(); i += 2)
            {
                torneio.Semi[(i / 2)] = new Luta(torneio.Quartas[i].GetVencedor(), torneio.Quartas[i + 1].GetVencedor());
            }
        }

        private void Final(ref Torneio torneio)
        {
            torneio.Final = new Luta(torneio.Semi[0].GetVencedor(), torneio.Semi[1].GetVencedor());
        }
    }
}
