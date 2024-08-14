using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FitnessHub.Models;

namespace FitnessHub.Controllers
{
    [Authorize]
    [RoutePrefix("api/users")]
    public class UserApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;

        public UserApiController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: api/users
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetUsers()
        {
            var users = userManager.Users.ToList();
            var userDtos = users.Select(user => new UserDto
            {
                UserID = int.Parse(user.Id), // Convert string ID to int
                
                Email = user.Email,
                Role = userManager.GetRoles(user.Id).FirstOrDefault()
            }).ToList();

            return Ok(userDtos);
        }

        // GET: api/users/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetUser(int id)
        {
            var userId = id.ToString(); // Convert int ID to string
            var user = userManager.FindById(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                UserID = id,
                
                Email = user.Email,
                Role = userManager.GetRoles(user.Id).FirstOrDefault()
            };

            return Ok(userDto);
        }

        // POST: api/users
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> PostUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                
                Email = userDto.Email
            };

            var result = await userManager.CreateAsync(user, "DefaultPassword123!"); // You should generate a secure password

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user.Id, userDto.Role);
                userDto.UserID = int.Parse(user.Id); // Convert string ID to int
                return CreatedAtRoute("DefaultApi", new { id = user.Id }, userDto);
            }

            return BadRequest(result.Errors.FirstOrDefault());
        }

        // PUT: api/users/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> PutUser(int id, UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = id.ToString(); // Convert int ID to string
            var user = userManager.FindById(userId);
            if (user == null)
            {
                return NotFound();
            }

            
            user.Email = userDto.Email;

            var currentRole = userManager.GetRoles(user.Id).FirstOrDefault();
            if (currentRole != userDto.Role)
            {
                await userManager.RemoveFromRoleAsync(user.Id, currentRole);
                await userManager.AddToRoleAsync(user.Id, userDto.Role);
            }

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(userDto);
            }

            return BadRequest(result.Errors.FirstOrDefault());
        }

        // DELETE: api/users/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            var userId = id.ToString(); // Convert int ID to string
            var user = userManager.FindById(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors.FirstOrDefault());
        }
    }
}
