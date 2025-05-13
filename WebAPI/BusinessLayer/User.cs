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
        [MaxLength(50, ErrorMessage = "Name must not be more than 50 symbols!")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Address must not be more than 50 symbols!")]
        public string Address { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Password must not be more than 100 symbols!")]
        public string Password { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Email must not be more than 30 symbols!")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please, enter valid email!")]
        public string Email { get; set; }


        public List<Order> Orders { get; set; }

        public User()
        {

        }

        private User(string name, string address, string password, string email)
        {
            Name = name;
            Address = address;
            Password = password;
            Email = email;
            Orders = new List<Order>();
        }
        
    }
}
