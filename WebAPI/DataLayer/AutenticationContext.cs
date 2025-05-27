using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AutenticationContext : IAutentication
    {
        private readonly GiftShopDbContext _context;
        public AutenticationContext(GiftShopDbContext giftShopDbContext) 
        {
            _context = giftShopDbContext;
        }
        public async Task<int> Login(string email, string password)
        {
             User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return -1;
            }
            if (user.Password == password)
            {
                return 1;
            }
            return 0; 
        }
        public async Task<int> Register(string email, string password, string userName)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                _context.Users.Add(new User(userName, password, email));
                _context.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
