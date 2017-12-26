using System;

namespace Cleartrip.Library.Features.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public bool IsBorrowLimited { get; set; } = false;
        //Default borrow capacity : 3
        public int BorrowCapacity { get; set; } = 3;
    }
}
