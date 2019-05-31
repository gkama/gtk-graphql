using System;
using System.Collections.Generic;
using System.Text;

namespace gkama.graph.ql.data
{
    public class CountryPostalCode
    {
        public string code { get; set; }
        public int num_postal_codes { get; set; }
        public string min_postal_code { get; set; }
        public string max_postal_code { get; set; }
    }
}
