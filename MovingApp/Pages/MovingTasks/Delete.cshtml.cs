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
    public class DeleteModel : PageModel
    {
        private readonly MovingApp.Data.MovingAppContext _context;

        public DeleteModel(MovingApp.Data.MovingAppContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Task = await _context.Task.FindAsync(id);

            if (Task != null)
            {
                _context.Task.Remove(Task);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
