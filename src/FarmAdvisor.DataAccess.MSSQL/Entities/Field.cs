using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAdvisor.DataAccess.MSSQL.Entities
{
    public class Field
    {
        public Guid FieldId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int? Alt { get; set; }
        public string? Polygon { get; set; }


        public virtual Farm? Farm { get; set; }  
        public virtual ICollection<Sensor>? Sensors { get; set; }
    }
}
