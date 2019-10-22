using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_Assignment.Data;

namespace ASP_Assignment.Controllers
{
    public class CastsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CastsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Casts
        public ActionResult Index()
        {
            var casts = _context.Casts.Include(x => x.Movie).ToList();
            return View(casts);
        }

        // GET: Casts/Details/5
        public ActionResult Details(int id)
        {
            var cast = _context.Casts.Include(x => x.Movie).FirstOrDefault(x => x.Id == id);
            return View(cast);
        }

        // GET: Casts/Create
        public ActionResult Create()
        {
            ViewBag.Movies = _context.Movies.ToList();
            return View();
        }

        // POST: Casts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Cast cast)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Casts.Add(cast);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Movies = _context.Movies.ToList();
                return View();
            }
        }

        // GET: Casts/Edit/5
        public ActionResult Edit(int id)
        {
            var cast = _context.Casts.FirstOrDefault(x => x.Id == id);
            ViewBag.Movies = _context.Movies.ToList();
            return View(cast);
        }

        // POST: Casts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] Cast cast)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(cast);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Movies = _context.Movies.ToList();
                return View(cast);
            }
        }

        // GET: Casts/Delete/5
        public ActionResult Delete(int id)
        {
            var cast = _context.Casts.Include(x=>x.Movie).FirstOrDefault(x => x.Id == id);
            return View(cast);
        }

        // POST: Casts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [FromForm] Cast cast)
        {
            try
            {
                _context.Remove(cast);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(cast);
            }
        }
    }
}