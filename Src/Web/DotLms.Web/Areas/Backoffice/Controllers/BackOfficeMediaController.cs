﻿using System.Web.Mvc;
using Bytes2you.Validation;
using DotLms.Services.Data;
using DotLms.Web.Models;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackOfficeMediaController : Controller
    {
        private readonly FileService fileService;

        public BackOfficeMediaController(FileService fileService)
        {
            Guard.WhenArgument(fileService, nameof(fileService)).IsNull().Throw();

            this.fileService = fileService;
        }
        // GET: Backoffice/Media
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(MediaItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                fileService.SaveFile(model.File);
            }
            return View();
        }
    }
}