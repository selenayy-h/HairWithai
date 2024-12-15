using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hairr.Models
{
    public class Personel
    {
        [Key]
        public int PersonelId { get; set; }

        [Required]
        public string Ad { get; set; } = string.Empty;

        [Required]
        public string Soyad { get; set; } = string.Empty;

        [Required]
        public string Sehir { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Islem")]
        public int IslemId { get; set; }

        public Islem Islem { get; set; }
        public IList<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

}
