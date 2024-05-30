using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mooder.Data;
using Mooder.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Mooder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MooderDBContext _context;

        public HomeController(ILogger<HomeController> logger, MooderDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var entries = _context.UserMoodEntry.ToList();
            var currentDate = DateTime.Now;
            var previousMonth = currentDate.AddMonths(-1).Month;
            var previousMonthYear = currentDate.AddMonths(-1).Year;
            var previousMonthEntries = entries.Where(e => e.Date.Month == previousMonth && e.Date.Year == previousMonthYear).ToList();
            var moodCounts = previousMonthEntries.GroupBy(e => e.Mood)
                                                 .Select(g => new
                                                 {
                                                     Mood = g.Key.ToString(),
                                                     Count = g.Count()
                                                 }).ToList();

            var groupedByMonth = entries.GroupBy(e => new { e.Date.Year, e.Date.Month })
                                        .Select(g => new
                                        {
                                            Date = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
                                            EntryCount = g.Count()
                                        }).ToList();

            var viewModel = new MoodFrequencyViewModel
            {
                Moods = moodCounts.Select(g => g.Mood).ToList(),
                MoodCounts = moodCounts.Select(g => g.Count).ToList(),
                Months = groupedByMonth.Select(g => g.Date).ToList(),
                MonthlyEntryCounts = groupedByMonth.Select(g => g.EntryCount).ToList()
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
