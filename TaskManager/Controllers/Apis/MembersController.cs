﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TaskManager.Models;
using TaskManager.Dtos;

namespace TaskManager.Controllers.Apis
{
    public class MembersController : ApiController
    {
        private ApplicationDbContext _context;

        public MembersController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<MembersDto> GetMovies(string query = null)
        {
            var membersQuery = _context.Members.ToList();            

            //if (!String.IsNullOrWhiteSpace(query))
            //    membersQuery = membersQuery.Where(m => m.MemberName.Contains(query));

            return membersQuery
                .ToList()
                .Select(Mapper.Map<Members, MembersDto>);
        }
    }
}