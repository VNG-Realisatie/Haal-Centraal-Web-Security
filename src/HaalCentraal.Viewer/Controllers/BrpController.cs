using HaalCentraal.Viewer.Models;
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

        private static GetIngeschrevenPersonenViewModel ViewModel { get; set; } = new GetIngeschrevenPersonenViewModel();

        public BrpController(IHttpClientFactory httpClientFactory,
                             ILogger<BrpController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(GetIngeschrevenPersonenCommandModel model)
        {
            ViewModel.Command = model;

            try
            {
                var client = new BrpBevragen.Client(_httpClientFactory.CreateClient("brp"));

                ViewModel.Resultaat = await client.GetIngeschrevenPersonenAsync(naam__geslachtsnaam: model.Geslachtsnaam, geboorte__datum: model.Geboortedatum);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                ViewModel.Fout = ex switch
                {
                    BrpBevragen.ApiException<BrpBevragen.Foutbericht> exc2 => exc2.Result,
                    BrpBevragen.ApiException exc => new BrpBevragen.Foutbericht { Status = exc.StatusCode },
                    _ => new BrpBevragen.Foutbericht { Title = ex.Message },
                };
            }

            return RedirectToAction("Index");
        }
    }
}
