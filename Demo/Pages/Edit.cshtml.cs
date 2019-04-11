using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.DAL;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;

        public EditModel(AppDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Customer Customer { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet(int id)
        {
            Customer = _db.Customers.Find(id);
        }

        public IActionResult OnPostSave()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(Customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();

            var msg = $"Customer with name {Customer.Name} was updated!";
            Message = msg;

            return RedirectToPage("/Index");
        }
    }
}