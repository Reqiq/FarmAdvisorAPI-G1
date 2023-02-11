namespace FarmAdvisor.Models.Models
{

    public class NotificationModel
    {


        public Guid NotificationId { get; set; }

        public string? Title { get; set; }

        public string? Message { get; set; }

        public Enum? SentBy { get; set; }

        public Enum? Status { get; set; }

        public FarmModel? Farm { get; set; }

    }
}
