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
    public class DetailsModel : PageModel
    {
        private readonly MovingApp.Data.MovingAppContext _context;

        public DetailsModel(MovingApp.Data.MovingAppContext context)
        {
            _context = context;
        }

        public MovingTask Task { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Task = await _context.Task.FirstOrDefaultAsync(m => m.ID == id);

            if (Task == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
