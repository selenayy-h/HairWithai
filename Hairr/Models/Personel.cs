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
        [Required(ErrorMessage = "Uygun günler alanı zorunludur.")]
        public string UygunlukGunler { get; set; } = string.Empty;
        // Örneğin: "Pazartesi,Salı,Çarşamba"

        [Required(ErrorMessage = "Uygunluk başlangıç saati zorunludur.")]
        public TimeSpan UygunlukBaslangic { get; set; } // Örneğin: 09:00

        [Required(ErrorMessage = "Uygunluk bitiş saati zorunludur.")]
        public TimeSpan UygunlukBitis { get; set; } // Örneğin: 18:00


        [Required]
        [ForeignKey("Islem")]
        public int IslemId { get; set; }

        public Islem Islem { get; set; }
        public IList<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

}
