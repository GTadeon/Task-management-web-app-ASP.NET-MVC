using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _Zadatko.Models
{
    public class Zadaci
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        [Required]
        [Range(1,10)]
        public int Tezina { get; set; }

        [Required]
        [Range(1, 10)]
        public int Vaznost { get; set; }

        [Required]
        [StringLength(100)]
        public string KratkiOpis { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RokObavljanja { get; set; }

        [Required]
        [Range(1, 100)]
        public int PostotnaRjesenostZadatka { get; set; }

        [Required]
        public string KorisnikID { get; set; }
    }
}