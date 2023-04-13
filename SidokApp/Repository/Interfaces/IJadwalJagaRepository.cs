using SidokApp.Models.Dto;
using SidokApp.Models;

namespace SidokApp.Repository.Interfaces
{
    public interface IJadwalJagaRepository
    {
        Task<bool> Insert(JadwalJagaModel model);
        Task<bool> Update(JadwalJagaModel model);
        Task<bool> Delete(long id);
        Task<List<JadwalJagaModel>> SelectAll();
        Task<JadwalJagaModel> SelectById(long Id);
        Task<List<JadwalJagaDto>> SelectByDokter(long idDokter);
        Task<List<JadwalJagaDto>> SelectByPoli(long idPoli);
    }
}
