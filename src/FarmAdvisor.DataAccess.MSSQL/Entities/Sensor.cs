using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAdvisor.DataAccess.MSSQL.Entities
{
    public class Sensor
    {
        public Guid SensorId { get; set; }
        public string? SerialNumber{ get; set; }
        public DateTime LastCommunication { get; set; }

        public int BatteryStatus { get; set; }
        public int OptimalGDD { get; set; }
        public DateTime CuttingDateTimeCalculated { get; set; }

        public DateTime LastForecastDate { get; set; }

        public double Lat { get; set; }
        public double Long { get; set; }

        public Enum? State { get; set; }


        public virtual Field? Field { get; set; }
        public virtual ICollection<User>? Users { get; set; }    





}
}
