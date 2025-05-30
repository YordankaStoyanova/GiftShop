﻿using System;
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
            new Product("Mouse","Apple", 25,3)};
                User user = new User("Ivan Ivanov", "Plovdiv,bul.Bulgaria 131", "1234567*", "ivanivanov1@gmail.com");
                Order order = new Order("Plovdiv, bul.Bulgaria 131", "0888888875", user, 75, products);
                int ordersBefore = TestManager.dbContext.Orders.Count();

                await orderContext.Create(order);

                int ordersAfter = TestManager.dbContext.Orders.Count();
                Order lastOrder = TestManager.dbContext.Orders.Last();
                Assert.That(ordersBefore + 1 == ordersAfter && lastOrder.Id == order.Id,
                    "Id are not equal or the order is not created!");
            
        }

            [Test]
            public async Task ReadOrder()
            {
                List<Product> products = new List<Product>() {
            new Product("Mouse", "Apple", 25,3)};
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
            new Product("Mouse", "Apple", 25,3)};
            User user = new User("Ivan Ivanov", "Sofia,bul.Bulgaria 131", "1234567*", "ivanivanov1@gmail.com");
            Order newOrder = new("Varna, bul.Bulgaria 131", "0888888875", user, 75, products);
                 await orderContext.Create(newOrder);

                Order lastOrder = (await orderContext.ReadAll()).Last();
                lastOrder.Address = "Updated Order";

                 await orderContext.Update(lastOrder, false);

                Assert.That((await orderContext.Read(lastOrder.Id)).Address == "Updated Order",
                "Update() does not change the Order's address!");
            }

            [Test]
            public async Task DeleteOrder()
            {
            List<Product> products = new List<Product>() {
            new Product("Mouse", "Apple", 25,3)};
            User user = new User("Ivan Ivanov", "Sofia,bul.Bulgaria 131", "1234567*", "ivanivanov1@gmail.com");
            Order newOrder = new Order("Varna, bul.Bulgaria 131", "0888888875", user, 75, products);
                await orderContext.Create(newOrder);

                List<Order> orders =  await orderContext.ReadAll();
                int orderBefore = orders.Count;
                Order order = orders.Last();

                 await orderContext.Delete(order.Id);

                int orderAfter =( await orderContext.ReadAll()).Count;
                Assert.That(orderBefore == orderAfter + 1, "Delete() does not delete an order!");
            }

        }
}
