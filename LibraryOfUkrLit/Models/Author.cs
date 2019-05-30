using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace LibraryOfUkrLit.Models
{
    public class Author
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[А-ЯҐЄЇІ]+[А-Яа-яЇїЄєҐґІі'-]*$")]
        public string LastName { get; set; }
        [Required]
        [StringLength(1000)]
        [RegularExpression(@"^[А-ЯҐЄЇІ]+[А-Яа-яЇїЄєҐґІі',.!?() -]*$")]
        public string Information { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth date")]
        public DateTime Year { get; set; }
        public ICollection<AuthorPoem> AuthorPoem { get; set; }
        public ICollection<AuthorProse> AuthorProse { get; set; }
    }
}
