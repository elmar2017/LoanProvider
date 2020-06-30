using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanProvider.Models.Dto
{
    public class LoanDto
    {
        public string LoanId { get; set; }
        public string LoanAmount { get; set; }
        public string LoanPeriod { get; set; }
        public string MonthlyIntersetRate { get; set; }
        public string PayoutDate { get; set; }
        public string InvoiceOrderNo { get; set; }
        public string InvoiceCount { get; set; }
    }
}
