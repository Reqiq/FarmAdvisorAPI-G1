namespace FarmAdvisor.Models.Models
{
    public class FieldModel
    {
        public Guid FieldId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int? Alt { get; set; }
        public string? Polygon { get; set; }


        public FarmModel? Farm { get; set; }  
        public ICollection<SensorModel>? Sensors { get; set; }
        
    }
}
