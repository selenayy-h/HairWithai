using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hairr.Models;

namespace Hairr.Models
{
    public class Appointment
    {

        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Islem")]
        public int IslemId { get; set; }
        public Islem Islem { get; set; }

        [Required]
        [ForeignKey("Personel")]
        public int PersonelId { get; set; }
        public Personel Personel { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Status { get; set; } = "Beklemede";

        [NotMapped]
        public DateTime AppointmentEndDate => AppointmentDate.AddMinutes(Islem.Time);
    }

}
//Appointment'ın Amacı: Appointment (Randevu) sınıfı, belirli bir tarihte, belirli bir personel ile yapılan bir randevuyu temsil eder. Randevu oluşturma işlemi sırasında personelin uygun gün ve saatlerde olup olmadığı kontrol edilir,
//    ancak bu bilgi randevunun kendisinde saklanmaz.