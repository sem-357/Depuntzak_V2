using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Depuntzak_V2.Data;
using Depuntzak_V2.Models;
using Depuntzak_V2.Services;

namespace Depuntzak_V2.Controllers
{
    public class MenusController : Controller
    {
        private readonly ShoppingCartService _cartService;
        private readonly ApplicationDbContext _context;
        private int? subtotal;

        public MenusController(ShoppingCartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        public IActionResult AddToCart(int productId)
        {
            var product = _context.Product.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _cartService.AddToCart(product);
            }
            return RedirectToAction("Index"); 
        }

        public IActionResult ViewCart()
        {
            var productsToAdd= _context.Product.Take(2).ToList();

            foreach (var product in productsToAdd)
            {
                _cartService.AddToCart(product);
            }
            var cartItems = _cartService.ViewCart();
            subtotal = cartItems.Sum(item => item.Product.Price * item.Quantity);

            ViewBag.Subtotal = subtotal; 
            return View("/Views/Cart/ViewCart.cshtml", cartItems);
        }

        public IActionResult Index()
        {
            var products = _context.Product.ToList();
            return View(products);
        }

        

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Menu == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Menu'  is null.");
            }
            var menu = await _context.Menu.FindAsync(id);
            if (menu != null)
            {
                _context.Menu.Remove(menu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
          return (_context.Menu?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Products(int id)
{
    // Zoek het menu op basis van het id
    var menu = _context.Menu.Include(m => m.Products).FirstOrDefault(m => m.Id == id);

    if (menu == null)
    {
        return NotFound();
    }

    return View(menu.Products);
}

    }
}
