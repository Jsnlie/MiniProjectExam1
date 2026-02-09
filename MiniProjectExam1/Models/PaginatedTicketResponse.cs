namespace MiniProjectExam1.Models
{
    public class PaginatedTicketResponse
    {
        public List<TicketModel> Tickets { get; set; } = new();
        public int TotalTickets { get; set; }
    }
}