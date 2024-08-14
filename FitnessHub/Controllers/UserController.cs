using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FitnessHub.Models;

namespace FitnessHub.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager;

        public UserController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: User
        public ActionResult Index()
        {
            var users = UserManager.Users.ToList();
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = UserManager.FindById(id.ToString()); // Use FindById with string ID
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new EditUserViewModel
            {
                UserID = id, // Assign int ID
               
                Email = user.Email,
                Role = UserManager.GetRoles(user.Id).FirstOrDefault()
            };

            return View(model);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = UserManager.FindById(id.ToString()); // Use FindById with string ID
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new EditUserViewModel
            {
                UserID = id, // Assign int ID
               
                Email = user.Email,
                Role = UserManager.GetRoles(user.Id).FirstOrDefault()
            };

            return View(model);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(model.UserID.ToString()); // Use FindById with string ID
                if (user == null)
                {
                    return HttpNotFound();
                }

                
                user.Email = model.Email;

                var currentRole = UserManager.GetRoles(user.Id).FirstOrDefault();
                if (currentRole != model.Role)
                {
                    UserManager.RemoveFromRole(user.Id, currentRole);
                    UserManager.AddToRole(user.Id, model.Role);
                }

                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                AddErrors(result);
            }

            return View(model);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = UserManager.FindById(id.ToString()); // Use FindById with string ID
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = UserManager.FindById(id.ToString()); // Use FindById with string ID
            if (user != null)
            {
                var result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                AddErrors(result);
            }

            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
