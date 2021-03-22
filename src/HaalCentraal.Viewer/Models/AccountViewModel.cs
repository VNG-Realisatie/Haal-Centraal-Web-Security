using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaalCentraal.Viewer.Models
{
    public class AccountViewModel
    {
        public IDictionary<string, string> Tokens { get; } = new Dictionary<string, string>();
        public IDictionary<string, string> Claims { get; } = new Dictionary<string, string>();
    }
}
