using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DEMO.VM.User;
using DEMO.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DEMO.WEB.Controllers
{
    public class UsersController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<IActionResult> Index()
        {
            var json = await client.GetStringAsync(APIDATA.URL + $"Users/Index");
            var users = JsonSerializer.Deserialize<IEnumerable<UserIndexViewModel>>(json);
            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Birthday")] UserCreateViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var serializedItem = JsonSerializer.Serialize(user);
                    var response = await client.PostAsync(APIDATA.URL + $"Users", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        AddModelError();
                    }
                }
            }
            catch
            {
                AddModelError();
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var json = await client.GetStringAsync(APIDATA.URL + $"Users/Edit/{id}");
            var user = JsonSerializer.Deserialize<UserEditViewModel>(json);

            return user == null ? NotFound() : View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Email,Birthday")] UserEditViewModel user)
        {
            if (id != user.Id) return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    var serializedItem = JsonSerializer.Serialize(user);
                    var response = await client.PutAsync(APIDATA.URL + $"Users/{id}", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        AddModelError();
                    }
                }
            }
            catch
            {
                AddModelError();
            }
            return View(user);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var json = await client.GetStringAsync(APIDATA.URL + $"Users/Details/{id}");
            var user = JsonSerializer.Deserialize<UserDetailsViewModel>(json);

            return user == null ? NotFound() : View(user);
        }

        public async Task<IActionResult> Delete(Guid? id, bool? saveChangesError = false)
        {
            if (id == null) return NotFound();

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            var json = await client.GetStringAsync(APIDATA.URL + $"Users/Delete/{id}");
            var user = JsonSerializer.Deserialize<UserDeleteViewModel>(json);

            return user == null ? NotFound() : View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await client.DeleteAsync(APIDATA.URL + $"Users/{id}");
            return response.IsSuccessStatusCode ? RedirectToAction(nameof(Index)) : RedirectToAction("Delete", new { id = id, saveChangesError = true });
        }

        public void AddModelError() => ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

    }
}