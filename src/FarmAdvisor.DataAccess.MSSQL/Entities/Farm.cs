using System;
using System.Collections.Generic;
using System.Text;


namespace FarmAdvisor.DataAccess.MSSQL.Entities
{
    public class Farm
    {
        public Guid FarmId { get; set; }
        public string Name { get; set; }  = string.Empty;
        public string Postcode{ get; set;}
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<Notification> Notifications{ get; set; }
        public virtual ICollection<Field> Fields { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
