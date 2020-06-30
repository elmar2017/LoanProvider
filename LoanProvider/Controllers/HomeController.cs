using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoanProvider.Models;
using LoanProvider.Models.Dto;
using LoanProvider.Models.ViewModels;

namespace LoanProvider.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (LoanDbContext db = new LoanDbContext())
            {
                List<Loan> loans = db.Loan.Select(x => new Loan
                {
                    Id = x.Id,
                    ClientUnique = new Client { Name = x.ClientUnique.Name, Surname = x.ClientUnique.Surname, ClientUniqueId = x.ClientUniqueId },
                    Amount = x.Amount,
                    PayoutDate = x.PayoutDate
                }).ToList();


                ViewBag.Clients = db.Client.ToList();

                return View(loans);
            }
        }

        [HttpGet]
        public IActionResult GetLoanDetails(int loanId)
        {
            using (LoanDbContext db = new LoanDbContext())
            {
                Loan loan = db.Loan.FirstOrDefault(x => x.Id == loanId);
                db.Entry(loan).Reference(p => p.ClientUnique).Load();
                db.Entry(loan).Collection(x => x.Invoices).Load();
                ViewBag.Clients = db.Client.ToList();
                return View(loan);
            }
        }

        [HttpPost]
        public IActionResult CreateNewLoan(LoanViewModel model)
        {
            using (LoanDbContext db = new LoanDbContext())
            {
                Loan loan = new Loan()
                {
                    Amount = model.LoanAmount,
                    ClientUniqueId = model.ClientId,
                    InterestRate = model.InterestRate,
                    LoanPeriod = model.LoanPeriod,
                    PayoutDate = DateTime.Parse(model.PayoutDate)
                };

                loan.Invoices.Add(new Invoices
                {
                    Amount = model.InvoiceAmount,
                    DueDate = DateTime.Parse(model.InvoiceDueDate)

                });

                db.Loan.Add(loan);
                db.SaveChanges();


                return RedirectToAction("Index");
            }
        }




        [HttpPost]
        public PartialViewResult CalculateInvoice([FromBody]LoanDto loan)
        {
            int loanPeriod = int.Parse(loan.LoanPeriod);
            int orderNo = int.Parse(loan.InvoiceOrderNo);
            decimal loanAmount = decimal.Parse(loan.LoanAmount);
            int loanId = int.Parse(loan.LoanId);
            decimal monthlyIntersetRate = decimal.Parse(loan.MonthlyIntersetRate);

            using (LoanDbContext db = new LoanDbContext())
            {
                Invoices invoce = new Invoices();
                decimal unpaidAmount = ((loanPeriod - orderNo + 1) * loanAmount) / orderNo;
                decimal invoiceAmount = (loanAmount) / (loanPeriod) + (unpaidAmount * monthlyIntersetRate);
                DateTime invoiceDueDate;

                if (loan.InvoiceCount == "0") // eger 1-cidirse
                {
                    DateTime dateTime = DateTime.ParseExact(loan.PayoutDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                    invoiceDueDate = dateTime.AddMonths(1);
                }
                else
                {

                    invoce = db.Invoices.Where(x => x.LoanId == loanId).OrderByDescending(x => x.DueDate).FirstOrDefault();

                    invoiceDueDate = invoce.DueDate.AddMonths(1);

                }

                InvoiceViewModel invoiceViewModel = new InvoiceViewModel
                {
                    Amount = Math.Round(invoiceAmount, 2).ToString(),
                    DueDate = invoiceDueDate.ToString(),
                    InvoiceNo = loanId == 0 ? "1" : (invoce.OrderNr + 1).ToString()
                };

                return PartialView(invoiceViewModel);
            }
        }








        [HttpGet]
        public IActionResult IssueNewLoan()
        {
            using (LoanDbContext db = new LoanDbContext())
            {
                Loan loan = new Loan
                {
                    ClientUnique = new Client()
                };
                ViewBag.Clients = db.Client.ToList();
                return View("GetLoanDetails", loan);
            }

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
