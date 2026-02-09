using System;
using System.Collections.Generic;

namespace MiniProjectExam1.Entities;

public partial class BookedTicket
{
    public Guid BookedTicketId { get; set; }

    public DateTime BookingDate { get; set; }

    public virtual ICollection<BookedTicketDetail> BookedTicketDetails { get; set; } = new List<BookedTicketDetail>();
}
