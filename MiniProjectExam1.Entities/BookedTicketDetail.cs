using System;
using System.Collections.Generic;

namespace MiniProjectExam1.Entities;

public partial class BookedTicketDetail
{
    public Guid Id { get; set; }

    public Guid BookedTicketId { get; set; }

    public string CodeTicket { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual BookedTicket BookedTicket { get; set; } = null!;

    public virtual Ticket CodeTicketNavigation { get; set; } = null!;
}
