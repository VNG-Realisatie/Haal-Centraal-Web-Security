using HaalCentraal.Viewer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HaalCentraal.Viewer.Controllers
{
    [Authorize(Policy = "CanAccessBag")]

    public class BagController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BagController> _logger;


        public BagController(IHttpClientFactory httpClientFactory,
                             ILogger<BagController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new GetAdressenViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(GetAdressenCommandModel model)
        {
            var vm = new GetAdressenViewModel
            {
                Command = model
            };

            try
            {
                var client = new BagBevragen.Client(_httpClientFactory.CreateClient("bag"));

                vm.Resultaat = await client.ZoekAsync(zoek: model.ZoekTerm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                vm.Fout = ex switch
                {
                    BagBevragen.ApiException<BagBevragen.Foutbericht> exc2 => exc2.Result,
                    BagBevragen.ApiException exc => new BagBevragen.Foutbericht { Status = exc.StatusCode },
                    _ => new BagBevragen.Foutbericht { Title = ex.Message },
                };
            }

            return View("Index", vm);
        }
    }
}
