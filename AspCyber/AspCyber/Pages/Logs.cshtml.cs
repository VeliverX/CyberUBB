using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspCyber.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspCyber.Data; // Zast¹p "YourNamespace" odpowiedni¹ przestrzeni¹ nazw

namespace AspCyber.Pages
{
    [Authorize(Roles = "Admin")]
    public class LogsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LogsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserLog> UserLogs { get; set; }

        public async Task OnGetAsync()
        {
            UserLogs = await _context.UserLogs.ToListAsync();
        }
    }
}
