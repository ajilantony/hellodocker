using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DockerASPNETCore.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloDocker.Controllers
{
    [Route("api")]
    public class UserProfilesController : Controller
    {
        [HttpGet("userprofiles")]
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            var userProfiles = new List<UserProfile>();
            userProfiles.Add(new UserProfile() { Id = 1, Guid = new Guid(), Username = "akalathil", FirstName = "Ajil", LastName = "Kalathil", Email = "akalathil@usga.org" });
            return userProfiles;
        }

        [HttpGet("userprofiles/{id}")]
        public UserProfile Get(int id)
        {
            return new UserProfile() { Id = 1, Guid = Guid.NewGuid(), Username = "akalathil", FirstName = "Ajil", LastName = "Kalathil", Email = "akalathil@usga.org" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("userprofiles/{id}")]
        public void Put(int id, [FromBody]UserProfile userProfile)
        {
        }

        // DELETE api/values/5
        [HttpDelete("userprofiles/{id}")]
        public void Delete(int id)
        {
        }


    }
}
