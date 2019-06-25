﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtCoreIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            List<Student> data = new List<Student>
            {
                new Student() {StudentId = 1, FirstName = "Shahid", LastName = "AB"},
                new Student() {StudentId = 2, FirstName = "Ahsan", LastName = "Habib"},
                new Student() {StudentId = 3, FirstName = "Hasan", LastName = "Abul"}
            };
            return new ObjectResult(data);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
