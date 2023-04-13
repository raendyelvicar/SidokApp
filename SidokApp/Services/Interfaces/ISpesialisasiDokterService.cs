using SidokApp.Models;
using SidokApp.Models.Dto;

namespace SidokApp.Services.Interfaces
{
    public interface ISpesialisasiDokterService
    {
        Task<bool> Insert(SpesialiasiDokterModel model);
        Task<bool> Update(SpesialiasiDokterModel model);
        Task<bool> Delete(long id);
    }
}
