using System;
using System.Collections.Generic;

namespace LoanProviderApi.Models
{
    public partial class Loan
    {
        public Loan()
        {
            Invoices = new HashSet<Invoices>();
        }

        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int LoanPeriod { get; set; }
        public DateTime PayoutDate { get; set; }
        public string ClientUniqueId { get; set; }

        public virtual Client ClientUnique { get; set; }
        public virtual ICollection<Invoices> Invoices { get; set; }
    }
}
