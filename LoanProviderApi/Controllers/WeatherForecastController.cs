using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanProviderApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoanProviderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        
        [HttpGet]
        public ActionResult<Loan> Get()
        {
            using (LoanDbContext db = new LoanDbContext())
            {
                List<Loan> loans = db.Loan.ToList();

                return Ok(loans);
            };
        }
    }
}
