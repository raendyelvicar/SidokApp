using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SidokApp.Helpers;
using SidokApp.Models;
using SidokApp.Models.Dto;
using SidokApp.Repository.Interfaces;
using System.Reflection;

namespace SidokApp.Repository
{
    public class DokterRepository : IDokterRepository
    {
        private readonly DapperContext _context;
        public DokterRepository(DapperContext context) 
        {
            _context = context;
        }
        public async Task<bool> Delete(string nik)
        {
            var query = @"DELETE FROM Dokter WHERE Nik = @nik";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { nik });
                return result > 0;
            }
        }

        public async Task<int> Insert(DokterModel model)
        {
            var query = @"INSERT INTO Dokter([Nama],
                                            [Nip],
                                            [Nik],
                                            [TanggalLahir],
                                            [TempatLahir],
                                            [JenisKelamin]) 
                           VALUES ( @Nama,
                                    @Nip,
                                    @Nik,
                                    @TanggalLahir,
                                    @TempatLahir,
                                    @JenisKelamin);
                            SELECT IDENT_CURRENT('Dokter');";
               
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(query, model);
                return result;
            }
        }

        public async Task<List<DokterDto>> SearchAll(long idSpesialiasi, long idPoli)
        {
            var query = @"SELECT DISTINCT d.Nama, d.Nip, d.Nik, d.TempatLahir, d.TanggalLahir, jk.Deskripsi as JenisKelaminString, s.Nama as Spesialisasi
                FROM DOKTER d
                JOIN SpesialisasiDokter sd ON sd.IdDokter = d.Id 
                JOIN Spesialisasi s ON s.Id = sd.IdSpesialisasi 
                JOIN JenisKelamin jk ON jk.Kode = d.JenisKelamin 
                JOIN JadwalJaga jj ON jj.IdDokter = d.Id
                JOIN Poli p ON p.Id = jj.IdPoli";
            using (var connection = _context.CreateConnection())
            {
                query += @" WHERE p.Id = @idPoli AND s.Id = @idSpesialisasi; ";
                var result = await connection.QueryAsync<DokterDto>(query, new { idSpesialisasi = idSpesialiasi, idPoli = idPoli});
                return result.ToList();
            }
        }

        public async Task<List<DokterDto>> SelectAll()
        {
            var query = @"SELECT d.Nama, d.Nip, d.Nik, d.TempatLahir, d.TanggalLahir, jk.Deskripsi as JenisKelaminString, s.Nama as Spesialisasi
                FROM DOKTER d 
                JOIN SpesialisasiDokter sd ON sd.IdDokter = d.Id
                JOIN Spesialisasi s ON s.Id = sd.IdSpesialisasi
                JOIN JenisKelamin jk ON jk.Kode = d.JenisKelamin";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<DokterDto>(query);
                return result.ToList();
            }
        }

        public async Task<DokterDto> SelectByNik(string nik)
        {
            var query = @"SELECT d.Id, d.Nama, d.Nip, d.Nik, d.TempatLahir, d.TanggalLahir,jk.Kode as JenisKelamin, jk.Deskripsi as JenisKelaminString, s.Id as IdSpesialisasi, s.Nama as Spesialisasi
                FROM DOKTER d 
                JOIN SpesialisasiDokter sd ON sd.IdDokter = d.Id
                JOIN Spesialisasi s ON s.Id = sd.IdSpesialisasi 
                JOIN JenisKelamin jk ON jk.Kode = d.JenisKelamin
                WHERE Nik = @nik;";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<DokterDto>(query, new { nik });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<DokterDto>> SelectByPoli(long idPoli)
        {
            var query = @"SELECT d.Nama, d.Nip, d.Nik, d.TempatLahir, d.TanggalLahir, jk.Deskripsi as JenisKelaminString, s.Nama as Spesialisasi 
                 FROM DOKTER d
                 JOIN SpesialisasiDokter sd ON sd.IdDokter = d.Id
                 JOIN Spesialisasi s ON s.Id = sd.IdSpesialisasi 
                 JOIN JenisKelamin jk ON jk.Kode = d.JenisKelamin 
                 JOIN JadwalJaga jj ON jj.IdDokter = d.Id
                 JOIN Poli p ON p.Id = jj.IdPoli";
            using (var connection = _context.CreateConnection())
            {
                query += @" WHERE p.Id = @idPoli;";
                var result = await connection.QueryAsync<DokterDto>(query, idPoli);
                return result?.ToList();
            }
        }

        public async Task<int> Update(DokterModel model)
        {
            var query = @"UPDATE Dokter 
                           SET [Nama] = @Nama,
                               [TanggalLahir] = @TanggalLahir,
                               [TempatLahir] = @TempatLahir,
                               [JenisKelamin] = @JenisKelamin 
                            OUTPUT inserted.Id
                            WHERE [Nik] = @Nik;
                            EXEC GenerateNIP @Id;";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(query, model);
                return result;
            }
        }

        public async Task<bool> GenerateNIP(int id)
        {
            var query = @"EXEC GenerateNIP @id;";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(query, new { id });
                return result > 0;
            }
        }
    }
}
