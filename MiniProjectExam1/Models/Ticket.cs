namespace MiniProjectExam1.Models
{
    public class Ticket
    {
        public string CodeTicket { get; set; }
        public string NameTicket { get; set; }
        public string CategoryTicket { get; set; }
        public DateTime EventDate { get; set; }
        public decimal Price { get; set; }
        public int Quota { get; set; }
        public int RemainingQuota { get; set; }
    }
}
