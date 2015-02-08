using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlayerManagement.Models;

namespace PlayerManagement.Controllers
{
    public class AuthenticationsController : Controller
    {
        private PlayerManagementContext db = new PlayerManagementContext();

        // GET: Authentications
        public ActionResult Index()
        {
            return View(db.Authentications.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }

        // GET: Authentications/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authentication authentication = db.Authentications.Find(id);
            if (authentication == null)
            {
                return HttpNotFound();
            }
            return View(authentication);
        }

        // GET: Authentications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authentications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,MD5Password")] Authentication authentication)
        {
            if (ModelState.IsValid)
            {
                db.Authentications.Add(authentication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(authentication);
        }

        // GET: Authentications/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authentication authentication = db.Authentications.Find(id);
            if (authentication == null)
            {
                return HttpNotFound();
            }
            return View(authentication);
        }

        // POST: Authentications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,MD5Password")] Authentication authentication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authentication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(authentication);
        }

        // GET: Authentications/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authentication authentication = db.Authentications.Find(id);
            if (authentication == null)
            {
                return HttpNotFound();
            }
            return View(authentication);
        }

        // POST: Authentications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Authentication authentication = db.Authentications.Find(id);
            db.Authentications.Remove(authentication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
