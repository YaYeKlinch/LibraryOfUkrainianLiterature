using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryOfUkrLit.Models
{
    public class BookPoems
    {


        public int BookPoemsID { get; set; }
        public int BooksID { get; set; }
        public int PoemID { get; set; }


        public Poem Poem { get; set; }
        public Books Book { get; set; }




    }
}
