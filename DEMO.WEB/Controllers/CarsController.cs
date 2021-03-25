using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DEMO.VM;
using DEMO.VM.Car;
using DEMO.VM.User;
using DEMO.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DEMO.WEB.Controllers
{
    public class CarsController : Controller
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<IActionResult> Index()
        {
            var json = await client.GetStringAsync(APIDATA.URL + $"Cars/Index");
            var users = JsonConvert.DeserializeObject<IEnumerable<CarIndexViewModel>>(json);
            return View(users);
        }

        public async Task<IActionResult> Create()
        {
            var json = await client.GetStringAsync(APIDATA.URL + $"Users/Select");
            var users = JsonConvert.DeserializeObject<IEnumerable<SelectItem>>(json);
            ViewData["OwnerId"] = new SelectList(users, "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerId,Model,Year,Color")] CarCreateViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var serializedItem = JsonConvert.SerializeObject(user);
                    var response = await client.PostAsync(APIDATA.URL + $"Cars", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
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
            var json = await client.GetStringAsync(APIDATA.URL + $"Users/Select");
            var users = JsonConvert.DeserializeObject<IEnumerable<SelectItem>>(json);
            ViewData["OwnerId"] = new SelectList(users, "Value", "Text");
            return View(user);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var jsonCar = await client.GetStringAsync(APIDATA.URL + $"Cars/Edit/{id}");
            var jsonUser = await client.GetStringAsync(APIDATA.URL + $"Users/Select");

            var car = JsonConvert.DeserializeObject<CarEditViewModel>(jsonCar);
            var users = JsonConvert.DeserializeObject<IEnumerable<SelectItem>>(jsonUser);

            ViewData["OwnerId"] = new SelectList(users, "Value", "Text", car.OwnerId);

            return car == null ? NotFound() : View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OwnerId,Model,Year,Color")] CarEditViewModel car)
        {
            if (id != car.Id) return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    var serializedItem = JsonConvert.SerializeObject(car);
                    var response = await client.PutAsync(APIDATA.URL + $"Cars/{id}", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
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
            var json = await client.GetStringAsync(APIDATA.URL + $"Users/Select");
            var users = JsonConvert.DeserializeObject<IEnumerable<SelectItem>>(json);
            ViewData["OwnerId"] = new SelectList(users, "Value", "Text", car.OwnerId);
            return View(car);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var json = await client.GetStringAsync(APIDATA.URL + $"Cars/Details/{id}");
            var car = JsonConvert.DeserializeObject<CarDetailsViewModel>(json);

            return car == null ? NotFound() : View(car);
        }

        public async Task<IActionResult> Delete(Guid? id, bool? saveChangesError = false)
        {
            if (id == null) return NotFound();

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            var json = await client.GetStringAsync(APIDATA.URL + $"Cars/Delete/{id}");
            var car = JsonConvert.DeserializeObject<CarDeleteViewModel>(json);

            return car == null ? NotFound() : View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await client.DeleteAsync(APIDATA.URL + $"Cars/{id}");
            return response.IsSuccessStatusCode ? RedirectToAction(nameof(Index)) : RedirectToAction("Delete", new { id = id, saveChangesError = true });
        }

        public void AddModelError() => ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

    }
}
