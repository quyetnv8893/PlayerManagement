using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlayerManagement.Models;
using System.IO;
using PlayerManagement.Models.PlayerMatch;

namespace PlayerManagement.Controllers
{
    public class PlayersController : Controller
    {
        private static List<String> positions = new List<string>(new String[] {
            "GK", "SW", "RB", "CB", "LB", "LWB", "DM", "RWB", "CM", "AM", "LW", "RW", "WF", "CF"
        });
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IPlayerRepository _repository;

        public PlayersController()
            : this(new PlayerRepository())
        {
        }

        public PlayersController(IPlayerRepository repository)
        {
            _repository = repository;
        }


        // GET: Players
        public ActionResult Index()
        {
            return View(_repository.GetPlayers());
        }

        // GET: Players/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = _repository.GetPlayerByID(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        [Authorize]
        public ActionResult Create()
        {
            Player player = new Player();
            player.ID = ((int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            player.ClubName = "Real Madrid";

            return View(player);
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,clubName,number,name,position,dateOfBirth,placeOfBirth,weight,height,description,imageLink,status")] Player player)
        public ActionResult Create(String id, String clubName, int number, String name, String position, DateTime dateOfBirth,
            String placeOfBirth, Double weight, Double height, String description, HttpPostedFileBase file, Boolean status)
        {
            Player player = null;
            if (ModelState.IsValid)
            {

                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    String path = Path.Combine(Server.MapPath("~/images"), fileName);
                    file.SaveAs(path);

                    String pathInXML = "/images/" + file.FileName;

                    player = new Player(clubName, id, number, name, position, dateOfBirth, placeOfBirth,
 weight, height, description, pathInXML, status);

                    _repository.InsertPlayer(player);
                    return RedirectToAction("Index");
                }

            }

            return View(player);
        }

        // GET: Players/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = _repository.GetPlayerByID(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(String id, String clubName, int number, String name, String position, DateTime dateOfBirth,
            String placeOfBirth, Double weight, Double height, String description, HttpPostedFileBase file, Boolean status)
        {
            Player player = null;
            if (ModelState.IsValid)
            {
                if (file == null)
                {                    
                    player = new Player(clubName, id, number, name, position, dateOfBirth, placeOfBirth,
    weight, height, description, status);

                    _repository.EditPlayer(player);
                    return RedirectToAction("Index");
                }
                else
                {
                    var fileName = Path.GetFileName(file.FileName);
                    String path = Path.Combine(Server.MapPath("~/images"), fileName);
                    file.SaveAs(path);

                    String pathInXML = "/images/" + file.FileName;

                    player = new Player(clubName, id, number, name, position, dateOfBirth, placeOfBirth,
    weight, height, description,pathInXML, status);

                    _repository.EditPlayer(player);
                    return RedirectToAction("Index");
                }
                

            }
            return View(player);
        }

        // GET: Players/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = _repository.GetPlayerByID(id);

            PlayerMatchRepository _playerMatchRepository = new PlayerMatchRepository();
            List<PlayerMatch> playerMatches = _playerMatchRepository.GetPlayerMatchesByPlayerId(player.ID).ToList();

            if (player == null)
            {
                return HttpNotFound();
            }

            if (playerMatches == null)
            {
                return View(player);
            }          
            foreach (var playerMatch in playerMatches)
            {
                if (playerMatch.NumberOfGoals == 0 && playerMatch.NumberOfReds == 0 && playerMatch.NumberOfYellows == 0)
                {
                    continue;
                }
                else
                {
                    ViewBag.ErrorMessage = "Deleting this player affected the result of some matches. You cannot delete this player";
                    return View("Error");
                }
            }

            return View(player);
                                   
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(string id)
        {
            _repository.DeletePlayer(id);
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
