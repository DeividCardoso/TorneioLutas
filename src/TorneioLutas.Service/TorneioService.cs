using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneioLutas.Service.Helpers;
using TorneioLutas.Service.Models;

namespace TorneioLutas.Service
{
    public class TorneioService : ITorneioService
    {
        public async Task<List<Lutador>> GetAllLutadores()
        {
            return await HttpHelper.GetAsync<List<Lutador>>("http://localhost:3000/lutadores");
        }

        public Task<Torneio> GetTorneio(List<Lutador> lutadores)
        {
            if (lutadores.Count != 16)
                throw new System.Exception("diferente de 16");

            lutadores = lutadores.OrderBy(l => l.Idade).ToList();

            Torneio torneio = new Torneio();

            for (int i = 0; i < lutadores.Count; i+=2)
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

            throw new System.NotImplementedException();
        }
    }
}
