using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinaryClinic.Models
{
    public class Animal
    {
        public int AnimalID { get; set; }
        [Display(Name = "Кличка")]
        [Required(ErrorMessage = "Введте кличку животного")]
        public string NickName { get; set; }
        [Display(Name = "Вид")]
        [Required(ErrorMessage = "Введте вид животного")]
        public string Kind { get; set; }
        [Display(Name = "Порода")]
        [Required(ErrorMessage = "Введте породу животного")]
        public string Breed { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Введте пол животного")]
        public string Sex { get; set; }
        [Display(Name = "Цвет")]
        public string Color { get; set; }
        [Display(Name = "Длина")]
        public string Length { get; set; }
        [Display(Name = "Вес")]
        public string Weight { get; set; }

        public ICollection<Visit> Visits { get; set; }
    }
}