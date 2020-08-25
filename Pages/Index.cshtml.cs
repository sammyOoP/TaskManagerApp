using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MovingApp.Data;
using MovingApp.Models;
using MovingApp.Utils;

namespace MovingApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MovingAppContext _db;

        public IndexModel(ILogger<IndexModel> logger, MovingAppContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IList<MovingTask> Tasks { get; set; }
        public MovingTask Task { get; set; }

        public async Task OnGetAsync()
        {
            Tasks = await _db.Task.ToListAsync();
        }

        public async Task<IActionResult> OnGetCompleteAsync(int id)
        {
            if(!TaskExists(id))
            {
                NotFound();
            }

            Task = await _db.Task.FirstOrDefaultAsync(m => m.ID == id);
            Task.Status = (int)StatusTypes.Complete;

            _db.Attach(Task).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!TaskExists(Task.ID))
                {
                    NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage();
        }

                public async Task<IActionResult> OnGetIncompleteAsync(int id)
        {
            if(!TaskExists(id))
            {
                NotFound();
            }

            Task = await _db.Task.FirstOrDefaultAsync(m => m.ID == id);
            Task.Status = (int)StatusTypes.Incomplete;

            _db.Attach(Task).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!TaskExists(Task.ID))
                {
                    NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage();
        }

        private bool TaskExists(int id)
        {
            return _db.Task.Any(e => e.ID == id);
        }
    }
}
