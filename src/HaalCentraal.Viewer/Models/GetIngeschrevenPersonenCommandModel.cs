using System;

namespace HaalCentraal.Viewer.Models
{
    public class GetIngeschrevenPersonenCommandModel
    {
        public string Geslachtsnaam { get; set; } = "";
        public DateTimeOffset? Geboortedatum { get; set; }
    }
}
