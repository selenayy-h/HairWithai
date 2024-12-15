using System.ComponentModel.DataAnnotations;

namespace Hairr.Models
{
    public class Islem
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public string? IslemAdi { get; set; }

        public int Time { get; set; }


        [Range(0, 10000)]
        public int Price { get; set; }
        public IList<Appointment>? Appointments { get; set; }
        public IList<Personel>? Personels { get; set; }
    }
}
