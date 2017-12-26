using Cleartrip.Library.Features.Implementation;
using Cleartrip.Library.Features.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleartrip.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookRepo = new BookRepository();
            var tranRepo = new TransactionOperations();
            var userRepo = new UserRepository();
            var libOperations = new LibraryOperations(bookRepo, tranRepo, userRepo);

            //Add User
            var user = new User
            {
                Address = "Some Dummy Address",
                FirstName = "John",
                LastName = "Doe"
            };
            var check = libOperations.AddUser(user);

            //Add Book
            var book = new Book
            {
                Author = "Amitava Ghosh",
                ISBN = String.Concat("ISBN-", Guid.NewGuid().ToString()),
                Price = 560.12M,
                TypeOfBook = BookType.GeneralBook,
                Name = "Dummy New Book"
            };

            libOperations.AddBook(book);
            var dt = libOperations.IssueBook(book, user);
            if (dt != DateTime.MinValue)
                Console.WriteLine($"Book - {book.Name} has been issued to User - {user.FirstName + " " + user.LastName}, with a return date of - {dt}");
            libOperations.ReturnBook(book);
            Console.ReadLine();
        }
    }
}
