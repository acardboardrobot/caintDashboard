using caint.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace caintDashboard.Pages
{
    public class dashboard_BasePageModel : PageModel
    {
        protected caintDBContext _context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }

        public dashboard_BasePageModel(
            caintDBContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base()
        {
            _context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        } 
    }
}