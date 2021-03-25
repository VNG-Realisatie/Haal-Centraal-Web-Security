using HaalCentraalViewer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HaalCentraal.Viewer.Controllers
{
    [Authorize(Policy = "CanAccessBrk")]
    public class BrkController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BrkController> _logger;

        private static GetKadastraalOnroerendeZakenViewModel ViewModel { get; set; } = new GetKadastraalOnroerendeZakenViewModel();

        public BrkController(IHttpClientFactory httpClientFactory,
                             ILogger<BrkController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(GetKadastraalOnroerendeZakenCommandModel model)
        {
            ViewModel.Command = model;

            try
            {
                var client = new BrkBevragen.Client(_httpClientFactory.CreateClient("brk"));

                ViewModel.Resultaat = await client.GetKadastraalOnroerendeZakenAsync(postcode: model.Postcode, huisnummer: model.Huisnummer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                ViewModel.Fout = ex switch
                {
                    BrkBevragen.ApiException<BrkBevragen.Foutbericht> exc2 => exc2.Result,
                    BrkBevragen.ApiException exc => new BrkBevragen.Foutbericht { Status = exc.StatusCode },
                    _ => new BrkBevragen.Foutbericht { Title = ex.Message },
                };
            }

            return RedirectToAction("Index");
        }
    }
}
