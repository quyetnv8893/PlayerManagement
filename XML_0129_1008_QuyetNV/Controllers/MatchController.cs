using PlayerManagement.Models;
using PlayerManagement.Models.PlayerMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlayerManagement.Controllers
{
    public class MatchController : Controller
    {
        private IMatchRepository _repository;
        private IPlayerRepository _playerRepository = new PlayerRepository();
        private IPlayerMatchRepository _playerMatchRepository = new PlayerMatchRepository();
        private ILeagueRepository _leagueRepository = new LeagueRepository();
        public MatchController()
            : this(new MatchRepository())
        {
        }

        public MatchController(IMatchRepository repository)
        {
            _repository = repository;
        }


        public ActionResult Index()
        {
            return View(_repository.GetMatches());
        }

        public ActionResult ViewByLeagueName(String id)
        {
            return View(_repository.GetMatchesByLeagueName(id));
        }


        public ActionResult Details(String id)
        {
            Match match = _repository.GetMatchByID(id);
            if (match == null)
                return RedirectToAction("Index");

            IEnumerable<PlayerMatch> temp = _playerMatchRepository.GetPlayerMatchesByMatchId(id);

            foreach (var item in temp)
            {
                item.Player = _playerRepository.GetPlayerByID(item.PlayerID);
            }

            if (temp != null && match != null)
            {
                match.PlayerMatches = temp;
            } 
            return View(match);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.LeagueName = new SelectList(_leagueRepository.GetLeagues(), "Name", "Name");

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Match match)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.InsertMatch(match);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed insert in XML file
                    ModelState.AddModelError("", "Error creating record. " + ex.Message);
                }
            }

            return View(match);
        }
        [Authorize]
        public ActionResult Edit(String id)
        {
            Match match = _repository.GetMatchByID(id);
            ViewBag.LeagueName = new SelectList(_leagueRepository.GetLeagues(), "Name", "Name");
            if (match == null)
                return RedirectToAction("Index");
            return View(match);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Match match)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.EditMatch(match);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed edit in XML file
                    ModelState.AddModelError("", "Error editing record. " + ex.Message);
                }
            }

            return View(match);
        }
        [Authorize]
        public ActionResult Delete(String id)
        {
            Match match = _repository.GetMatchByID(id);
            ViewBag.LeagueName = new SelectList(_leagueRepository.GetLeagues(), "Name", "Name");
            if (match == null)
                return RedirectToAction("Index");
            return View(match);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(Match match)
        {
            try
            {
                _repository.DeleteMatch(match.ID);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //error msg for failed delete in XML file
                ViewBag.ErrorMsg = "Error deleting record. " + ex.Message;
                return View(_repository.GetMatchByID(match.ID));
            }
        }

        public ActionResult GoToLeagueDetails(String id)
        {
            return RedirectToAction("Details", "League", new { id = id });
        }

        public ActionResult GoToPlayerDetails(String id)
        {
            return RedirectToAction("Details", "Players", new { id = id });
        }
    }
}