using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestLayer
{
    [TestFixture]
    public class OrderContextTest
    {
                       
            static OrderContext orderContext;
            static OrderContextTest()
            {
                orderContext = new OrderContext(TestManager.dbContext);
            }

        [Test]
        public async Task CreateOrder()
        {
          
                List<Product> products = new List<Product>() {
            new Product("Apple", 25,3)};
                User user = new User("Ivan Ivanov", "Plovdiv,bul.Bulgaria 131", "1234567*", "ivanivanov1@gmail.com");
                Order order = new Order("Plovdiv, bul.Bulgaria 131", "0888888875", user, 75, products);
                int ordersBefore = TestManager.dbContext.Orders.Count();

                await orderContext.Create(order);

                int districtsAfter = TestManager.dbContext.Orders.Count();
                Order lastOrder = TestManager.dbContext.Orders.Last();
                Assert.That(ordersBefore + 1 == districtsAfter && lastOrder.Id == order.Id,
                    "Id are not equal or the order is not created!");
            
        }

            [Test]
            public async Task ReadDistrict()
            {
                List<Product> products = new List<Product>() {
            new Product("Apple", 25,3)};
                User user = new User("Ivan Ivanov", "Sofia,bul.Bulgaria 131", "1234567*", "ivanivanov1@gmail.com");
                Order newOrder = new Order("Sofia, bul.Bulgaria 131", "0888888875", user, 75,products);
                await orderContext.Create(newOrder);

                Order order = await orderContext.Read(newOrder.Id);

                Assert.That(order.Address == "Sofia, bul.Bulgaria 131", "Read() does not get Order by id!");
            }

            [Test]
            public  async Task  ReadAllOrder()
            {
                int orderBefore = TestManager.dbContext.Orders.Count();

                int orderAfter = (await orderContext.ReadAll()).Count;

                Assert.That(orderBefore == orderAfter, "ReadAll() does not return all of the Order!");
            }

            [Test]
            public async Task UpdateOrder()
        {
            List<Product> products = new List<Product>() {
            new Product("Apple", 25,3)};
            User user = new User("Ivan Ivanov", "Sofia,bul.Bulgaria 131", "1234567*", "ivanivanov1@gmail.com");
            Order newOrder = new("Varna, bul.Bulgaria 131", "0888888875", user, 75, products);
                 await orderContext.Create(newOrder);

                Order lastOrder = (await orderContext.ReadAll()).Last();
                lastOrder.Address = "Updated Order";

                orderContext.Update(lastOrder, false);

                Assert.That((await orderContext.Read(lastOrder.Id)).Address == "Updated Order",
                "Update() does not change the District's name!");
            }

            [Test]
            public async Task DeleteOrder()
            {
            List<Product> products = new List<Product>() {
            new Product("Apple", 25,3)};
            User user = new User("Ivan Ivanov", "Sofia,bul.Bulgaria 131", "1234567*", "ivanivanov1@gmail.com");
            Order newOrder = new Order("Varna, bul.Bulgaria 131", "0888888875", user, 75, products);
                await orderContext.Create(newOrder);

                List<Order> orders =  await orderContext.ReadAll();
                int orderBefore = orders.Count;
                Order district = orders.Last();

                 await orderContext.Delete(district.Id);

                int districtAfter =( await orderContext.ReadAll()).Count;
                Assert.That(orderBefore == districtAfter + 1, "Delete() does not delete a district!");
            }

            [Test]
            public async Task DeleteOrder2()
            {
            List<Product> products = new List<Product>() {
            new Product("Apple", 25,3)};
            User user = new User("Ivan Ivanov", "Sofia,bul.Bulgaria 131", "1234567*", "ivanivanov1@gmail.com");
            Order newOrder = new Order("Varna, bul.Bulgaria 131", "0888888875", user, 75, products);
                await orderContext.Create(newOrder);

                Order order =  (await orderContext.ReadAll()).Last();
                int orderId = order.Id;

              await  orderContext.Delete(newOrder.Id);

                ArgumentException ex = await Assert.ThrowsAsync<ArgumentException>(async ()=> await orderContext.Read(newOrder.Id));
                Assert.That(ex.Message, Is.EqualTo($"Order with Id{orderId} does not exist!"));
            await Assert.ThrowsAsync<ArgumentException>(async () => await orderContext.Read(orderId));
        }
        }
}
