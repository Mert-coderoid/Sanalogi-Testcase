using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceManagement.Models;

namespace InvoiceManagement.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly List<Invoice> _invoices = new List<Invoice>();

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await Task.FromResult(_invoices);
        }

        public async Task<Invoice> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_invoices.FirstOrDefault(i => i.Id == id));
        }

        public async Task AddAsync(Invoice invoice)
        {
            invoice.Id = Guid.NewGuid();
            foreach (var product in invoice.Products)
            {
                product.Id = Guid.NewGuid();
                product.InvoiceId = invoice.Id;
            }
            _invoices.Add(invoice);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            var existingInvoice = _invoices.FirstOrDefault(i => i.Id == invoice.Id);
            if (existingInvoice != null)
            {
                existingInvoice.Date = invoice.Date;
                existingInvoice.TotalAmount = invoice.TotalAmount;
                existingInvoice.Products = invoice.Products;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var invoice = _invoices.FirstOrDefault(i => i.Id == id);
            if (invoice != null)
            {
                _invoices.Remove(invoice);
            }
            await Task.CompletedTask;
        }
    }
}
