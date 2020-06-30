using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanProvider.Models.ViewModels
{
    public class LoanViewModel
    {
        public string ClientId { get; set; }
        public string TelephoneNumber { get; set; }
        public decimal LoanAmount { get; set; }
        public int LoanPeriod { get; set; }
        public decimal InterestRate { get; set; }
        public string PayoutDate { get; set; }
        public int InvoiceNo { get; set; }
        public string InvoiceDueDate { get; set; }
        public decimal InvoiceAmount { get; set; }
      
    }
}
