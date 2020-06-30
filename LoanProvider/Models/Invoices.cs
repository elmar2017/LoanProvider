using System;
using System.Collections.Generic;

namespace LoanProvider.Models
{
    public partial class Invoices
    {
        public int InvoiceNr { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public int OrderNr { get; set; }
        public int LoanId { get; set; }

        public virtual Loan Loan { get; set; }
    }
}
