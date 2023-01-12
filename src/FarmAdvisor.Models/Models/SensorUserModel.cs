using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAdvisor.Models.Models
{
    public class SensorUserModel
    {
        public ICollection<SensorModel>? Sensors{ get; set; }
        public DateTime TimeStamp { get; set; }
        public ICollection<UserModel>? Users { get; set; }
    }
}
