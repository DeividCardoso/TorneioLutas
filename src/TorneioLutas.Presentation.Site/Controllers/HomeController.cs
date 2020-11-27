using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private readonly ITorneioService _lutadoresService;

        public HomeController(ILogger<HomeController> logger, ITorneioService lutadoresService)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Lutador, LutadorViewModel>().ReverseMap());
            mapper = config.CreateMapper();

            _logger = logger;
            _lutadoresService = lutadoresService;
        }

        public async Task<IActionResult> Index()
        {
            var lutadores = await _lutadoresService.GetAllLutadores();
            var lutadoresVM = mapper.Map<List<LutadorViewModel>>(lutadores);

            return View(lutadoresVM);
        }

        public IActionResult IniciarTorneio()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
