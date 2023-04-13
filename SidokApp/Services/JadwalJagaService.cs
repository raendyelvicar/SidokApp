using SidokApp.Models;
using SidokApp.Models.Dto;
using SidokApp.Repository.Interfaces;
using SidokApp.Services.Interfaces;

namespace SidokApp.Services
{
    public class JadwalJagaService : IJadwalJagaService
    {
        private readonly IJadwalJagaRepository _jadwalJagaRepository;
        public JadwalJagaService(IJadwalJagaRepository jadwalJagaRepository) 
        {
            _jadwalJagaRepository = jadwalJagaRepository;
        }
        public async Task<bool> Delete(long id)
        {
            return await _jadwalJagaRepository.Delete(id);
        }

        public async Task<bool> Insert(JadwalJagaDto model)
        {
            JadwalJagaModel jadwalJagaModel = new JadwalJagaModel
            {
                IdDokter = model.IdDokter,
                IdPoli = model.IdPoli,
                Hari = model.Hari
            };
            return await _jadwalJagaRepository.Insert(jadwalJagaModel);
        }

        public async Task<List<JadwalJagaModel>> SelectAll()
        {
            return await _jadwalJagaRepository.SelectAll();
        }

        public async Task<bool> Update(JadwalJagaModel model)
        {
            return await _jadwalJagaRepository.Update(model);
        }

        public async Task<List<JadwalJagaDto>> SelectByDokter(long idDokter)
        {
            return await _jadwalJagaRepository.SelectByDokter(idDokter);
        }

        public async Task<List<JadwalJagaDto>> SelectByPoli(long idPoli)
        {
            return await _jadwalJagaRepository.SelectByPoli(idPoli);
        }
    }
}
