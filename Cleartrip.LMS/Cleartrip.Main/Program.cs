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
            var user2 = new User
            {
                Address = "Another Dummy Address",
                FirstName = "Jenny",
                LastName = "Doe"
            };

            var check = libOperations.AddUser(user);
            libOperations.AddUser(user2);
            var userCapacity = userRepo.GetUserById(user.Id).BorrowCapacity;
            Console.WriteLine($"User - {user.FirstName + " " + user.LastName} borrow capacity - {user.BorrowCapacity}");
            try
            {
                Console.WriteLine($"Test to check add same user - Should return Exception");
                libOperations.AddUser(user);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Exception - {ex.InnerException.Message}");
                }
                else
                {
                    Console.WriteLine($"Exception - {ex.Message}");
                }
            }

            //Add Book
            var book = new Book
            {
                Author = "Amitava Ghosh",
                ISBN = String.Concat("ISBN-", Guid.NewGuid().ToString()),
                Price = 560.12M,
                TypeOfBook = BookType.GeneralBook,
                Name = "Dummy New Book"
            };
            var book2 = new Book
            {
                Author = "Isaac Asimov",
                ISBN = String.Concat("ISBN-", Guid.NewGuid().ToString()),
                Price = 760.12M,
                TypeOfBook = BookType.GeneralBook,
                Name = "Robot Book"
            };

            var getBook = bookRepo.SearchByTitle(book.Name);
            Console.WriteLine($"Book - {book.Name} fetched from Book Repository");

            libOperations.AddBook(book);
            libOperations.AddBook(book2);
            var dt = libOperations.IssueBook(book, user);
            userCapacity = userRepo.GetUserById(user.Id).BorrowCapacity;
            Console.WriteLine($"User - {user.FirstName + " " + user.LastName} borrow capacity - {user.BorrowCapacity}");
            if (dt != DateTime.MinValue)
                Console.WriteLine($"Book - {book.Name} has been issued to User - {user.FirstName + " " + user.LastName}, with a return date of - {dt}");
            var isReturned = libOperations.ReturnBook(book);
            if (isReturned)
                Console.WriteLine($"Book => {book.Name}, has been returned.");
            userCapacity = userRepo.GetUserById(user.Id).BorrowCapacity;
            Console.WriteLine($"User - {user.FirstName + " " + user.LastName} borrow capacity - {user.BorrowCapacity}");
            isReturned = libOperations.ReturnBook(book);
            Console.WriteLine($"Book is retuerned, no more pending state.Should be false => {isReturned == false}");
            Console.ReadLine();
        }
    }
}
