using Dapper;
using Microsoft.EntityFrameworkCore;
using SidokApp.Helpers;
using SidokApp.Models;
using SidokApp.Models.Dto;
using SidokApp.Repository.Interfaces;

namespace SidokApp.Repository
{
    public class PoliRepository : IPoliRepository
    {
        private readonly DapperContext _context;
        public PoliRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(long id)
        {

            var query = @"DELETE FROM Poli WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { id });
                return result > 0;
            }
        }

        public async Task<bool> Insert(PoliModel model)
        {
            var query = @"INSERT INTO Poli([Nama],
                                            [Lokasi]) 
                           VALUES ( @Nama,
                                    @Lokasi);";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, model);
                return result > 0;
            }
        }

        public async Task<List<PoliModel>> SelectAll()
        {

            var query = @"SELECT * FROM Poli";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<PoliModel>(query);
                return result.ToList();
            }
        }

        public async Task<bool> Update(PoliModel model)
        {
            var query = @"UPDATE Poli 
                           SET [Nama] = @Nama,
                               [Lokasi] = @Lokasi
                            WHERE [Id] = @Id;";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, model);
                return result > 0;
            }
        }

        public async Task<PoliModel> SelectById(long idPoli)
        {
            var query = @"SELECT *
                      FROM Poli p
                      WHERE Id  = @idPoli";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<PoliModel>(query, new { idPoli });
                return result.FirstOrDefault();
            }
        }
    }
}
