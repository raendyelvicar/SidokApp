using MessagePack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SidokApp.Models;
using SidokApp.Repository;
using SidokApp.Repository.Interfaces;
using SidokApp.Services;
using SidokApp.Services.Interfaces;

namespace SidokApp.Controllers
{
    public class PoliController : Controller
    {
        IPoliService _poliService;
        IJadwalJagaService _jadwalJagaService;
        public PoliController(IPoliService poliService, IJadwalJagaService jadwalJagaService) 
        {
            _poliService = poliService;
            _jadwalJagaService = jadwalJagaService;
        }
        // GET: PoliController
        public async Task<ActionResult> Index()
        {
            var data = await _poliService.SelectAll();
            return View(data);
        }

        // GET: PoliController/Details/5
        public async Task<ActionResult> Details(long id)
        {
            var data = await _poliService.SelectById(id);
            ViewData["DokterJaga"] = await _jadwalJagaService.SelectByPoli(id);
            return View(data);
        }

        // GET: PoliController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoliController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PoliModel model)
        {
            try
            {
                await _poliService.Insert(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PoliController/Edit/5
        public async Task<ActionResult> Edit(long id)
        {
            var data = await _poliService.SelectById(id);
            return View(data);
        }

        // POST: PoliController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PoliModel model)
        {
            try
            {
                await _poliService.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PoliController/Delete/5
        public async Task<ActionResult> Delete(long id)
        {
            var data = await _poliService.SelectById(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        // POST: PoliController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await _poliService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
