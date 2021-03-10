using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace caintDashboard.Pages
{
    public class SetupModel : PageModel
    {
        private readonly ILogger<SetupModel> _logger;

        public SetupModel(ILogger<SetupModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
