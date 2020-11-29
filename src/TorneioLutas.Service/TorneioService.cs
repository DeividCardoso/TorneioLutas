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
            return await HttpHelper.GetAsync<List<Lutador>>(apiUrl);
        }

        public Torneio GetTorneio(List<Lutador> lutadores)
        {
            Torneio torneio = new Torneio();
            torneio.ListaLutadores = lutadores;

            if (torneio.ListaLutadores.Count != 16) { 
                torneio.Validation.Sucess = false;
                torneio.Validation.ErrorMessage = $"Torneio deve iniciar com 16 lutadores, você selecionou {torneio.ListaLutadores.Count()}";
                return torneio;
            }

            lutadores = lutadores.OrderBy(l => l.Idade).ToList();

            

            for (int i = 0; i < lutadores.Count; i += 2)
            {
                torneio.Oitavas[(i / 2)] = new Luta(lutadores[i], lutadores[i+1]);
            }

            for (int i = 0; i < torneio.Oitavas.Count(); i += 2)
            {
                torneio.Quartas[(i / 2)] = new Luta(torneio.Oitavas[i].GetVencedor(), torneio.Oitavas[i+1].GetVencedor());
            }

            for (int i = 0; i < torneio.Quartas.Count(); i += 2)
            {
                torneio.Semi[(i / 2)] = new Luta(torneio.Quartas[i].GetVencedor(), torneio.Quartas[i + 1].GetVencedor());
            }

            torneio.Final = new Luta(torneio.Semi[0].GetVencedor(), torneio.Semi[1].GetVencedor());

            torneio.Vencedor = torneio.Final.GetVencedor();

            torneio.Validation.Sucess = true;
            return torneio;
        }
    }
}
