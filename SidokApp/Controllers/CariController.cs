using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SidokApp.Models.Dto;
using SidokApp.Repository.Interfaces;
using SidokApp.Services.Interfaces;

namespace SidokApp.Controllers
{
    public class CariController : Controller
    {
        private readonly IPoliService _poliService;
        private readonly IDokterService _dokterService;
        private readonly ISpesialisasiService _spesialisasiService;
        public CariController(IPoliService poliService, IDokterService dokterService, ISpesialisasiService spesialisasiService) 
        {
            _poliService = poliService;
            _dokterService = dokterService;
            _spesialisasiService = spesialisasiService;
        }
        // GET: Cari/Create
        public async Task<IActionResult> Create()
        {
            var poli = await _poliService.SelectAll();
            var spesialisasi = await _spesialisasiService.SelectAll();
            ViewData["poli"] = poli.Select(x => new SelectListItem { Text = x.Nama, Value = Convert.ToString(x.Id) }).ToList();
            ViewData["spesialisasi"] = spesialisasi.Select(x => new SelectListItem { Text = x.Nama, Value = Convert.ToString(x.Id) }).ToList();
            return View();
        }

        // POST: Cari/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSpesialisasi, IdPoli")] JadwalJagaDto dokter)
        {
            try
            {
                var res = await _dokterService.SearchAll(dokter.IdSpesialisasi, dokter.IdPoli);
                var poli = await _poliService.SelectAll();
                var spesialisasi = await _spesialisasiService.SelectAll();
                ViewData["poli"] = poli.Select(x => new SelectListItem { Text = x.Nama, Value = Convert.ToString(x.Id) }).ToList();
                ViewData["spesialisasi"] = spesialisasi.Select(x => new SelectListItem { Text = x.Nama, Value = Convert.ToString(x.Id) }).ToList();
                ViewData["dokter"] = res;
                return View();

            }
            catch
            {
                return View(dokter);
            }
        }
    }
}
