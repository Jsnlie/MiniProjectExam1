namespace MiniProjectExam1.Models
{
    public class TicketModel
    {
        public string? CategoryTicket { get; set; }
        public string? CodeTicket { get; set; }
        public string? NameTicket { get; set; }
        public decimal? Price { get; set; }
        public DateTime? EventDate { get; set; }
        public DateTime? MinEventDate { get; set; }
        public DateTime? MaxEventDate { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderState { get; set; }
        public int? Quota { get; set; }
        public int? RemainingQuota { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
