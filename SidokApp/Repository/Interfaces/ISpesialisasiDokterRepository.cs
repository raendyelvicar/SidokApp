using SidokApp.Models.Dto;
using SidokApp.Models;

namespace SidokApp.Repository.Interfaces
{
    public interface ISpesialisasiDokterRepository
    {
        Task<bool> Insert(SpesialiasiDokterModel model);
        Task<bool> Update(SpesialiasiDokterModel model);
        Task<bool> Delete(long id);
    }
}
