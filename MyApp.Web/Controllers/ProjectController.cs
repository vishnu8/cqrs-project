﻿using System;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces;
using MediatR;
using MyApp.Domain.Core.Notifications;
using MyApp.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace MyApp.Web.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectAppService projectAppService;

        public ProjectController(
            IProjectAppService productAppService, 
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            this.projectAppService = productAppService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await projectAppService.GetAll();
            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var createNewProjectViewModel = await projectAppService.GetCreateNewProjectData();
            return View(createNewProjectViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProjectViewModel projectViewModel)
        {
            if (!ModelState.IsValid) return View(projectViewModel);
            projectAppService.Create(projectViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var updateProjectData = await projectAppService.GetUpdateProjectData(id);
            return View(updateProjectData);
        }

        [HttpPost]
        public IActionResult Edit(UpdateProjectViewModel updateProjectViewModel)
        {
            if (!ModelState.IsValid) return View(updateProjectViewModel);
            projectAppService.Update(updateProjectViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete([FromQuery]Guid id)
        {
            projectAppService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
