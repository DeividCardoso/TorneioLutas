using System.Collections.Generic;
using System.Threading.Tasks;
using TorneioLutas.Service.Models;

namespace TorneioLutas.Service
{
    public interface ITorneioService
    {
        Task<List<Lutador>> GetAllLutadores();

        Torneio GetTorneio(List<Lutador> lutadores);
    }
}
