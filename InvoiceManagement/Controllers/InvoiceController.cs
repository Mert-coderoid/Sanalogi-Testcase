using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Models;
using InvoiceManagement.Services;
using FluentValidation;
using FluentValidation.Results;

namespace InvoiceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IValidator<Invoice> _invoiceValidator;

        public InvoiceController(IInvoiceService invoiceService, IValidator<Invoice> invoiceValidator)
        {
            _invoiceService = invoiceService;
            _invoiceValidator = invoiceValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoiceById(Guid id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<ActionResult> CreateInvoice(Invoice invoice)
        {
            ValidationResult result = await _invoiceValidator.ValidateAsync(invoice);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            await _invoiceService.AddInvoiceAsync(invoice);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice.Id }, invoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(Guid id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }
            ValidationResult result = await _invoiceValidator.ValidateAsync(invoice);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            await _invoiceService.UpdateInvoiceAsync(invoice);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            await _invoiceService.DeleteInvoiceAsync(id);
            return NoContent();
        }
    }
}
