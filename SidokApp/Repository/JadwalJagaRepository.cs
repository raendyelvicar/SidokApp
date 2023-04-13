using Dapper;
using MessagePack;
using SidokApp.Helpers;
using SidokApp.Models;
using SidokApp.Models.Dto;
using SidokApp.Repository.Interfaces;

namespace SidokApp.Repository
{
    public class JadwalJagaRepository : IJadwalJagaRepository
    {
        private readonly DapperContext _context;
        public JadwalJagaRepository(DapperContext context) 
        {
            _context = context;
        }
        

        public async Task<bool> Delete(long id)
        {
            var query = @"DELETE FROM JadwalJaga WHERE IdDokter = @id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { id });
                return result > 0;
            }
        }

        public async Task<bool> Insert(JadwalJagaModel model)
        {
            var query = @"INSERT INTO JadwalJaga([Hari], 
                                            [IdPoli],
                                            [IdDokter]) 
                           VALUES ( @Hari,
                                    @IdPoli,
                                    @IdDokter);";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query,model);
                return result > 0;
            }
        }

        public async Task<List<JadwalJagaModel>> SelectAll()
        {
            var query = @"SELECT * FROM JadwalJaga";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<JadwalJagaModel>(query);
                return result.ToList();
            }
        }

        public async Task<bool> Update(JadwalJagaModel model)
        {
            var query = @"UPDATE JadwalJaga 
                           SET [IdDokter] = @IdDokter,
                               [IdPoli] = @IdPoli
                            WHERE [IdDokter] = @IdDokter;";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync(query, new {IdDokter = model.IdDokter});
                return result.Any();
            }
        }

        public async Task<JadwalJagaModel> SelectById(long Id)
        {
            var query = @"SELECT * FROM JadwalJaga Where IdDokter = @Id";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<JadwalJagaModel>(query, new { Id });
                return result?.FirstOrDefault();
            }
        }

        public async Task<List<JadwalJagaDto>> SelectByDokter(long idDokter)
        {
            var query = @"SELECT p.Nama as NamaPoli, p.Lokasi, jj.Hari as Jadwal
                          FROM Poli p
                          JOIN JadwalJaga jj ON jj.IdPoli = p.Id
                          JOIN Dokter d ON d.Id = jj.IdDokter
                          WHERE d.Id  = @idDokter";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<JadwalJagaDto>(query, new { idDokter });
                return result.ToList();
            }
        }

        public async Task<List<JadwalJagaDto>> SelectByPoli(long idPoli)
        {
            var query = @"SELECT d.Nama as NamaDokter, s.Nama as Spesialisasi, p.Nama as NamaPoli, p.Lokasi, jj.Hari as Jadwal
                          FROM Poli p
                          JOIN JadwalJaga jj ON jj.IdPoli = p.Id
                          JOIN Dokter d ON d.Id = jj.IdDokter
						  JOIN SpesialisasiDokter sd ON sd.IdDokter = d.Id
						  JOIN Spesialisasi s ON s.Id = sd.IdSpesialisasi
                          WHERE p.Id  = @idPoli";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<JadwalJagaDto>(query, new { idPoli });
                return result.ToList();
            }
        }
    }
}
