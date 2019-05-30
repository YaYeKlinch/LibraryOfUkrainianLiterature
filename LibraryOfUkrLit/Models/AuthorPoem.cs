using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryOfUkrLit.Models
{
    public class AuthorPoem
    {
        public int AuthorPoemID { get; set; }
        public int AuthorID { get; set; }
        public int PoemID { get; set; }


        public Poem Poem { get; set; }
        public Author Author { get; set; }
    }
}
