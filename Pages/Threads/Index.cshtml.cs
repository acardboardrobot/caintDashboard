using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using caint.Data;
using caint.Models;

namespace caintDashboard.Pages.Threads
{
    public class IndexModel : PageModel
    {
        private readonly caint.Data.caintDBContext _context;

        public IndexModel(caint.Data.caintDBContext context)
        {
            _context = context;
        }

        public IList<Thread> Thread { get;set; }

        public async Task OnGetAsync()
        {
            var ownerId = UserManager.GetUserId(User);
            Thread = await _context.threads.Where(x => x.ownerId == ownerId).ToListAsync();
        }
    }
}
