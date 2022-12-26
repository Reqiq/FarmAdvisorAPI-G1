using System;
using System.Collections.Generic;

using System.Text;

namespace FarmAdvisor.DataAccess.MSSQL.Entities
{

    public class Notification
    {


        public Guid NotificationId { get; set; }

        public string? Title { get; set; }

        public string? Message { get; set; }

        public Enum SentBy { get; set; }

        public Enum Status { get; set; }

        public virtual Farm Farm { get; set; }

    }
}
