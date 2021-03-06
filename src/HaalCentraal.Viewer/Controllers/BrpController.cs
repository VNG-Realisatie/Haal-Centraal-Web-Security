﻿using HaalCentraal.Viewer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HaalCentraal.Viewer.Controllers
{
    [Authorize(Policy = "CanAccessBrp")]
    public class BrpController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BrpController> _logger;

        public BrpController(IHttpClientFactory httpClientFactory,
                             ILogger<BrpController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(new GetIngeschrevenPersonenViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(GetIngeschrevenPersonenCommandModel model)
        {
            var vm = new GetIngeschrevenPersonenViewModel();
            vm.Command = model;

            try
            {
                var client = new BrpBevragen.Client(_httpClientFactory.CreateClient("brp"));

                vm.Resultaat = await client.GetIngeschrevenPersonenAsync(naam__geslachtsnaam: model.Geslachtsnaam, geboorte__datum: model.Geboortedatum);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                vm.Fout = ex switch
                {
                    BrpBevragen.ApiException<BrpBevragen.Foutbericht> exc2 => exc2.Result,
                    BrpBevragen.ApiException exc => new BrpBevragen.Foutbericht { Status = exc.StatusCode },
                    _ => new BrpBevragen.Foutbericht { Title = ex.Message },
                };
            }

            return View("Index", vm);
        }
    }
}
