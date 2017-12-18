﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    public class TaskProceduresController : Controller
    {
        private ApplicationDbContext _context;

        public TaskProceduresController()
        {
            _context = new ApplicationDbContext();
        }
        
        public ViewResult New()
        {
            return View("TaskProceduresForm");
        }

        public ActionResult Edit(int id)
        {
            var taskProc = _context.TaskProcedures.SingleOrDefault(t => t.TaskProcedureId == id);

            if (taskProc == null)
                return HttpNotFound();

            return View("TaskProceduresForm", taskProc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TaskProcedures taskProcedure)
        {
            if (!ModelState.IsValid)
            {               
                return View("TaskProceduresForm", taskProcedure);
            }

            if (taskProcedure.TaskProcedureId == 0)
            {
                _context.TaskProcedures.Add(taskProcedure);
            }
            else
            {
                var taskProcInDb = _context.TaskProcedures.Single(t => t.TaskProcedureId == taskProcedure.TaskProcedureId);
                taskProcInDb.TaskProcedureOrder = taskProcedure.TaskProcedureOrder;
                taskProcInDb.TaskProcedureDescription = taskProcedure.TaskProcedureDescription;
                taskProcInDb.TaskVideoFile = taskProcedure.TaskVideoFile;
                taskProcInDb.TaskId = taskProcedure.TaskId;               
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Tasks");
        }
    }
}