using System.ComponentModel.DataAnnotations;

namespace SidokApp.Models
{
    public class DokterModel
    {
        public long Id { get; set; }
        public string Nama { get; set; }
        public string Nip { get; set; }
        public string Nik { get; set; }
        [DataType(DataType.Date)]
        public DateTime TanggalLahir { get; set; }
        public string TempatLahir { get; set; }
        public int JenisKelamin { get; set; }



    }
}
