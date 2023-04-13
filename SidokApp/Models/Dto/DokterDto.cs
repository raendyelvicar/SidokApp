using System.ComponentModel.DataAnnotations;

namespace SidokApp.Models.Dto
{
    public class DokterDto : DokterModel
    {
        public string? JenisKelaminString { get; set; }
        public long IdSpesialisasi { get; set; }
        public string? Spesialisasi { get; set; }
        public long IdPoli { get; set; }
        public string? Gelar { get; set; }
        public List<PoliModel>? JadwalJaga { get; set; }
    }
}
