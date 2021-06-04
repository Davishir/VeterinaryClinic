using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinaryClinic.Models
{
    public class Owner
    {

        
        public int OwnerID { get; set; }
        
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "ФИО")]
        [Required(ErrorMessage = "Введте ФИО клиента")]
        public string Fio { get; set; }
        [Display(Name = "Адрес")]
        [Required(ErrorMessage = "Введте адрес клиента")]
        public string Address { get; set; }
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Введте номер телефона клиента")]
        public string PhoneNumber { get; set; }
        public ICollection<Visit> Visits { get; set; }
    }
}
