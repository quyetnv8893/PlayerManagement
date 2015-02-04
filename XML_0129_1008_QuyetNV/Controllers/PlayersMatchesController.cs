using PlayerManagement.Models;
using PlayerManagement.Models.PlayerMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlayerManagement.Controllers
{
    public class PlayersMatchesController : Controller
    {
        private IPlayerMatchRepository _repository;
        private IMatchRepository _matchRepository = new MatchRepository();
        private IPlayerRepository _playerRepository = new PlayerRepository();
        public PlayersMatchesController(): this(new PlayerMatchRepository())
        {
        }

        public PlayersMatchesController(IPlayerMatchRepository repository)
        {
            _repository = repository;
        }


        public ActionResult Index()
        {
            return View(_repository.GetPlayerMatches());
        }


        public ActionResult Details(String id1,String id2)
        {
            PlayerMatch playermatch = _repository.GetPlayerMatchByPlayerIdAndMatchId(id1,id2);
            if (playermatch == null)
                return RedirectToAction("Index");
            return View(playermatch);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PlayerMatch playermatch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.InsertPlayerMatch(playermatch);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    //error msg for failed insert in XML file
                    ModelState.AddModelError("", "Error creating record. " + ex.Message);
                }
            }

            return View(playermatch);
        }

        public ActionResult Edit(String id1, String id2)
        {
            PlayerMatch match = _repository.GetPlayerMatchByPlayerIdAndMatchId(id1, id2);
            if (match == null)
                return RedirectToAction("Index");
            return View(match);
        }
       
        [HttpPost]
        public ActionResult Edit(PlayerMatch playermatch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.EditPlayerMatch(playermatch);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed edit in XML file
                    ModelState.AddModelError("", "Error editing record. " + ex.Message);
                }
            }

            return View(playermatch);
            }

        public ActionResult Delete(String id1, String id2)
        {
            PlayerMatch match = _repository.GetPlayerMatchByPlayerIdAndMatchId(id1, id2);
            if (match == null)
                return RedirectToAction("Index");
            return View(match);
        }       

        [HttpPost]
        public ActionResult Delete(PlayerMatch playermatch)
        {
            try
            {
                _repository.DeletePlayerMatch(playermatch);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                //error msg for failed delete in XML file
                ViewBag.ErrorMsg = "Error deleting record. " + ex.Message;
                return View(playermatch);
            }
        }

        public ActionResult ViewPlayerMatches(String id)
        {
            IEnumerable<PlayerMatch> playerMatches = _repository.GetPlayerMatchesByPlayerId(id);
            foreach (var playerMatch in playerMatches)
            {
                playerMatch.Match = _matchRepository.GetMatchByID(playerMatch.MatchId);
                playerMatch.Player = _playerRepository.GetPlayerByID(playerMatch.PlayerId);
            }
            return View(playerMatches);
        }
    }
}