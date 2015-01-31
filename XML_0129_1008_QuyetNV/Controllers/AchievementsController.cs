using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlayerManagement.Models;

namespace AchievementManagement.Controllers
{
    public class AchievementsController : Controller
    {
        private IAchievementRepository _repository;

        public AchievementsController()
            : this(new AchievementRepository())
        {
        }

        public AchievementsController(IAchievementRepository repository)
        {
            _repository = repository;
        }


        // GET: Achievements
        public ActionResult Index()
        {
            return View(_repository.GetAchievements());
        }

        // GET: Achievements/Details/5
        public ActionResult Details(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = _repository.GetAchievementByName(name);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // GET: Achievements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Achievements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,clubName,number,name,position,dateOfBirth,placeOfBirth,weight,height,description,imageLink,status")] Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                _repository.InsertAchievement(achievement);
                return RedirectToAction("Index");
            }

            return View(achievement);
        }

        // GET: Achievements/Edit/5
        public ActionResult Edit(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = _repository.GetAchievementByName(name);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // POST: Achievements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name, imageLink")] Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                _repository.EditAchievement(achievement);
                //db.Entry(achievement).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(achievement);
        }

        // GET: Achievements/Delete/5
        public ActionResult Delete(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = _repository.GetAchievementByName(name);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // POST: Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            _repository.DeleteAchievement(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
