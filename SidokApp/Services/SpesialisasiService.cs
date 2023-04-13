using Microsoft.AspNetCore.Mvc.Rendering;
using SidokApp.Models;
using SidokApp.Repository;
using SidokApp.Repository.Interfaces;
using SidokApp.Services.Interfaces;

namespace SidokApp.Services
{
    public class SpesialisasiService : ISpesialisasiService
    {
        private readonly ISpesialisasiRepository _spesialisasiRepository;
        public SpesialisasiService(ISpesialisasiRepository spesialisasiRepository) 
        {
            _spesialisasiRepository = spesialisasiRepository;
        }
        public async Task<List<SpesialisasiModel>> SelectAll()
        {
            var data = await _spesialisasiRepository.SelectAll();
            return data;

        }
    }
}
