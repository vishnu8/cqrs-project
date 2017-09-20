﻿using System;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces;
using MediatR;
using MyApp.Domain.Core.Notifications;
using MyApp.Application.ViewModels;

namespace MyApp.Web.Controllers
{
    public class EntryLogController : BaseController
    {
        private readonly IEntryLogAppService entryLogAppService;

        public EntryLogController(
            IEntryLogAppService entryLogAppService, 
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            this.entryLogAppService = entryLogAppService;
        }

        public IActionResult Index()
        {
            var entryLogs = entryLogAppService.GetByUser();
            return View(entryLogs);
        }

        [HttpPost]
        public IActionResult Add(CreateUpdateEntryLogViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            entryLogAppService.Create(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete([FromQuery]Guid id)
        {
            entryLogAppService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}