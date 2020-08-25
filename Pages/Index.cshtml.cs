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


        public string DateSort { get; set; }
        public string StatusSort { get; set; }
        public string CurrentSort { get; set; }
        public IList<MovingTask> Tasks { get; set; }
        public MovingTask Task { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            //Code queryable and add sort. Linq used to retrieve full list.
            CurrentSort = sortOrder;
            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            StatusSort = sortOrder == "Status" ? "status_desc" : "Status";
            
            IQueryable<MovingTask> movingTaskIQ = from t in _db.Task
                                                  select t;

            switch(sortOrder)
            {
                case "date_desc":
                    movingTaskIQ = movingTaskIQ.OrderByDescending(t => t.DueDate);
                    break;
                case "Status":
                    movingTaskIQ = movingTaskIQ.OrderBy(t => t.Status);
                    break;
                case "status_desc":
                    movingTaskIQ = movingTaskIQ.OrderByDescending(t => t.Status);
                    break;
                default:
                    movingTaskIQ = movingTaskIQ.OrderBy(t => t.DueDate);
                    break;
            }

            Tasks = await movingTaskIQ.AsNoTracking().ToListAsync();

        }

        public async Task<IActionResult> OnGetCompleteAsync(int id, string sortOrder)
        {
            if(!TaskExists(id))
            {
                NotFound();
            }

            CurrentSort = sortOrder;

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

            return RedirectToPage(new { sortOrder = CurrentSort});
        }

        public async Task<IActionResult> OnGetIncompleteAsync(int id, string sortOrder)
        {
            if(!TaskExists(id))
            {
                NotFound();
            }

            CurrentSort = sortOrder;

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

            return RedirectToPage(new { sortOrder = CurrentSort });
        }

        public async Task<IActionResult> OnGetCancelAsync(int id, string sortOrder)
        {
            if(!TaskExists(id))
            {
                NotFound();
            }

            CurrentSort = sortOrder;

            Task = await _db.Task.FirstOrDefaultAsync(m => m.ID == id);
            Task.Status = (int)StatusTypes.Cancelled;

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

            return RedirectToPage(new { sortOrder = CurrentSort });
        }

        private bool TaskExists(int id)
        {
            return _db.Task.Any(e => e.ID == id);
        }
    }
}
