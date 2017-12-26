using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleartrip.Library.Features.Model
{
    public class Book
    {
        public BookType TypeOfBook { get; set; }
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String ISBN { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        public String Author { get; set; }
    }

    public enum BookType
    {
        Journals = 1,
        Magazine = 2,
        AudioBooks = 3,
        Video = 4,
        GeneralBook = 5
    }
}
