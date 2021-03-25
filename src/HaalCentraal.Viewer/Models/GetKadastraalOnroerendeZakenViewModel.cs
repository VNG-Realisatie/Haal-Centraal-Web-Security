namespace HaalCentraalViewer.Models
{
    public class GetKadastraalOnroerendeZakenViewModel
    {
        public GetKadastraalOnroerendeZakenViewModel()
        {
            Command = new GetKadastraalOnroerendeZakenCommandModel();
        }

        public BrkBevragen.KadastraalOnroerendeZaakHalCollectie Resultaat { get; set; }
        public BrkBevragen.Foutbericht Fout { get; set; }
        public GetKadastraalOnroerendeZakenCommandModel Command { get; set; }
    }
}
