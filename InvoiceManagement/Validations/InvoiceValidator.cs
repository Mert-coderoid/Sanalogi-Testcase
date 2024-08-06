using FluentValidation;
using InvoiceManagement.Models;
using System.Linq;

namespace InvoiceManagement.Validations
{
    public class InvoiceValidator : AbstractValidator<Invoice>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required.");
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("TotalAmount must be greater than 0.");
            RuleForEach(x => x.Products).SetValidator(new ProductValidator());

            RuleFor(x => x)
                .Must(InvoiceTotalMatchesProductsTotal)
                .WithMessage("TotalAmount must be equal to the sum of all product prices.");
        }

        private bool InvoiceTotalMatchesProductsTotal(Invoice invoice)
        {
            return invoice.TotalAmount == invoice.Products.Sum(p => p.Price);
        }
    }
}
