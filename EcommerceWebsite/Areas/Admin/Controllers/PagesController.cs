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
            if (page is null)
                return View("PageForm");
            if (!ModelState.IsValid)
                return View("PageForm", page);

            _context.Pages.Add(page);
            _context.SaveChanges();
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
                return View("PageForm");
            var PageInDb = _context.Pages.SingleOrDefault(p => p.Id == id);
            if (PageInDb is null)
                return HttpNotFound();
            PageInDb.Title = page.Title;
            PageInDb.Body = page.Body;
            PageInDb.Slug = page.Slug;
            PageInDb.Sorting = page.Sorting;
            PageInDb.HasSideBar = page.HasSideBar;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult Delete(int id)
        {
            var pageInDb = _context.Pages.SingleOrDefault(p => p.Id == id);
            if (pageInDb is null)
                return HttpNotFound();
            _context.Pages.Remove(pageInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}