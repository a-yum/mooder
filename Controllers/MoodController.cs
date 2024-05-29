using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mooder.Data;
using Mooder.Exceptions;
using Mooder.Models;

namespace Mooder.Controllers
{
    public class MoodController : Controller
    {
        private readonly MooderDBContext _context;

        public MoodController(MooderDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index(UserMood? searchMood, DateTime? searchDate)
        {
            if (_context.UserMoodEntry == null)
            {
                return Problem("Entity set 'MooderContext.UserMoodEntry' is null.");
            }

            var entries = from e in _context.UserMoodEntry
                          select e;

            if (searchMood.HasValue)
            {
                entries = entries.Where(e => e.Mood == searchMood);
            }

            if (searchDate.HasValue)
            {
                entries = entries.Where(e => e.Date == searchDate.Value.Date);
            }

            var moodSelectList = new SelectList(Enum.GetValues(typeof(UserMood)).Cast<UserMood>());

            var viewModel = new MoodSearchViewModel
            {
                SearchMood = searchMood,
                SearchDate = searchDate,
                MoodSelectList = moodSelectList,
                Entries = await entries.ToListAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var userMoodEntry = await _context.UserMoodEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userMoodEntry == null)
            {
                throw new EntityNotFoundException($"UserMoodEntry with ID {id} not found.");
            }

            return View(userMoodEntry);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Time,Mood,Notes")] UserMoodEntry userMoodEntry)
        {
            if (userMoodEntry == null)
            {
                throw new ArgumentNullException(nameof(userMoodEntry));
            }

            if (ModelState.IsValid)
            {
                _context.Add(userMoodEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userMoodEntry);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var userMoodEntry = await _context.UserMoodEntry.FindAsync(id);
            if (userMoodEntry == null)
            {
                throw new EntityNotFoundException($"UserMoodEntry with ID {id} not found.");
            }
            return View(userMoodEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Time,Mood,Notes")] UserMoodEntry userMoodEntry)
        {
            if (userMoodEntry == null)
            {
                throw new ArgumentNullException(nameof(userMoodEntry));
            }

            if (id != userMoodEntry.Id)
            {
                throw new ArgumentException("Mismatched UserMoodEntry ID.", nameof(id));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userMoodEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMoodEntryExists(userMoodEntry.Id))
                    {
                        throw new EntityNotFoundException($"UserMoodEntry with ID {userMoodEntry.Id} not found.");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userMoodEntry);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var userMoodEntry = await _context.UserMoodEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userMoodEntry == null)
            {
                throw new EntityNotFoundException($"UserMoodEntry with ID {id} not found.");
            }

            return View(userMoodEntry);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userMoodEntry = await _context.UserMoodEntry.FindAsync(id);
            if (userMoodEntry != null)
            {
                _context.UserMoodEntry.Remove(userMoodEntry);
            }
            else
            {
                throw new EntityNotFoundException($"UserMoodEntry with ID {id} not found.");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserMoodEntryExists(int id)
        {
            return _context.UserMoodEntry.Any(e => e.Id == id);
        }
    }
}
