namespace SidokApp.Models.Dto
{
    public class JadwalJagaDto : JadwalJagaModel
    {
        public string? NamaDokter { get; set; }
        public long IdSpesialisasi { get; set; }
        public string? Spesialisasi { get; set; }
        public string? NamaPoli { get; set; }
        public string? Lokasi { get; set; }
        public string? Jadwal { get; set; }
    }
}
