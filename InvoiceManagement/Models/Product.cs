using System;

namespace InvoiceManagement.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
