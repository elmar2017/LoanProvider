using System;
using System.Collections.Generic;

namespace LoanProviderApi.Models
{
    public partial class Client
    {
        public Client()
        {
            Loan = new HashSet<Loan>();
        }

        public string ClientUniqueId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TelephoneNr { get; set; }

        public virtual ICollection<Loan> Loan { get; set; }
    }
}
