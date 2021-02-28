using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using caint.Data;
using caint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace caintDashboard.Pages.Tenants
{
    public class CreateModel : dashboard_BasePageModel
    {

        public CreateModel(
            caintDBContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Tenant Tenant { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_context.tenants.Where(x => x.tenantName == Tenant.tenantName).ToList().Count == 0)
            {
                Tenant.active = true;
                Tenant.ownerId = UserManager.GetUserId(User);

                _context.tenants.Add(Tenant);
                await _context.SaveChangesAsync();
            }

            

            return RedirectToPage("./Index");
        }
    }
}
