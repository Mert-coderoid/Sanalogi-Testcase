using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceManagement.Models;
using InvoiceManagement.Repositories;

namespace InvoiceManagement.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _invoiceRepository.GetAllAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(Guid id)
        {
            return await _invoiceRepository.GetByIdAsync(id);
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            await _invoiceRepository.AddAsync(invoice);
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            await _invoiceRepository.UpdateAsync(invoice);
        }

        public async Task DeleteInvoiceAsync(Guid id)
        {
            await _invoiceRepository.DeleteAsync(id);
        }
    }
}
