using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceManagement.Models;

namespace InvoiceManagement.Services
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<Invoice> GetInvoiceByIdAsync(Guid id);
        Task AddInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(Guid id);
    }
}
