using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinaryClinic.Models
{
    public class Visit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitID { get; set; }
        
        [Display(Name = "Питомец")]
        [Required(ErrorMessage = "Введите питомца")]
        public int AnimalID { get; set; }
               
        [Display(Name = "Хозяин животного")]
        [Required(ErrorMessage = "Введите Хозяина")]
        public int OwnerID { get; set; }

        [Display(Name = "Жалобы")]
        [Required(ErrorMessage = "Введте Жалобы ")]
        public string Complaints { get; set; }

        [Display(Name = "Диагноз")]
        [DisplayFormat(NullDisplayText = "Без диагноза")]
        public string Diagnosis { get; set; }

        [Display(Name = "Лечащий врач")]
        [Required(ErrorMessage = "Введте ФИО ветеринара ")]
        public string AttendinDoctor { get; set; }

        [Display(Name = "Стоимость услуг")]
        public int Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата посещения клиники")]
        public DateTime Dateofvisit { get; set; }
        [Display(Name = "Длительность посещения")]
        public string Duration { get; set; }

        public Animal Animal { get; set; }
        public Owner Owner { get; set; }
    }
}
