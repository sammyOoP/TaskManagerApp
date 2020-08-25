using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovingApp.Data;
using MovingApp.Models;

namespace MovingApp.Pages.MovingTasks
{
    public class IndexModel : PageModel
    {
        private readonly MovingApp.Data.MovingAppContext _context;

        public IndexModel(MovingApp.Data.MovingAppContext context)
        {
            _context = context;
        }

        public IList<MovingTask> Task { get;set; }

        public async Task OnGetAsync()
        {
            Task = await _context.Task.ToListAsync();
        }
    }
}
