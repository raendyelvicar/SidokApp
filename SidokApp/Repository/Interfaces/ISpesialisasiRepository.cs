using SidokApp.Models;

namespace SidokApp.Repository.Interfaces
{
    public interface ISpesialisasiRepository
    {
        Task<List<SpesialisasiModel>> SelectAll();
    }
}
