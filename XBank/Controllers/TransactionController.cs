using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok();
        }

        [HttpPost("{cpf}")]
        public async Task<IActionResult> Post(string cpf, [FromBody] string value)
        {
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Ok();
        }
    }
}
