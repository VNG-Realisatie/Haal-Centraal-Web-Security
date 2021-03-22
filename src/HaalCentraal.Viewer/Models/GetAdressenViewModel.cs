using BagBevragen;

namespace HaalCentraal.Viewer.Models
{
    public class GetAdressenViewModel
    {
        public GetAdressenCommandModel Command { get; set; } = new GetAdressenCommandModel();
        public ZoekResultaatHalCollectie Resultaat { get; set; }
        public Foutbericht Fout { get; internal set; }
    }
}
