using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class UserContext : IDb<User, int>
    {
        private readonly GiftShopDbContext dbContext;
        public UserContext(GiftShopDbContext context)
        {
            dbContext = context;
        }

        public void Create(User item)
        {
            dbContext.Users.Add(item);
            dbContext.SaveChanges();
        }

        public User Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<User> query = dbContext.Users;
            if (useNavigationalProperties) query = query.Include(q => q.Orders);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();
            User user = query.FirstOrDefault(x => x.Id == key);
            if (user is null) throw new KeyNotFoundException();
            return user;
        }

        public List<User> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<User> query = dbContext.Users;

            if (useNavigationalProperties)
            {
                query = query.Include(u => u.Orders);
            }

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }

            return query.ToList();
        }

        public void Update(User item, bool useNavigationalProperties = false)
        {
            User user = Read(item.Id, useNavigationalProperties);
            dbContext.Entry(user).CurrentValues.SetValues(item);
            if (useNavigationalProperties)
            {
                
                List<Order> orders = new List<Order>();
                foreach (var ord in item.Orders)
                {
                    Order order = dbContext.Orders.FirstOrDefault(x => x.Id == ord.Id);
                    if (order == null) orders.Add(ord);
                    else orders.Add(order);
                }
                user.Orders = item.Orders;
            }
            dbContext.SaveChanges();
        }

        public void Delete(int key)
        {
            User user = Read(key);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }
    }
}
