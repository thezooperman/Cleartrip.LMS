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
            var users = libOperations.SearchByUser("doe");
            foreach (var usr in users)
            {
                Console.WriteLine($"User Name- {usr.FirstName + " " + usr.LastName}, Borrow capacity - {usr.BorrowCapacity}");
            }

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
            Console.WriteLine();
            Console.WriteLine("Books");
            Console.WriteLine("-----");
            foreach (var b in bookRepo.GetAllBooks())
            {
                Console.WriteLine($"Book Name - {b.Name}, Author - {b.Author}, ISBN - {b.ISBN}");
            }
            Console.WriteLine();
            Console.WriteLine("Users");
            Console.WriteLine("-----");
            foreach (var u in userRepo.GetAllUsers())
            {
                Console.WriteLine($"User - {u.FirstName + " " + u.LastName}, IsBorrowLimited - {u.IsBorrowLimited}, BorrowCapacity - {u.BorrowCapacity} ");
            }
            Console.WriteLine();
            var dt = libOperations.IssueBook(book, user);
            var dt2 = libOperations.IssueBook(book2, user2);
            userCapacity = userRepo.GetUserById(user.Id).BorrowCapacity;
            Console.WriteLine($"User - {user.FirstName + " " + user.LastName} borrow capacity - {user.BorrowCapacity}");
            if (dt != DateTime.MinValue)
                Console.WriteLine($"Book - {book.Name} has been issued to User - {user.FirstName + " " + user.LastName}, with a return date of - {dt}");
            if (dt2 != DateTime.MinValue)
                Console.WriteLine($"Book - {book2.Name} has been issued to User - {user2.FirstName + " " + user2.LastName}, with a return date of - {dt2}");
            var isReturned = libOperations.ReturnBook(book);
            if (isReturned)
                Console.WriteLine($"Book => {book.Name}, has been returned by User - {user.FirstName + " " + user.LastName}.");
            userCapacity = userRepo.GetUserById(user.Id).BorrowCapacity;
            Console.WriteLine($"User - {user.FirstName + " " + user.LastName} borrow capacity - {user.BorrowCapacity}");
            isReturned = libOperations.ReturnBook(book);
            Console.WriteLine($"Book is retuerned, no more pending state.Should be false => {isReturned == false}");

            isReturned = libOperations.ReturnBook(book2);
            if (isReturned)
                Console.WriteLine($"Book => {book2.Name}, has been returned by User - {user2.FirstName + " " + user2.LastName}.");
            Console.WriteLine($"\nGet all transactions by User - {user.FirstName + " " + user.LastName}");
            Console.WriteLine("----------------------------------------");
            foreach (var tran in libOperations.GetTransactionsByUser(user))
            {
                Console.WriteLine($"Book Id - {tran.BookId}, Issue Date - {tran.DateOfIssue}, Due Date - {tran.DueDate}");
            }

            Console.WriteLine($"\nGet all transactions by User - {user2.FirstName + " " + user2.LastName}");
            Console.WriteLine("----------------------------------------");
            foreach (var tran in libOperations.GetTransactionsByUser(user2))
            {
                Console.WriteLine($"Book Id - {tran.BookId}, Issue Date - {tran.DateOfIssue}, Due Date - {tran.DueDate}");
            }
            Console.ReadLine();
        }
    }
}
