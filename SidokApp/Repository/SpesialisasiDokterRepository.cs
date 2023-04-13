using Dapper;
using MessagePack;
using SidokApp.Helpers;
using SidokApp.Models;
using SidokApp.Models.Dto;
using SidokApp.Repository.Interfaces;

namespace SidokApp.Repository
{
    public class SpesialisasiDokterRepository : ISpesialisasiDokterRepository
    {
        private readonly DapperContext _context;
        public SpesialisasiDokterRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(long id)
        {
            var query = @"DELETE FROM SpesialisasiDokter WHERE IdDokter = @id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { id });
                return result > 0;
            }
        }

        public async Task<bool> Insert(SpesialiasiDokterModel model)
        {
            var query = @"INSERT INTO SpesialisasiDokter([IdSpesialisasi], [IdDokter]) 
                           VALUES (@IdSpesialisasi, @IdDokter)";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<SpesialiasiDokterModel>(query, model);
                return result.Any();
            }
        }

        public async Task<bool> Update(SpesialiasiDokterModel model)
        {
            var query = @"UPDATE SpesialisasiDokter 
                           SET [IdSpesialisasi] = @IdSpesialisasi,
                               [IdDokter] = @IdDokter
                            WHERE [IdDokter] = @IdDokter;";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<SpesialiasiDokterModel>(query, model);
                return result.Any();
            }
        }
    }
}
