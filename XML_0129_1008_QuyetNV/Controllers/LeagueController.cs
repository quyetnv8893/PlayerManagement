﻿using PlayerManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlayerManagement.Controllers
{
    public class LeagueController : Controller
    {
        private ILeagueRepository _repository;        

        public LeagueController(): this(new LeagueRepository())
        {
        }

        public LeagueController(ILeagueRepository _repository)
        {
            this._repository = _repository;
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(League league)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.InsertLeague(league);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("","Error insert new league: " + e.Message);
                }
            }
            return View(league);
        }

        public ActionResult Edit(String id)
        {
            String name = id;
            League league = _repository.GetLeagueByName(name);
            if (league==null){
                return RedirectToAction("Index");
            }
            return View(league);
        }

        [HttpPost]
        public ActionResult Edit(League league)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.EditLeague(league);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Error insert new league: " + e.Message);
                }
            }
            return View(league);
        }

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

        [HttpPost]
        public ActionResult Delete(League league)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.DeleteLeague(league.name);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMsg = "Error deleting record. " + e.Message;
                    return View(_repository.GetLeagueByName(league.name));
                }
            }
            return View(league);
        }
    }
}