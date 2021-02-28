using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using TechnicalTest.Models;

namespace TechnicalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Patient : ControllerBase
    {

        [HttpGet]
        public IActionResult Index()
        {
            return Ok($"Welcome to Medi-Map Technical Test!");
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<PatientDetails> value)
        {
            ProcPatientInfo procpatient = new ProcPatientInfo();
            string result = procpatient.ProcessingPatientInfo(value);
            return Ok(result);      
        }
    }
}
