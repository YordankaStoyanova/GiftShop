using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 symbols!")]
        [MaxLength(50, ErrorMessage = "Name must not be more than 50 symbols!")]
        public string Name { get; set; }

        [MinLength(20, ErrorMessage = "Address must be at least 20 symbols!")]
        [MaxLength(50, ErrorMessage = "Address must not be more than 50 symbols!")]
        public string Address { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 symbols!")]
        [MaxLength(20, ErrorMessage = "Password must not be more than 20 symbols!")]
        public string Password { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Email must not be more than 30 symbols!")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please, enter valid email!")]
        public string Email { get; set; }


        public List<Order> Orders { get; set; }

        private User()
        {

        }

        public User(string name, string password, string email)
        {
            Name = name;
            Password = password;
            Email = email;
            Orders = new List<Order>();
        }
    }
}
