using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryOfUkrLit.Models
{
    public class PublisherBook
    {


        public int PublisherBookID { get; set; }
        public int BookID { get; set; }
        public int PublisherID { get; set; }


        public Publishers Publisher { get; set; }
        public Books Book { get; set; }







    }
}
