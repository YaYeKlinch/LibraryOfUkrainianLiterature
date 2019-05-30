using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryOfUkrLit.Models
{
    public class BookProse
    {

        public int BookProseID { get; set; }
        public int BooksID { get; set; }
        public int ProseID { get; set; }


        public Prose Prose { get; set; }
        public Books Book { get; set; }



    }
}
