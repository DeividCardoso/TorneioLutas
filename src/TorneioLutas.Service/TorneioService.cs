using System.Collections.Generic;
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

        public Task<Torneio> GetTorneio()
        {
            throw new System.NotImplementedException();
        }
    }
}
