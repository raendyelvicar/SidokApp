using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Web;
using SidokApp.Models.Dto;
using SidokApp.Services.Interfaces;
using System.Collections.Generic;

namespace SidokApp.Controllers
{
    public class DokterController : Controller
    {
        private readonly IDokterService _dokterService;
        private readonly ISpesialisasiService _spesialisasiService;
        private readonly ISpesialisasiDokterService _spesialisasiDokterService;
        private readonly IJadwalJagaService _jadwalJagaService;

        public DokterController(IDokterService dokterService, ISpesialisasiService spesialisasiService, 
            ISpesialisasiDokterService spesialisasiDokterService, IJadwalJagaService jadwalJagaService)
        {
            _dokterService = dokterService;
            _spesialisasiService = spesialisasiService;
            _spesialisasiDokterService = spesialisasiDokterService;
            _jadwalJagaService = jadwalJagaService;
        }

        // GET: Dokter
        public async Task<IActionResult> Index()
        {
            var model = await _dokterService.SelectAll();
            return View(model);
        }

        // GET: Dokter/Details/5
        public async Task<IActionResult> Details(string Nik)
        {
            var dokterDto = await _dokterService.SelectByNik(Nik);
            if (Nik == null || dokterDto == null)
            {
                return NotFound();
            }
            ViewData["JadwalJaga"] = await _jadwalJagaService.SelectByDokter(dokterDto.Id);
            return View(dokterDto);
        }

        // GET: Dokter/Create
        public async Task<IActionResult> Create()
        {
            var spesialisasi = await _spesialisasiService.SelectAll();
            ViewData["Spesialisasi"] = spesialisasi.Select(x => new SelectListItem { Text=x.Nama, Value=Convert.ToString(x.Id) }).ToList();
            return View();
        }

        // POST: Dokter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nama,Nip,Nik,TanggalLahir,TempatLahir,JenisKelamin,IdSpesialisasi")] DokterDto dokterDto)
        {
            if (ModelState.IsValid)
            {
                await _dokterService.Insert(dokterDto);
                return RedirectToAction(nameof(Index));
            }
            return View(dokterDto);
        }

        // GET: Dokter/Edit/5
        public async Task<IActionResult> Edit(string nik)
        {
            var dokterDto = await _dokterService.SelectByNik(nik);
            if (nik == null || dokterDto == null)
            {
                return NotFound();
            }

            var spesialisasi = await _spesialisasiService.SelectAll();
            List<SelectListItem> spesialisasiList = spesialisasi.Select(x => new SelectListItem { Text = x.Nama, Value = Convert.ToString(x.Id) }).ToList();
            var selected = spesialisasiList.Find(x => x.Value == dokterDto.IdSpesialisasi.ToString());
            selected.Selected = true;
            ViewData["Spesialisasi"] = spesialisasiList;
            return View(dokterDto);
        }

        // POST: Dokter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string nik, [Bind("Nama,Nip,Nik,TanggalLahir,TempatLahir,JenisKelamin,IdSpesialisasi")] DokterDto dokterDto)
        {
            if (nik != dokterDto.Nik)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _dokterService.Update(dokterDto);
               
                return RedirectToAction(nameof(Index));
            }
            return View(dokterDto);
        }

        // GET: Dokter/Delete/5
        public async Task<IActionResult> Delete(string nik)
        {
            var dokterDto = await _dokterService.SelectByNik(nik);
            if (dokterDto == null)
            {
                return NotFound();
            }

            return View(dokterDto);
        }

        // POST: Dokter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string nik)
        {
            await _dokterService.Delete(nik);
            return RedirectToAction(nameof(Index));
        }
    }
}
