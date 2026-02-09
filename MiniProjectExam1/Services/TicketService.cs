using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MiniProjectExam1.Entities;
using MiniProjectExam1.Models;

namespace MiniProjectExam1.Services
{
    public class TicketService
    {
        private readonly MiniProjectExam1Context _db;
        public TicketService(MiniProjectExam1Context db)
        {
            _db = db;
        }

        public async Task<PaginatedTicketResponse> GetAllAvailableTickets(TicketModel request)
        {
            var query = _db.Tickets.Where(t => t.RemainingQuota > 0).AsQueryable();

            // Filter by Category
            if (!string.IsNullOrWhiteSpace(request.CategoryTicket))
            {
                query = query.Where(t => t.CategoryTicket.Contains(request.CategoryTicket));
            }

            // Filter by Ticket Code
            if (!string.IsNullOrWhiteSpace(request.CodeTicket))
            {
                query = query.Where(t => t.CodeTicket.Contains(request.CodeTicket));
            }

            // Filter by Ticket Name
            if (!string.IsNullOrWhiteSpace(request.NameTicket))
            {
                query = query.Where(t => t.NameTicket.Contains(request.NameTicket));
            }

            // Filter by Price (price <= input)
            if (request.Price.HasValue)
            {
                query = query.Where(t => t.Price <= request.Price.Value);
            }

            // Filter by Event Date Range
            if (request.MinEventDate.HasValue)
            {
                query = query.Where(t => t.EventDate >= request.MinEventDate.Value);
            }

            if (request.MaxEventDate.HasValue)
            {
                query = query.Where(t => t.EventDate <= request.MaxEventDate.Value);
            }

            // Get total count before pagination
            var totalTickets = await query.CountAsync();

            // Apply ordering
            var orderBy = string.IsNullOrWhiteSpace(request.OrderBy) ? "CodeTicket" : request.OrderBy;
            var orderState = string.IsNullOrWhiteSpace(request.OrderState) ? "ascending" : request.OrderState.ToLower();

            query = orderBy.ToLower() switch
            {
                "categoryticket" => orderState == "descending"
                    ? query.OrderByDescending(t => t.CategoryTicket)
                    : query.OrderBy(t => t.CategoryTicket),
                "codeticket" => orderState == "descending"
                    ? query.OrderByDescending(t => t.CodeTicket)
                    : query.OrderBy(t => t.CodeTicket),
                "nameticket" => orderState == "descending"
                    ? query.OrderByDescending(t => t.NameTicket)
                    : query.OrderBy(t => t.NameTicket),
                "eventdate" => orderState == "descending"
                    ? query.OrderByDescending(t => t.EventDate)
                    : query.OrderBy(t => t.EventDate),
                "price" => orderState == "descending"
                    ? query.OrderByDescending(t => t.Price)
                    : query.OrderBy(t => t.Price),
                "remainingquota" => orderState == "descending"
                    ? query.OrderByDescending(t => t.RemainingQuota)
                    : query.OrderBy(t => t.RemainingQuota),
                _ => query.OrderBy(t => t.CodeTicket) // Default
            };

            // Apply pagination
            var page = request.Page < 1 ? 1 : request.Page;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            var tickets = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TicketModel
                {
                    CodeTicket = t.CodeTicket,
                    NameTicket = t.NameTicket,
                    CategoryTicket = t.CategoryTicket,
                    EventDate = t.EventDate,
                    Price = t.Price,
                    Quota = t.Quota,
                    RemainingQuota = t.RemainingQuota
                })
                .ToListAsync();

            return new PaginatedTicketResponse
            {
                Tickets = tickets,
                TotalTickets = totalTickets
            };
        }
    }
}
