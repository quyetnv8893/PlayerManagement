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
    public class CareersController : Controller
    {
        //private PlayerManagementContext db = new PlayerManagementContext();
        private ICareerRepository _repository;
        private IPlayerRepository _playerRepository = new PlayerRepository();

        public CareersController(ICareerRepository repository)
        {
            _repository = repository;
        }


        public CareersController()
            : this(new CareerRepository())
        {
        }

        // GET: Careers
        public ActionResult Index(String id)
        {
            var careers = _repository.GetCareersByPlayerID(id);
            Career career;
            if (careers.Count() == 0)
            {
                career = new Career();
                career.PlayerID = id;
                List<Career> tmpCareers = careers.ToList();
                tmpCareers.Add(career);
                return View(tmpCareers);
                
            }
            foreach (var c in careers)
            {                
                c.Player = _playerRepository.GetPlayerByID(c.PlayerID);
                
            }
            return View(careers.ToList());
        }

        // GET: Careers/Create
        [Authorize]
        public ActionResult Create(String id)
        {
            Career career = new Career();
            career.PlayerID = id;
            //ViewBag.PlayerID = id;
            return View(career);
        }
      
        // POST: Careers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,From,To,NumberOfGoals,ClubName,PlayerID")] Career career)
        {
            if (ModelState.IsValid)
            {
                career.ID = ((int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds).ToString();

                _repository.InsertCareer(career);
                return RedirectToAction("Index", "Careers", new { id = career.PlayerID});
            }

            ViewBag.PlayerID = new SelectList(_playerRepository.GetPlayers(), "ID", "Name");
            return View(career);
        }

        // GET: Careers/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Career career = _repository.GetCareerByID(id);
            if (career == null)
            {
                return HttpNotFound();
            }
            //ViewBag.PlayerID = new SelectList(_repository.GetCareers(), "ID", "ClubName", career.PlayerID);
            return View(career);
        }

        // POST: Careers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,From,To,NumberOfGoals,ClubName,PlayerID")] Career career)
        {
            if (ModelState.IsValid)
            {
                _repository.EditCareer(career);
                return RedirectToAction("Index", "Careers", new { id = career.PlayerID });
            }

            return View(career);
        }

        // GET: Careers/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Career career = _repository.GetCareerByID(id);
            if (career == null)
            {
                return HttpNotFound();
            }
            return View(career);
        }

        // POST: Careers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(string id)
        {
            Career career = _repository.GetCareerByID(id);
            var playerID = career.PlayerID;
            _repository.DeleteCareer(id);

            return RedirectToAction("Index", "Careers", new { id = playerID });
        }

    }
}
