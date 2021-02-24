using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using caint.Data;
using caint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace caintDashboard.Pages.Threads
{
    public class IndexModel : dashboard_BasePageModel
    {

        public IndexModel(
            caintDBContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Thread> Thread { get;set; }

        public async Task OnGetAsync()
        {
            var ownerId = UserManager.GetUserId(User);
            Thread = await _context.threads.Where(x => x.ownerId == ownerId).ToListAsync();
        }
    }
}
