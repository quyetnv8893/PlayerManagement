using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlayerManagement.Models;
using System.Collections;

namespace PlayerManagement.Controllers
{
    public class PlayerAchievementsController : Controller
    {
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
                achievement.Player = _playerRepository.GetPlayerByID(achievement.PlayerID);
            }
            return View(playerAchievements.ToList());
        }

        // GET: PlayerAchievements/Create
        [Authorize]
        public ActionResult Create(String id)
        {
            PlayerAchievement playerArchivement = new PlayerAchievement();
            playerArchivement.PlayerID = id;
            ViewBag.AchievementName = new SelectList(_achievementRepository.GetAchievements(), "Name", "Name");
          
            return View(playerArchivement);
        }

        // POST: PlayerAchievements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(String PlayerID, String AchievementName, int Number)
        {
            PlayerAchievement playerAchievement = null;
            if (ModelState.IsValid)
            {
                playerAchievement = new PlayerAchievement(Number, PlayerID, AchievementName);
                _repository.InsertPlayerAchievement(playerAchievement);
                return RedirectToAction("Index", "PlayerAchievements", new { id = playerAchievement.PlayerID });
            }
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();
            ViewBag.AchievementName = new SelectList(_achievementRepository.GetAchievements(), "Name", "Name", playerAchievement.AchievementName);
            return View(playerAchievement);
        }

        // GET: PlayerAchievements/Edit/5
        [Authorize]
        public ActionResult Edit(String playerID, String achievementName)
        {
            if (playerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerAchievement playerAchievement = _repository.GetPlayerAchievement(playerID, achievementName);
            if (playerAchievement == null)
            {
                return HttpNotFound();
            }
            ViewBag.AchievementName = new SelectList(_repository.GetPlayerAchievementsByPlayerID(playerID), "Name", "ImageLink", playerAchievement.AchievementName);
            return View(playerAchievement);
        }

        // POST: PlayerAchievements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "PlayerID,AchievementName,Number")] PlayerAchievement playerAchievement)
        {
            if (ModelState.IsValid)
            {
                _repository.EditPlayerAchievement(playerAchievement);
                return RedirectToAction("Index", "PlayerAchievements", new { id = playerAchievement.PlayerID });
            }
            ViewBag.AchievementName = new SelectList(_achievementRepository.GetAchievements(), "Name", "ImageLink", playerAchievement.AchievementName);       
            return View(playerAchievement);
        }

        // GET: PlayerAchievements/Delete/5
        [Authorize]
        public ActionResult Delete(String playerID, String achievementName)
        {
            if (playerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerAchievement playerAchievement = _repository.GetPlayerAchievement(playerID, achievementName);
            if (playerAchievement == null)
            {
                return HttpNotFound();
            }
            return View(playerAchievement);
        }

        // POST: PlayerAchievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(string playerID, string achievementName)
        {
            _repository.DeletePlayerAchievement(playerID, achievementName);
            return RedirectToAction("Index", "PlayerAchievements", new { id = playerID });
        }

    }
}
