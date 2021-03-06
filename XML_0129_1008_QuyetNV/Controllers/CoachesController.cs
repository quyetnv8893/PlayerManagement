﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlayerManagement.Models;
using System.IO;

namespace PlayerManagement.Controllers
{
    public class CoachesController : Controller
    {
        private ICoachRepository _repository;

        public CoachesController()
            : this(new CoachRepository())
        {
        }

        public CoachesController(ICoachRepository repository)
        {
            _repository = repository;
        }


        // GET: Coaches
        public ActionResult Index()
        {
            return View(_repository.GetCoaches());
        }

        // GET: Coaches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = _repository.GetCoachByName(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // GET: Coaches/Create
        [Authorize]
        public ActionResult Create()
        {
            Coach coach = new Coach();
            coach.ClubName = "Real Madrid";
            return View(coach);
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        //public ActionResult Create([Bind(Include = "name, imageLink, position, dateOfBirth, clubName")] Coach coach)
        public ActionResult Create(String name, HttpPostedFileBase file, String position, DateTime dateOfBirth, String clubName)
        {
            Coach coach = null;
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    String path = Path.Combine(Server.MapPath("~/images"), fileName);
                    file.SaveAs(path);

                    String pathInXML = "/images/" + file.FileName;

                    coach = new Coach(name, pathInXML, position, dateOfBirth, clubName);

                    _repository.InsertCoach(coach);
                    return RedirectToAction("Index");
                }
                
            }

            return View(coach);
        }

        // GET: Coaches/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = _repository.GetCoachByName(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(String name, HttpPostedFileBase file, String position, DateTime dateOfBirth, String clubName)
        {
            Coach coach = null;
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    String path = Path.Combine(Server.MapPath("~/images"), fileName);
                    file.SaveAs(path);

                    String pathInXML = "/images/" + file.FileName;

                    coach = new Coach(name, pathInXML, position, dateOfBirth, clubName);

                    _repository.EditCoach(coach);                    
                    return RedirectToAction("Index");
                }
                else
                {                   
                    coach = new Coach(name, position, dateOfBirth, clubName);

                    _repository.EditCoach(coach);
                    return RedirectToAction("Index");
                }

                
            }
            return View(coach);
        }

        // GET: Coaches/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = _repository.GetCoachByName(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(string id)
        {
            _repository.DeleteCoach(id);
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
