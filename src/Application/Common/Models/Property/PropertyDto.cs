using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clean.Application.Common.Models.Property
{
    public class PropertyDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContact { get; set; }
        public string Image { get; set; }
    }

}
