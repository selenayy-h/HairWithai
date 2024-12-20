using Hairr.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class CalismaSaati
{
    [Key]
    public int Id { get; set; }



    [Required]
    public int PersonelId { get; set; } // Bu alan `ForeignKey`'e bağlanır

    [ForeignKey("PersonelId")] // Hata burada düzeltilir
    public Personel Personel { get; set; }

    [Required]
    public string Gun { get; set; } = string.Empty; // Örn: Pazartesi, Salı

    [Required]
    public TimeSpan BaslangicSaati { get; set; } // Çalışma başlangıç saati

    [Required]
    public TimeSpan BitisSaati { get; set; } // Çalışma bitiş saati
}
