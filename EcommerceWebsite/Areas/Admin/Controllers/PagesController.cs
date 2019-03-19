using EcommerceWebsite.Models;
using EcommerceWebsite.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceWebsite.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        #region DB Connection Object
        private ApplicationDbContext _context;
        public PagesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        #region R => Read Operations
        public ActionResult Index()
        {
            var pages = _context.Pages.ToList().OrderBy(p => p.Sorting);

            return View(pages);
        }

        public ActionResult Details(int id)
        {
            var page = _context.Pages.SingleOrDefault(p => p.Id == id);
            if (page is null)
                return HttpNotFound();
            return View(page);
        }
        #endregion

        #region C => Create Operation
        public ActionResult Create()
        {
            return View("PageForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Page page)
        {
            if (!ModelState.IsValid)
                return View("PageForm", page);

            if ( _context.Pages.Any(p => p.Title == page.Title || p.Slug == page.Slug))
            {
                ModelState.AddModelError("", "The title or slug are already exists.");
                return View("PageForm", page);
            }

            page.Sorting = 100;
            _context.Pages.Add(page);
            _context.SaveChanges();

            TempData["SuccessAddedMessage"] = page.Title + " had been added successfully.";
            return RedirectToAction("Index");
        }
        #endregion

        #region U => Update Operation (Edit)
        public ActionResult Edit(int id)
        {
            var page = _context.Pages.SingleOrDefault(p => p.Id == id);
            if (page is null)
                return HttpNotFound();

            return View("PageForm", page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Page page)
        {
            if (!ModelState.IsValid)
                return View("PageForm", page);
            if (_context.Pages.Where(x => x.Id != id).Any(p => p.Title == page.Title || p.Slug == page.Slug))
            {
                ModelState.AddModelError("", "The title or slug are already exists Or No thing has been changed.");
                return View("PageForm", page);
            }
            
            var PageInDb = _context.Pages.SingleOrDefault(p => p.Id == id);
            PageInDb.Title = page.Title;
            PageInDb.Body = page.Body;
            PageInDb.Slug = page.Slug;
            PageInDb.Sorting = page.Sorting;
            PageInDb.HasSideBar = page.HasSideBar;
            _context.SaveChanges();

            TempData["SuccessEditMessage"] = "Edit done successfully";

            return RedirectToAction("Index");
        }
        #endregion

        #region D => Delete Operation
        public ActionResult Delete(int id)
        {
            var pageInDb = _context.Pages.SingleOrDefault(p => p.Id == id);
            if (pageInDb is null)
                return HttpNotFound();
            _context.Pages.Remove(pageInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}