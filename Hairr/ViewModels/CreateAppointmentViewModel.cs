using Hairr.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hairr.ViewModels
{
    public class CreateAppointmentViewModel
    {
        [Required]
        [Display(Name = "Hizmet")]
        public int IslemId { get; set; }

        [Required]
        [Display(Name = "Personel")]
        public int PersonelId { get; set; }

        [Required]
        [Display(Name = "Müşteri Adı")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Randevu Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }

        public List<SelectListItem> Islemler { get; set; }
        public List<SelectListItem> Personeller { get; set; }
    }
}
