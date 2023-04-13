using SidokApp.Models.Dto;
using SidokApp.Models;

namespace SidokApp.Services.Interfaces
{
    public interface IPoliService
    {
        Task<bool> Insert(PoliModel model);
        Task<bool> Update(PoliModel model);
        Task<bool> Delete(long id);
        Task<List<PoliModel>> SelectAll();
        Task<PoliModel> SelectById(long Id);
    }
}
