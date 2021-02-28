using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using caint.Data;
using caint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace caintDashboard.Pages.Tenants
{
    public class EditModel : dashboard_BasePageModel
    {

        public EditModel(
            caintDBContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string oldName = _context.tenants.AsNoTracking().FirstOrDefault(m => m.id == Tenant.id).tenantName;

            Tenant.active = true;
            Tenant.ownerId = UserManager.GetUserId(User);

            _context.Attach(Tenant).State = EntityState.Modified;

            //string oldName = currentTenant.tenantName;

            if (_context.tenants.Where(x => x.tenantName == Tenant.tenantName).ToList().Count == 0)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(Tenant.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            List<Thread> threadsToChange = _context.threads.Where(x => x.hostname == oldName).ToList();

            foreach (Thread thread in threadsToChange)
            {
                thread.hostname = Tenant.tenantName;
                _context.Attach(thread).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool TenantExists(long id)
        {
            return _context.tenants.Any(e => e.id == id);
        }
    }
}
