using System;
using System.Collections.Generic;
using System.Text;


namespace FarmAdvisor.DataAccess.MSSQL.Entities
{
    public class User
    {
        public Guid UserID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; } 
        public string? Phone { get; set; }
        public string? AuthId{ get; set; }

       public ICollection<Farm> Farms { get; set; }
       public virtual ICollection<Sensor> Sensors { get; set;}


    }
}
