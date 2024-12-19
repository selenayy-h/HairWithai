using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    }

}