using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HaalCentraal.BrkBevragen.Controllers
{
    public class BrkBevragenController : ControllerBase
    {
        private readonly ILogger<BrkBevragenController> _logger;

        public BrkBevragenController(ILogger<BrkBevragenController> logger)
        {
            _logger = logger;
        }

        public override Task<BeslagHal> GetBeslag([FromQuery] string fields, string kadastraalonroerendezaakidentificatie, string beslagidentificatie, [FromHeader(Name = "Accept-Crs")] AcceptCrs8? accept_Crs)
        {
            throw new NotImplementedException();
        }

        public override Task<BeslagHalCollectie> GetBeslagenKadastraalOnroerendeZaak([FromQuery] string fields, string kadastraalonroerendezaakidentificatie, [FromHeader(Name = "Accept-Crs")] AcceptCrs7? accept_Crs)
        {
            throw new NotImplementedException();
        }

        public override Task<HypotheekHal> GetHypotheek([FromQuery] string fields, string kadastraalonroerendezaakidentificatie, string hypotheekidentificatie, [FromHeader(Name = "Accept-Crs")] AcceptCrs6? accept_Crs)
        {
            throw new NotImplementedException();
        }

        public override Task<HypotheekHalCollectie> GetHypothekenKadastraalOnroerendeZaak([FromQuery] string fields, string kadastraalonroerendezaakidentificatie, [FromHeader(Name = "Accept-Crs")] AcceptCrs5? accept_Crs)
        {
            throw new NotImplementedException();
        }

        public override Task<KadasterNietNatuurlijkPersoonHalCollectie> GetKadasterNietNatuurlijkPersonen([FromQuery] string fields, [FromQuery] string q)
        {
            throw new NotImplementedException();
        }

        public override Task<KadasterNietNatuurlijkPersoonHal> GetKadasterNietNatuurlijkPersoon([FromQuery] string fields, string kadasternietnatuurlijkpersoonidentificatie)
        {
            throw new NotImplementedException();
        }

        public override Task<KadasterNatuurlijkPersoonHalCollectie> GetKadasterPersonen([FromQuery] string fields, [FromQuery] string q)
        {
            throw new NotImplementedException();
        }

        public override Task<KadasterNatuurlijkPersoonHal> GetKadasterPersoon([FromQuery] string fields, string kadasternatuurlijkpersoonidentificatie)
        {
            throw new NotImplementedException();
        }

        public override Task<KadastraalOnroerendeZaakHal> GetKadastraalOnroerendeZaak(string kadastraalonroerendezaakidentificatie, [FromHeader(Name = "Accept-Crs")] AcceptCrs2? accept_Crs, [FromQuery] string expand, [FromQuery] string fields)
        {
            throw new NotImplementedException();
        }

        public async override Task<KadastraalOnroerendeZaakHalCollectie> GetKadastraalOnroerendeZaken([FromHeader(Name = "Accept-Crs")] AcceptCrs? accept_Crs, [FromQuery] string expand, [FromQuery] string fields, [FromQuery] string kadastraleAanduiding, [FromQuery] string burgerservicenummer, [FromQuery] string persoon__identificatie, [FromQuery] TypeGerechtigdeEnum? zakelijkGerechtigde__type, [FromQuery] string postcode, [FromQuery] int? huisnummer, [FromQuery] string huisletter, [FromQuery] string huisnummertoevoeging)
        {
            _logger.LogInformation("Enter");

            return new KadastraalOnroerendeZaakHalCollectie
            {
                _links = new HalCollectionLinks
                {
                    Self = new HalLink
                    {
                        Href = "https://localhost",
                        Templated = false,
                        Title = ""
                    }
                },
                _embedded = new KadastraalOnroerendeZaakHalCollectieEmbedded
                {
                    KadastraalOnroerendeZaken = new System.Collections.Generic.List<KadastraalOnroerendeZaakHal>
                    {
                        new KadastraalOnroerendeZaakHal
                        {
                            Identificatie = "12345"
                        }
                    }
                }
            };
        }

        public override Task<PrivaatrechtelijkeBeperkingHal> GetPrivaatrechtelijkeBeperking([FromHeader(Name = "Accept-Crs")] AcceptCrs10? accept_Crs, [FromQuery] string fields, string kadastraalonroerendezaakidentificatie, string privaatrechtelijkebeperkingidentificatie)
        {
            throw new NotImplementedException();
        }

        public override Task<PrivaatrechtelijkeBeperkingHalCollectie> GetPrivaatrechtelijkeBeperkingen([FromHeader(Name = "Accept-Crs")] AcceptCrs9? accept_Crs, [FromQuery] string fields, string kadastraalonroerendezaakidentificatie)
        {
            throw new NotImplementedException();
        }

        public override Task<PubliekrechtelijkeBeperkingHalCollectie> GetPubliekrechtelijkeBeperkingen([FromQuery] string fields, [FromQuery] string kadastraalOnroerendeZaakIdentificatie)
        {
            throw new NotImplementedException();
        }

        public override Task<ZakelijkGerechtigdeHal> GetZakelijkGerechtigde([FromQuery] string fields, string kadastraalonroerendezaakidentificatie, string zakelijkgerechtigdeidentificatie, [FromHeader(Name = "Accept-Crs")] AcceptCrs3? accept_Crs)
        {
            throw new NotImplementedException();
        }

        public override Task<ZakelijkGerechtigdeHalCollectie> GetZakelijkGerechtigden([FromQuery] string fields, string kadastraalonroerendezaakidentificatie, [FromQuery] TypeGerechtigdeEnum? type, [FromHeader(Name = "Accept-Crs")] AcceptCrs4? accept_Crs)
        {
            throw new NotImplementedException();
        }
    }
}
