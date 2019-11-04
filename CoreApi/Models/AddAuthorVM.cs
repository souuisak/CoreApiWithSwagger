using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Models
{
    public class AddAuthorVM
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Trigram { get; set; }
    }
}
