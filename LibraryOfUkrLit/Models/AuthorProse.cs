using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryOfUkrLit.Models
{
    public class AuthorProse
    {
        public int AuthorProseID { get; set; }
        public int AuthorID { get; set; }
        public int ProseID { get; set; }
       

        public Prose Prose { get; set; }
        public Author Author { get; set; }
    }
}
