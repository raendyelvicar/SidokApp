using SidokApp.Models;
using SidokApp.Models.Dto;
using SidokApp.Repository.Interfaces;
using SidokApp.Services.Interfaces;

namespace SidokApp.Services
{
    public class DokterService : IDokterService
    {
        private readonly IDokterRepository _dokterRepository;
        private readonly ISpesialisasiDokterRepository _spesialisasiDokterRepository;
        public DokterService(IDokterRepository dokterRepository, ISpesialisasiDokterRepository spesialisasiDokterRepository) 
        {
            _dokterRepository = dokterRepository;
            _spesialisasiDokterRepository = spesialisasiDokterRepository;
        }
        public async Task<bool> Delete(string nik)
        {
            var data = await _dokterRepository.SelectByNik(nik);
           
            if(data != null)
            {
                await _dokterRepository.Delete(nik);
                return true;
            }
            return false;
        }

        public async Task<bool> Insert(DokterDto model)
        {
            DokterModel dokterModel = new DokterModel()
            {
                Nama = model.Nama,
                Nik = model.Nik,
                Nip = model.Nip,
                TanggalLahir = model.TanggalLahir,
                TempatLahir = model.TempatLahir,
                JenisKelamin = model.JenisKelamin
            };
            var insertDokter = await _dokterRepository.Insert(dokterModel);
            if (insertDokter > 0)
            {
                await _dokterRepository.GenerateNIP(insertDokter);
                await _spesialisasiDokterRepository.Insert(new SpesialiasiDokterModel { IdDokter = Convert.ToInt64(insertDokter), IdSpesialisasi = model.IdSpesialisasi });
                return true;
            }
            return false;
        }

        public async Task<List<DokterDto>> SearchAll(long idSpesialiasi, long idPoli)
        {
            return await _dokterRepository.SearchAll(idSpesialiasi, idPoli);
        }

        public async Task<List<DokterDto>> SelectAll()
        {
            return await _dokterRepository.SelectAll();
        }

        public async Task<DokterDto> SelectByNik(string nik)
        {
            return await _dokterRepository.SelectByNik(nik);
        }

        public async Task<List<DokterDto>> SelectByPoli(long idPoli)
        {
            return await _dokterRepository.SelectByPoli(idPoli);
        }

        public async Task<bool> Update(DokterDto model)
        {
            var data = await _dokterRepository.SelectByNik(model.Nik);

            if(data != null)
            {
                DokterModel dokterModel = new DokterModel()
                {
                    Nama = model.Nama,
                    Nik = model.Nik,
                    Nip = model.Nip,
                    TanggalLahir = model.TanggalLahir,
                    TempatLahir = model.TempatLahir,
                    JenisKelamin = model.JenisKelamin
                };
                var updatedData = await _dokterRepository.Update(dokterModel);
                if (updatedData > 0)
                {
                    await _dokterRepository.GenerateNIP(updatedData);
                    await _spesialisasiDokterRepository.Update(new SpesialiasiDokterModel { IdDokter = Convert.ToInt64(updatedData), IdSpesialisasi = model.IdSpesialisasi });
                    return true;
                }
            }

            return false;
        }
    }
}
