using System;
using System.Collections.Generic;

namespace InvoiceManagement.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
