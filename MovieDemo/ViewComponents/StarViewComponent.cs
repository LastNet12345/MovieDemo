using Microsoft.AspNetCore.Mvc;
using MovieDemo.Data;
using MovieDemo.Models.ViewModels;

namespace MovieDemo.ViewComponents
{
    public class StarViewComponent : ViewComponent
    {
        //private readonly MovieDemoContext movieDemoContext;

        //public StarViewComponent(MovieDemoContext movieDemoContext)
        //{
        //    this.movieDemoContext = movieDemoContext;
        //}


        public IViewComponentResult Invoke(float rating)
        {
            var doubleRating = (int)Math.Round(rating * 2);

            var model = new StarViewModel
            {
                Stars = doubleRating / 2,
                IsHalfStar = doubleRating % 2 == 1
            };

            return View(model);
        }
    }
}
