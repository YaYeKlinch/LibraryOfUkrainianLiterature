﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LibraryOfUkrLit.Models
{
    public class Poem
    {
        public int PoemID { get; set; }
        [Required]
        [StringLength(1000)]
        [RegularExpression(@"^[А-ЯҐЄЇІ]+[А-Яа-яЇїЄєҐґІі',.!?() -]*$")]
        public string Name { get; set; }
        [Required]
        [Range(503, 2020)]
        [RegularExpression(@"^[1-9]+[0-9]*$")]
        public int Year { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        [RegularExpression(@"^[1-9]+[0-9]*$")]
        public int CountLine { get; set; }

        public ICollection<AuthorPoem> AuthorPoem { get; set; }
        public ICollection<BookPoems> BookPoems { get; set; }
    }
}
