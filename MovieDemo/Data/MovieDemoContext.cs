using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieDemo.Models.Entities;

namespace MovieDemo.Data
{
    public class MovieDemoContext : DbContext
    {
        public MovieDemoContext (DbContextOptions<MovieDemoContext> options)
            : base(options)
        {
        }

        public DbSet<MovieDemo.Models.Entities.Movie> Movie { get; set; } = default!;
    }
}
