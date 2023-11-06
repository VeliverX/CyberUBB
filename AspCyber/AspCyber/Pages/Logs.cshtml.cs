using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using AspCyber.Data.Models;

namespace AspCyber.Pages
{
    [Authorize(Roles = "Admin")]
    public class LogsModel : PageModel
    {
        private readonly List<LogViewModel> _log;
        public LogsModel(List<LogViewModel> log)
        {
            _log = log;
        }
        public void OnGet()
        {
            var logs = _log.ToList();
            logs.Add(new LogViewModel { UserNameModel = "Admin@admin.com", Description = "Log Created", Time = DateTime.Now });
            ViewData["Logs"] = logs;
        }

    }
}
