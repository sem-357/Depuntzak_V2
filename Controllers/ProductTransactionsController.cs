using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Depuntzak_V2.Data;
using Depuntzak_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Depuntzak_V2.Services;

namespace Depuntzak_V2.Controllers
{
    public class ProductTransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ShoppingCartService _cartService;
        private int subtotal;

        public ProductTransactionsController(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        ShoppingCartService cartService)
        {
            _context = context;
            _userManager = userManager;
            _cartService = cartService;
        }


        [Authorize]
        public async Task<IActionResult> PlaceOrderAsync()
        {

            var user = await _userManager.GetUserAsync(User);

            subtotal = CalculateSubtotal();

            var cartItems = _cartService.ViewCart();

            var transaction = new Transaction
            {
                CustomerId = user.Id,
                Subtotal = subtotal
            };



            _context.Transaction.Add(transaction);
            _context.SaveChanges();

            int transactionId = transaction.Id;


            foreach (var item in cartItems)
            {
                var productTransaction = new ProductTransaction
                {
                    TransactionId = transaction.Id,
                    ProductId = item.Product.Id,
                };

                _context.ProductTransaction.Add(productTransaction);
            }

            _context.SaveChanges();



            return RedirectToAction("OrderConfirmation", new { transactionId = transaction.Id });
        }
        public IActionResult OrderConfirmation(int transactionId)
        {
            
            return View("OrderConfirmation", transactionId);
        }


        private int CalculateSubtotal()
        {
            var cartItems = _cartService.ViewCart();
            decimal subtotal = (decimal)cartItems.Sum(item => item.Product.Price * item.Quantity);
            return (int)subtotal;
        }


        // GET: ProductTransactions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductTransaction.Include(p => p.Product).Include(p => p.Transaction);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductTransaction == null)
            {
                return NotFound();
            }

            var productTransaction = await _context.ProductTransaction
                .Include(p => p.Product)
                .Include(p => p.Transaction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productTransaction == null)
            {
                return NotFound();
            }

            return View(productTransaction);
        }

        // GET: ProductTransactions/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
            ViewData["TransactionId"] = new SelectList(_context.Transaction, "Id", "Id");
            return View();
        }

        // POST: ProductTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransactionId,ProductId")] ProductTransaction productTransaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productTransaction.ProductId);
            ViewData["TransactionId"] = new SelectList(_context.Transaction, "Id", "Id", productTransaction.TransactionId);
            return View(productTransaction);
        }

        // GET: ProductTransactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductTransaction == null)
            {
                return NotFound();
            }

            var productTransaction = await _context.ProductTransaction.FindAsync(id);
            if (productTransaction == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productTransaction.ProductId);
            ViewData["TransactionId"] = new SelectList(_context.Transaction, "Id", "Id", productTransaction.TransactionId);
            return View(productTransaction);
        }

        // POST: ProductTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransactionId,ProductId")] ProductTransaction productTransaction)
        {
            if (id != productTransaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productTransaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTransactionExists(productTransaction.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productTransaction.ProductId);
            ViewData["TransactionId"] = new SelectList(_context.Transaction, "Id", "Id", productTransaction.TransactionId);
            return View(productTransaction);
        }

        // GET: ProductTransactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductTransaction == null)
            {
                return NotFound();
            }

            var productTransaction = await _context.ProductTransaction
                .Include(p => p.Product)
                .Include(p => p.Transaction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productTransaction == null)
            {
                return NotFound();
            }

            return View(productTransaction);
        }

        // POST: ProductTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductTransaction == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductTransaction'  is null.");
            }
            var productTransaction = await _context.ProductTransaction.FindAsync(id);
            if (productTransaction != null)
            {
                _context.ProductTransaction.Remove(productTransaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTransactionExists(int id)
        {
          return (_context.ProductTransaction?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
