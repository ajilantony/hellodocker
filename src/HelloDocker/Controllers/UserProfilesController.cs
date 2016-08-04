using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using HelloDocker.Models;
using Npgsql;
using Dapper;
using Swashbuckle.SwaggerGen.Annotations;
//using Cassandra;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloDocker.Controllers
{
    [Route("api")]
    public class UserProfilesController : Controller
    {
        [HttpGet("userprofiles")]
        [Produces(typeof(IEnumerable<UserProfile>))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(IEnumerable<UserProfile>))]
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            IEnumerable<UserProfile> userProfiles;
            using (var conn = new NpgsqlConnection("Server=dockerpochost.cloudapp.net;Port=5432;Username=postgres;Password=;Database=postgres"))
            {
                conn.Open();
                userProfiles = conn.Query<UserProfile>("Select * from UserProfile");
            }
            return userProfiles;
        }

        [HttpGet("userprofiles/{id}")]
        public UserProfile GetUserProfile(int id)
        {
            UserProfile userProfile;
            using (var conn = new NpgsqlConnection("Server=dockerpochost.cloudapp.net;Port=5432;Username=postgres;Password=;Database=postgres"))
            {
                conn.Open();
                var query = "Select * from UserProfile Where Id = @Id";
                userProfile = conn.QuerySingle<UserProfile>(query, id);
            }
            return userProfile;
        }

        [HttpPost("userprofiles")]
        public void InsertUserProfile([FromBody]UserProfile userProfile)
        {

            using (var conn = new NpgsqlConnection("Server=dockerpochost.cloudapp.net;Port=5432;Username=postgres;Password=;Database=postgres"))
            {
                conn.Open();
                var query = "INSERT INTO userprofile (id, firstname, lastname) VALUES (@Id, @FirstName,@LastName)";
                var parameters = new { userProfile.Id, userProfile.FirstName, userProfile.LastName };
                conn.Execute(query, parameters);
            }

            //Create a cluster instance using 3 cassandra nodes.
            //var cluster = Cluster.Builder()
            //  .AddContactPoints("http://dockerpochost.cloudapp.net:7000/")
            //  .Build();
            ////Create connections to the nodes using a keyspace
            //var session = cluster.Connect("sample_keyspace");
            //var ps = session.Prepare("UPDATE user_profiles SET birth=? WHERE key=?");

            ////...bind different parameters every time you need to execute
            //var statement = ps.Bind(new DateTime(1942, 11, 27), "hendrix");
            ////Execute the bound statement with the provided parameters
            //session.Execute(statement);
            ////Execute a query on a connection synchronously
            //var rs = session.Execute("SELECT * FROM sample_table");
            ////Iterate through the RowSet
            //foreach (var row in rs)
            //{
            //    var value = row.GetValue<int>("sample_int_column");
            //    //do something with the value
            //}
        }

        // PUT api/values/5
        [HttpPut("userprofiles/{id}")]
        public void UpdateUserProfile(int id, [FromBody]UserProfile userProfile)
        {
            using (var conn = new NpgsqlConnection("Server=dockerpochost.cloudapp.net;Port=5432;Username=postgres;Password=;Database=postgres"))
            {
                conn.Open();
                var query = "Update userprofile Set firstname = @FirstName, lastname = @LastName Where Id = @Id";
                var parameters = new { userProfile.FirstName, userProfile.LastName, userProfile.Id};
                conn.Execute(query, parameters);
            }
        }

        // DELETE api/values/5
        [HttpDelete("userprofiles/{id}")]
        public void DeleteUserProfile(int id)
        {
            using (var conn = new NpgsqlConnection("Server=dockerpochost.cloudapp.net;Port=5432;Username=postgres;Password=;Database=postgres"))
            {
                conn.Open();
                var query = "Delete from userprofile Where id = @id";
                conn.Execute(query, id);
            }
        }


    }
}
