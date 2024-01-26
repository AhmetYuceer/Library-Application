using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_Uygulamasi.Classes 
{
    internal class BorrowedBook
    {
        public string bookTitle { get; set; }
        public string bookAuthor { get; set; }
        public long _ISBN { get; set; }
        public long numberOfBookCopies { get; set; }

        public DateTime DateTheBookWasLent {get; set;}
        public DateTime DateOfDeliveryOfTheBook {get; set;}
    }
}