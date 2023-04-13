using SidokApp.Models.Dto;
using SidokApp.Models;

namespace SidokApp.Repository.Interfaces
{
    public interface IPoliRepository
    {
        Task<bool> Insert(PoliModel model);
        Task<bool> Update(PoliModel model);
        Task<bool> Delete(long id);
        Task<List<PoliModel>> SelectAll();
        Task<PoliModel> SelectById(long idPoli);
    }
}
