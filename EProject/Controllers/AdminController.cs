﻿using EProject.Models;
using EProject.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<DefaultUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager,UserManager<DefaultUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ListOfRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        //ADD ROLE
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        //ADD ROLE
        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new()
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListOfRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        //EDIT ROLE
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewData["ErrorMessage"] = $"No role with Id '{id}' was found";
                return View("Error");
            }

            EditRoleViewModel model = new()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        //Ediit Role
        [HttpPost]

        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewData["ErrorMessage"] = $"No role with Id '{model.Id}' was found";
                return View("Error");
            }
            else
            {
                role.Name = model.RoleName;

                var result  = await  _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return  RedirectToAction("ListOfRoles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }


    }
}