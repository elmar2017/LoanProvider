using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanProvider.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public string InvoiceNo { get; set; }
        public string DueDate { get; set; }
        public string Amount { get; set; }
    }
}
