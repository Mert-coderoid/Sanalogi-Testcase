using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceManagement.Models;

namespace InvoiceManagement.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();

        public async Task<IEnumerable<Product>> GetAllByInvoiceIdAsync(Guid invoiceId)
        {
            return await Task.FromResult(_products.Where(p => p.InvoiceId == invoiceId));
        }
    }
}
