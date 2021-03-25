using BrpBevragen;

namespace HaalCentraal.Viewer.Models
{
    public class GetIngeschrevenPersonenViewModel
    {
        public GetIngeschrevenPersonenCommandModel Command { get; set; } = new GetIngeschrevenPersonenCommandModel();
        public IngeschrevenPersoonHalCollectie Resultaat { get; set; }
        public Foutbericht Fout { get; set; }
    }
}
