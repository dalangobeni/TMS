﻿using AutoMapper;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;

namespace TaskManager.Controllers.Apis
{
    public class SubtaskLevel1Controller : ApiController
    {
        private ApplicationDbContext _context;

        public SubtaskLevel1Controller()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/subtasklevel1
        public IHttpActionResult GetSubtaskLevel1()
        {
            var subTasksQuery = _context.SubTasksLevel1
               .Include(s => s.Members)
               .Include(s => s.Prices)
               .Include(s => s.Tasks);

            var subTaskDtos = subTasksQuery
                .ToList()
                .Select(Mapper.Map<SubTasksLevel1, SubtaskLevel1Dto>);

            return Ok(subTaskDtos);
        }

        // DELETE /api/subtasklevel1
        public IHttpActionResult DeleteSubtaskLevel1(int id)
        {
            var subtaskInDb = _context.SubTasksLevel1.SingleOrDefault(t => t.SubTaskId == id);

            if (subtaskInDb == null)
                return NotFound();

            _context.SubTasksLevel1.Remove(subtaskInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}