using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieDemo.Services
{
    public interface IGenreSelectListService
    {
        Task<IEnumerable<SelectListItem>> GetGenresAsync();
    }
}