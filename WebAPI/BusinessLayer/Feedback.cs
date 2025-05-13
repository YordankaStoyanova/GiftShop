using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public User User { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Rating must be in (0;5]")]
        public int Rating { get; set; }

       
        [MaxLength(1_000, ErrorMessage = "Review must not be more than 1000 symbols!")]
        [MinLength(2, ErrorMessage = "Review must be at least 2 symbols!")]
        public string Review { get; set; }

        private Feedback()
        {

        }

        public Feedback(User user, int rating, string review)
        {
            User = user;
            Rating = rating;
            Review = review;

        }
    }
}
