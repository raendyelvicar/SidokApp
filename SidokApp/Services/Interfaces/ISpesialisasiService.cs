using Microsoft.AspNetCore.Mvc.Rendering;
using SidokApp.Models;

namespace SidokApp.Services.Interfaces
{
    public interface ISpesialisasiService
    {
        Task<List<SpesialisasiModel>> SelectAll();
    }
}
