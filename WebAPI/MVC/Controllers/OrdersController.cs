using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using DataLayer;
using Newtonsoft.Json;
using NuGet.Protocol;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;


namespace MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IDb<Order,int> _context;
        private readonly IdentityContext _authentication;
        
        public OrdersController(OrderContext context, IdentityContext identity)
        {
            _context = context;
            _authentication = identity;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReadAll());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Read((int)id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection keyValuePairs)
        {
            try
            {
                var json = keyValuePairs["productsString"];
                TempData["products"] =  json;
                return RedirectToAction(nameof(Checkout));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public IActionResult Checkout()
        {
            try
            {
                var products = JsonConvert.DeserializeObject<List<Product>>((TempData["products"]as string[])[0]);
                return View(new Order() { Products = products});
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Create));
            }
        }
        [HttpPost]
        //[Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout([Bind("Id,Address,PhoneNumber,Products")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.User = await _authentication.ReadUserAsync(User.Identity.GetUserId<string>());
                await _context.Create(order);
                HttpContext.Session.Remove("basket");
                return RedirectToAction(nameof(Index),nameof(HomeController));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Read((int)id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address,PhoneNumber,Price")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Update(order);

                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;

                }
                catch (ArgumentException) 
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Read((int)id);
                
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Read(id);
            if (order != null)
            {
                 await _context.Delete(order.Id);
            }

            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
