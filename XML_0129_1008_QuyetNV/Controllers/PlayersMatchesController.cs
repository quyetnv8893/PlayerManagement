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
        public PlayersMatchesController()
            : this(new PlayerMatchRepository())
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


        public ActionResult Details(String playerID, String matchID)
        {
            PlayerMatch playermatch = _repository.GetPlayerMatchByPlayerIdAndMatchId(playerID, matchID);
            if (playermatch == null)
                return RedirectToAction("Index");
            return View(playermatch);
        }

        [Authorize]
        public ActionResult Create(String id)
        {           
            ViewData["MatchID"] =
                new SelectList((from l in _matchRepository.GetMatches().ToList()
                                select new
                                {
                                    ID = l.ID,
                                    DisplayName = l.Name + " ( " + l.Time + " ) "
                                }),
                    "ID",
                    "DisplayName",
                    null);

            //ViewBag.PlayerID = new SelectList(_playerRepository.GetPlayers(), "ID", "Name");
            PlayerMatch playerMatch = new PlayerMatch();
            playerMatch.PlayerID = id;
            return View(playerMatch);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(PlayerMatch playermatch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.InsertPlayerMatch(playermatch);
                    return RedirectToAction("ViewPlayerMatches", new { id = playermatch.PlayerID });
                }
                catch (Exception ex)
                {
                    //error msg for failed insert in XML file
                    ModelState.AddModelError("", "Error creating record. " + ex.Message);
                }
            }

            return View(playermatch);
        }
        [Authorize]
        public ActionResult Edit(String playerID, String matchID)
        {
            PlayerMatch match = _repository.GetPlayerMatchByPlayerIdAndMatchId(playerID, matchID);
            if (match == null)
                return RedirectToAction("Index");
            return View(match);
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
        public ActionResult Delete(String playerID, String matchID)
        {
            PlayerMatch match = _repository.GetPlayerMatchByPlayerIdAndMatchId(playerID, matchID);
            if (match == null)
                return RedirectToAction("Index");
            return View(match);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(PlayerMatch playermatch)
        {
            try
            {
                _repository.DeletePlayerMatch(playermatch);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
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
                playerMatch.Match = _matchRepository.GetMatchByID(playerMatch.MatchID);
                playerMatch.Player = _playerRepository.GetPlayerByID(playerMatch.PlayerID);
            }
            return View(playerMatches);
        }
    }
}