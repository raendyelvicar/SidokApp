using SidokApp.Models;
using SidokApp.Models.Dto;

namespace SidokApp.Services.Interfaces
{
    public interface IJadwalJagaService
    {
        Task<bool> Insert(JadwalJagaDto model);
        Task<bool> Update(JadwalJagaModel model);
        Task<bool> Delete(long id);
        Task<List<JadwalJagaModel>> SelectAll();
        Task<List<JadwalJagaDto>> SelectByDokter(long idDokter);
        Task<List<JadwalJagaDto>> SelectByPoli(long idPoli);
    }
}
