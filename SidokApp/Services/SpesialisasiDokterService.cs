using SidokApp.Models;
using SidokApp.Repository.Interfaces;
using SidokApp.Services.Interfaces;

namespace SidokApp.Services
{
    public class SpesialisasiDokterService : ISpesialisasiDokterService
    {
        private readonly ISpesialisasiDokterRepository _spesialisasiDokterRepository;
        public SpesialisasiDokterService (ISpesialisasiDokterRepository spesialisasiDokterRepository)
        {
            _spesialisasiDokterRepository = spesialisasiDokterRepository;
        }
        public Task<bool> Delete(long id)
        {
           return _spesialisasiDokterRepository.Delete(id);
        }

        public Task<bool> Insert(SpesialiasiDokterModel model)
        {
            return _spesialisasiDokterRepository.Insert(model);
        }

        public Task<bool> Update(SpesialiasiDokterModel model)
        {
            return _spesialisasiDokterRepository.Update(model);
        }
    }
}
