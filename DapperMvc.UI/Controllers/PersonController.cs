using DapperMvc.Data.Models.Domain;
using DapperMvc.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DapperMvc.UI.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View(new Person());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            try
            {
                if(!ModelState.IsValid)
                    return View(person);

                bool addResult = await _personRepository.AddAsync(person);

                if(addResult) 
                    TempData["msg"] = "Successfully added";
                else
                    TempData["msg"] = "Failed to add";
            }

            catch (Exception ex)
            {
                TempData["msg"] = "Failed to add";
            }

            return RedirectToAction(nameof(Add));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(person);

                bool addResult = await _personRepository.UpdateAsync(person);

                if (addResult)
                    TempData["msg"] = "Successfully updated";

                else
                    TempData["msg"] = "Failed to update";
            }

            catch (Exception ex)
            {
                TempData["msg"] = "Failed to add";
            }
            
            return View(person);
        }


        [HttpGet]
        public async Task<IActionResult> DisplayAll(Person person)
        {
            var people = await _personRepository.GetAllAsync();

            return View(people);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _personRepository.DeleteAsync(id);

            return RedirectToAction(nameof(DisplayAll));
        }
    }
}
