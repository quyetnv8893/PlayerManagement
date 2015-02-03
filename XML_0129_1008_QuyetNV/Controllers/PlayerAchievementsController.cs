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
        private IAchievementRepository _achievementRepository = new AchievementRepository();
        private IPlayerRepository _playerRepository = new PlayerRepository();


        public PlayerAchievementsController(IPlayerAchievementRepository repository)
        {
            _repository = repository;
        }
        
     
        public PlayerAchievementsController()
            : this(new PlayerAchievementRepository())
        {
        }


        // View player's career by ID
        public ActionResult Index(String id)
        {            
            var playerAchievements = _repository.GetPlayerAchievementsByPlayerID(id)
                .Where(achievement => achievement.PlayerID.Equals(id));
            foreach (var achievement in playerAchievements)
            {
                achievement.Achievement = _achievementRepository.GetAchievementByName(achievement.AchievementName);
            }
            return View(playerAchievements.ToList());
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

            ViewBag.AchievementName = new SelectList(_achievementRepository.GetAchievements(), "Name", "Name");
            ViewBag.PlayerID = new SelectList(_playerRepository.GetPlayers(), "ID", "Name");
            return View();
        }

        // POST: PlayerAchievements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerID,AchievementName,Number")] PlayerAchievement playerAchievement)
        {
            if (ModelState.IsValid)
            {
                String a = playerAchievement.PlayerID;
                String b = playerAchievement.AchievementName;
                int c = playerAchievement.Number;
                _repository.InsertPlayerAchievement(playerAchievement);              
                return RedirectToAction("Index");
            }

            ViewBag.AchievementName = new SelectList(_achievementRepository.GetAchievements(), "Name", "Name", playerAchievement.AchievementName);
            ViewBag.PlayerID = new SelectList(_playerRepository.GetPlayers(), "ID", "Name", playerAchievement.PlayerID);
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
            ViewBag.AchievementName = new SelectList(db.Achievements, "Name", "ImageLink", playerAchievement.AchievementName);
            ViewBag.PlayerID = new SelectList(db.Players, "ID", "ClubName", playerAchievement.PlayerID);
            return View(playerAchievement);
        }

        // POST: PlayerAchievements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerID,AchievementName,Number")] PlayerAchievement playerAchievement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playerAchievement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AchievementName = new SelectList(db.Achievements, "Name", "ImageLink", playerAchievement.AchievementName);
            ViewBag.PlayerID = new SelectList(db.Players, "ID", "ClubName", playerAchievement.PlayerID);
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
