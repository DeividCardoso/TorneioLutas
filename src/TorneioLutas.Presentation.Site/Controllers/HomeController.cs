using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TorneioLutas.Presentation.Site.Models;
using TorneioLutas.Service;
using TorneioLutas.Service.Models;

namespace TorneioLutas.Presentation.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper mapper;

        private readonly ILogger<HomeController> _logger;
        private readonly ITorneioService _torneioService;

        public HomeController(ILogger<HomeController> logger, ITorneioService torneioService)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Lutador, LutadorViewModel>().ReverseMap());
            mapper = config.CreateMapper();

            _logger = logger;
            _torneioService = torneioService;
        }

        public async Task<IActionResult> Index()
        {
            var lutadores = await _torneioService.GetAllLutadores();
            var lutadoresVM = mapper.Map<List<LutadorViewModel>>(lutadores);

            return View(lutadoresVM);
        }

        [HttpPost]
        public IActionResult IniciarTorneio(List<LutadorViewModel> lutadoresVM)
        {
            var lutadores = new List<Lutador>();

            foreach (var lutadorVM in lutadoresVM)
            {
                if (lutadorVM.IsSelected)
                {
                    lutadores.Add(mapper.Map<Lutador>(lutadorVM));
                }
            }

            Torneio torneio = _torneioService.GetTorneio(lutadores);
            if (!torneio.Validation.Sucess)
            {
                ModelState.AddModelError("", torneio.Validation.ErrorMessage);
                return View("Index", lutadoresVM);
            }                                

            return View("Resultado", torneio);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
