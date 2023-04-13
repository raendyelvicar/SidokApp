using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SidokApp.Models.Dto;
using SidokApp.Repository.Interfaces;
using SidokApp.Services.Interfaces;

namespace SidokApp.Controllers
{
    public class JadwalController : Controller
    {
        private readonly IPoliService _poliService;
        private readonly IDokterService _dokterService;
        private readonly IJadwalJagaService _jadwalJagaService;
        public JadwalController(IPoliService poliService, IDokterService dokterService, IJadwalJagaService jadwalJagaService) 
        {
            _poliService = poliService;
            _dokterService = dokterService;
            _jadwalJagaService = jadwalJagaService;
        }
        // GET: JadwalJagaController/Create
        public async Task<IActionResult> Create(string nik)
        {
            var poli = await _poliService.SelectAll();
            var dokter =await  _dokterService.SelectByNik(nik);
            JadwalJagaDto jadwalDto = new JadwalJagaDto
            {
                IdDokter = dokter.Id,
                NamaDokter = dokter.Nama
            };
            ViewData["poli"] = poli.Select(x => new SelectListItem { Text = x.Nama, Value = Convert.ToString(x.Id) }).ToList();
            return View(jadwalDto);
        }

        // POST: JadwalJagaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hari, IdDokter, IdPoli")] JadwalJagaDto jadwalDokter)
        {
            try
            {
                await _jadwalJagaService.Insert(jadwalDokter);
                return RedirectToAction("Index", "Dokter");
            }
            catch
            {
                return View(jadwalDokter);
            }
        }
    }
}
