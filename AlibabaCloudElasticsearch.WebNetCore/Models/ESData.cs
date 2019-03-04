using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlibabaCloudElasticsearch.WebNetCore.Models
{
    public class ESData
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public int YearsInRole { get; set; }
        public string Company { get; set; }

    }
}
