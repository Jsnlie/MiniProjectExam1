using System;
using System.Collections.Generic;

namespace MiniProjectExam1.Entities;

public partial class Ticket
{
    public string CodeTicket { get; set; } = null!;

    public string NameTicket { get; set; } = null!;

    public string CategoryTicket { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public int Price { get; set; }

    public int Quota { get; set; }

    public int RemainingQuota { get; set; }

    public virtual ICollection<BookedTicketDetail> BookedTicketDetails { get; set; } = new List<BookedTicketDetail>();
}
