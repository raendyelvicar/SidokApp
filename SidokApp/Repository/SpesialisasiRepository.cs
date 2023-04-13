using Dapper;
using SidokApp.Helpers;
using SidokApp.Models;
using SidokApp.Models.Dto;
using SidokApp.Repository.Interfaces;

namespace SidokApp.Repository
{
    public class SpesialisasiRepository : ISpesialisasiRepository
    {
        private readonly DapperContext _context;
        public SpesialisasiRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<SpesialisasiModel>> SelectAll()
        {
            var query = "SELECT * FROM Spesialisasi;";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<SpesialisasiModel>(query);
                return result.ToList();
            }
        }
    }
}
