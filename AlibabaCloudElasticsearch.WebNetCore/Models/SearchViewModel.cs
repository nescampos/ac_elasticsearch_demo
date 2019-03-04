using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlibabaCloudElasticsearch.WebNetCore.Models
{
    public class SearchViewModel
    {
        public IEnumerable<ESData> Respuesta { get; internal set; }
        public string IndexName { get; internal set; }
    }
}
