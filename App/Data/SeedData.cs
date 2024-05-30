// Data/ApplicationDbContextSeed.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mooder.Models;
using Mooder.Factories;

namespace Mooder.Data
{
  public static class ApplicationDbContextSeed
  {
    public static async Task SeedAsync(MooderDBContext context)
    {
      context.Database.EnsureCreated();

      // Check if the database is already seeded
      if (context.UserMoodEntry.Any())
      {
        return;   // DB has been seeded
      }

      // Generate and add seed data using the factory
      var seedData = UserMoodEntryFactory.GenerateEntries(30);
      context.UserMoodEntry.AddRange(seedData);

      await context.SaveChangesAsync();
    }
  }
}
