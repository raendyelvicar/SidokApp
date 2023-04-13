using SidokApp.Models;
using SidokApp.Models.Dto;

namespace SidokApp.Repository.Interfaces
{
    public interface IDokterRepository
    {
        Task<int> Insert(DokterModel model);
        Task<int> Update(DokterModel model);    
        Task<bool> Delete(string nik);
        Task<List<DokterDto>> SelectAll();
        Task<DokterDto> SelectByNik(string nik);
        Task<List<DokterDto>> SelectByPoli(long idPoli);
        Task<List<DokterDto>> SearchAll(long idSpesialiasi, long idPoli);
        Task<bool> GenerateNIP(int id);

    }
}
