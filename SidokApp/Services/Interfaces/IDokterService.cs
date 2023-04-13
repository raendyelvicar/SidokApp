using SidokApp.Models.Dto;
using SidokApp.Models;

namespace SidokApp.Services.Interfaces
{
    public interface IDokterService
    {
        Task<bool> Insert(DokterDto model);
        Task<bool> Update(DokterDto model);
        Task<bool> Delete(string nik);
        Task<List<DokterDto>> SelectAll();
        Task<DokterDto> SelectByNik(string nik);
        Task<List<DokterDto>> SelectByPoli(long idPoli);
        Task<List<DokterDto>> SearchAll(long idSpesialiasi, long idPoli);
    }
}
