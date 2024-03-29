﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieDemo.Data;
using MovieDemo.Models.Entities;
using MovieDemo.Models.ViewModels;
using MovieDemo.Services;

namespace MovieDemo.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieDemoContext _context;
        private readonly IGenreSelectListService genreSelectListService;

        public MoviesController(MovieDemoContext context, IGenreSelectListService genreSelectListService)
        {
            _context = context;
            this.genreSelectListService = genreSelectListService;
        }

        public async Task<IActionResult> Filter(string title, int? genre)
        {
         
            var model = string.IsNullOrWhiteSpace(title) ? 
                                    _context.Movie : 
                                    _context.Movie.Where(m => m.Title.StartsWith(title));

            model = genre is null ?
                                model :
                                model.Where(m => (int)m.Genre == genre);

            return View(nameof(Index), await model.ToListAsync());
        }  
        
        public async Task<IActionResult> Filter2(IndexViewModel viewModel)
        {
         
            var movies = string.IsNullOrWhiteSpace(viewModel.Title) ? 
                                    _context.Movie : 
                                    _context.Movie.Where(m => m.Title.StartsWith(viewModel.Title));

            movies = viewModel.Genre is null ?
                                movies :
                                movies.Where(m => m.Genre == viewModel.Genre);

            var model = new IndexViewModel
            {
                Movies = await movies.ToListAsync(),
                Genres = await genreSelectListService.GetGenresAsync() //GetGenresAsync()
            };

            return View(nameof(Index2), model);
        } 
        
        public async Task<IActionResult> Filter3(IndexViewModel2 viewModel)
        {
         
            var movies = string.IsNullOrWhiteSpace(viewModel.Title) ? 
                                    _context.Movie : 
                                    _context.Movie.Where(m => m.Title.StartsWith(viewModel.Title));

            movies = viewModel.Genre is null ?
                                movies :
                                movies.Where(m => m.Genre == viewModel.Genre);

            var model = new IndexViewModel2
            {
                Movies = await movies.ToListAsync()
            };

            return View(nameof(Index3), model);
        }

        private async Task<IEnumerable<SelectListItem>> GetGenresAsync()
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

        // GET: Movies
        public async Task<IActionResult> Index2()
        {
            var movies = await _context.Movie.ToListAsync();

            var model = new IndexViewModel
            {
                Movies = movies,
                Genres = movies.Select(m => m.Genre)
                               .Distinct()
                               .Select(g => new SelectListItem
                               {
                                   Text = g.ToString(),
                                   Value = g.ToString()
                               })
                               .ToList()
            };

            return View(model);
                         
        }   
        
        public async Task<IActionResult> Index3()
        {
            var movies = await _context.Movie.ToListAsync();

            var model = new IndexViewModel2
            {
                Movies = movies,
            };

            return View(model);
                         
        } 
        
        public async Task<IActionResult> Index()
        {
            var model = await _context.Movie.ToListAsync();
            return View(model);
                         
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReleaseDate,Genre,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MovieDemoContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
