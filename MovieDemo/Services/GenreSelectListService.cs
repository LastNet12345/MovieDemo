using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieDemo.Data;

namespace MovieDemo.Services
{
    public class GenreSelectListService : IGenreSelectListService
    {
        private readonly MovieDemoContext _context;

        public GenreSelectListService(MovieDemoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetGenresAsync()
        {
            return await _context.Movie.Select(m => m.Genre)
                                .Distinct()
                                .Select(g => new SelectListItem
                                {
                                    Text = g.ToString(),
                                    Value = g.ToString()
                                })
                                .ToListAsync();
        }
    }
}
