using SidokApp.Models;
using SidokApp.Repository.Interfaces;
using SidokApp.Services.Interfaces;

namespace SidokApp.Services
{
    public class PoliService : IPoliService
    {
        private readonly IPoliRepository _poliRepository;
        public PoliService(IPoliRepository poliRepository)
        {
            _poliRepository = poliRepository;
        }
        public async Task<bool> Delete(long id)
        {
            return await _poliRepository.Delete(id);
        }

        public async Task<bool> Insert(PoliModel model)
        {
            return await _poliRepository.Insert(model);
        }

        public async Task<List<PoliModel>> SelectAll()
        {
            return await _poliRepository.SelectAll();
        }

        public async Task<PoliModel> SelectById(long Id)
        {
            return await _poliRepository.SelectById(Id);
        }

        public async Task<bool> Update(PoliModel model)
        {
            return await _poliRepository.Update(model);
        }
    }
}
