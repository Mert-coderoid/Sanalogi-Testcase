using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceManagement.Models;

namespace InvoiceManagement.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllByInvoiceIdAsync(Guid invoiceId);
    }
}
