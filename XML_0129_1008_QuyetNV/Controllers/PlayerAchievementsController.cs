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
    public class PlayerAchievementsController : Controller
    {
        private PlayerManagementContext db = new PlayerManagementContext();
        private IPlayerAchievementRepository _repository;

        public PlayerAchievementsController()
            : this(new PlayerAchievementRepository())
        {
        }

        public PlayerAchievementsController(IPlayerAchievementRepository repository)
        {
            _repository = repository;
        }

        // GET: PlayerAchievements
        public ActionResult Index(String id)
        {
            return View(_repository.GetPlayerAchievementsByPlayerID(id));
        }

        // GET: PlayerAchievements/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerAchievement playerAchievement = db.PlayerAchievements.Find(id);
            if (playerAchievement == null)
            {
                return HttpNotFound();
            }
            return View(playerAchievement);
        }

        // GET: PlayerAchievements/Create
        public ActionResult Create()
        {
            ViewBag.playerId = new SelectList(db.Players, "id", "name");
            ViewBag.achievementName = new SelectList(db.Achievements, "name", "name");
            return View();
        }

        // POST: PlayerAchievements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "playerId,achievementName,number")] PlayerAchievement playerAchievement)
        {
            if (ModelState.IsValid)
            {
                db.PlayerAchievements.Add(playerAchievement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.achievementName = new SelectList(db.Achievements, "name", "imageLink", playerAchievement.achievementName);
            return View(playerAchievement);
        }

        // GET: PlayerAchievements/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerAchievement playerAchievement = db.PlayerAchievements.Find(id);
            if (playerAchievement == null)
            {
                return HttpNotFound();
            }
            ViewBag.achievementName = new SelectList(db.Achievements, "name", "imageLink", playerAchievement.achievementName);
            return View(playerAchievement);
        }

        // POST: PlayerAchievements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "playerId,achievementName,number")] PlayerAchievement playerAchievement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playerAchievement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.achievementName = new SelectList(db.Achievements, "name", "imageLink", playerAchievement.achievementName);
            return View(playerAchievement);
        }

        // GET: PlayerAchievements/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerAchievement playerAchievement = db.PlayerAchievements.Find(id);
            if (playerAchievement == null)
            {
                return HttpNotFound();
            }
            return View(playerAchievement);
        }

        // POST: PlayerAchievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PlayerAchievement playerAchievement = db.PlayerAchievements.Find(id);
            db.PlayerAchievements.Remove(playerAchievement);
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
