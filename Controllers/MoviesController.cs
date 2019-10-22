using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAssignment.Data;

namespace NetCoreAssignment.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Movies
        public ActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(x => x.Casts).FirstOrDefault(x => x.Id == id);
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Movie movie)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _context.Movies.Add(movie);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(movie);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(movie);
            }
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            var movie = _context.Movies.Include(x => x.Casts).FirstOrDefault(x => x.Id == id);
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [FromForm] Movie movie)
        {
            try
            {
                _context.Remove(movie);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(movie);
            }
        }
    }
}