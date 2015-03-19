using PlayerManagement.Models;
using PlayerManagement.Models.PlayerMatch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlayerManagement.Controllers
{
    public class LeagueController : Controller
    {
        private ILeagueRepository _repository;
        private IMatchRepository _matchRepository;
        private IPlayerMatchRepository _playerMatchRepository;
        public LeagueController()
            : this(new LeagueRepository())
        {
        }

        public LeagueController(ILeagueRepository _repository)
        {
            this._repository = _repository;
            this._matchRepository = new MatchRepository();
            this._playerMatchRepository = new PlayerMatchRepository();
        }


        public ActionResult Index()
        {
            return View(_repository.GetLeagues());
        }



        public ActionResult Details(String id)
        {
            String name = id;
            League league = _repository.GetLeagueByName(name);
            if (league == null)
            {
                return RedirectToAction("Index");
            }
            return View(league);
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(String name, HttpPostedFileBase file)
        {
            League league = null;
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            String path = Path.Combine(Server.MapPath("~/images"), fileName);
                            file.SaveAs(path);

                            String pathInXML = "/images/" + file.FileName;

                            league = new League(name, pathInXML);

                            _repository.InsertLeague(league);
                            //db.Entry(coach).State = EntityState.Modified;
                            //db.SaveChanges();
                            return RedirectToAction("Index");
                        }

                    }                   
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Error insert new league: " + e.Message);
                }
            }
            return View(league);
        }
        [Authorize]
        public ActionResult Edit(String id)
        {
            String name = id;
            League league = _repository.GetLeagueByName(name);
            if (league == null)
            {
                return RedirectToAction("Index");
            }
            return View(league);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(String name, HttpPostedFileBase file)
        {
            League league = null;
            if (ModelState.IsValid)
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        String path = Path.Combine(Server.MapPath("~/images"), fileName);
                        file.SaveAs(path);

                        String pathInXML = "/images/" + file.FileName;

                        league = new League(name, pathInXML);

                        _repository.EditLeague(league);                        
                        return RedirectToAction("Index");
                    }
                    
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Error insert new league: " + e.Message);
                }
            }
            return View(league);
        }
        [Authorize]
        public ActionResult Delete(String id)
        {
            String name = id;
            League league = _repository.GetLeagueByName(name);
            if (league == null)
            {
                return RedirectToAction("Index");
            }
            return View(league);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(String id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //todo delete all matches in league and all player matches related to match                    
                    List<Match> matches = _matchRepository.GetMatchesByLeagueName(id).ToList();
                    foreach (var match in matches)
                    {
                        List<PlayerMatch> playerMatches = _playerMatchRepository.GetPlayerMatchesByMatchId(match.ID).ToList();
                        foreach (var playerMatch in playerMatches)
                        {
                            _playerMatchRepository.DeletePlayerMatch(playerMatch);
                        }
                        _matchRepository.DeleteMatch(match.ID);
                    }
                    _repository.DeleteLeague(id);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMsg = "Error deleting record. " + e.Message;
                    return View(_repository.GetLeagueByName(id));
                }
            }
            return View(_repository.GetLeagues());
        }

        public ActionResult GoToMatches(String id)
        {
            return RedirectToAction("ViewByLeagueName", "Match", new { id = id });
        }
    }
}