using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clean.Domain.Entities
{
    public class Property : BaseEntity
    {
        public string Type { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContact { get; set; }
        public string Image { get; set; }


    }
}
