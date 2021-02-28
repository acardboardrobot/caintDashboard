using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using caint.Data;
using caint.Models;

namespace caintDashboard.Pages.Tenants
{
    public class DeleteModel : PageModel
    {
        private readonly caint.Data.caintDBContext _context;

        public DeleteModel(caint.Data.caintDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tenant Tenant { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tenant = await _context.tenants.FirstOrDefaultAsync(m => m.id == id);

            if (Tenant == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tenant = await _context.tenants.FindAsync(id);

            if (Tenant != null)
            {
                _context.tenants.Remove(Tenant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
