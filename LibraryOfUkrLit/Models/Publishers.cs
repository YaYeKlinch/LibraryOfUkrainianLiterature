using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LibraryOfUkrLit.Models
{
    public class Publishers
    {
        public int ID { get; set; }
        [Required]
        [StringLength(1000)]
        public string Name { get; set; }
        [Required]
        [StringLength(1000)]
        [RegularExpression(@"^[А-ЯҐЄЇІ]+[А-Яа-яЇїЄєҐґІі' -]*$")]
        public string City { get; set; }
        [Required]
        [Range(503, 2020)]
        [RegularExpression(@"^[1-9]+[0-9]*$")]
        public int Year { get; set; }
        public ICollection<PublisherBook> PublisherBook { get; set; }






    }
}
