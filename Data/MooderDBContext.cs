using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mooder.Models;

namespace Mooder.Data
{
    public class MooderDBContext : DbContext
    {
        public MooderDBContext(DbContextOptions<MooderDBContext> options)
            : base(options)
        {
        }

        public DbSet<Mooder.Models.UserMoodEntry> UserMoodEntry { get; set; } = default!;
    }
}
