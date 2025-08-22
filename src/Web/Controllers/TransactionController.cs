using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpPost("{accountNumber}/deposit")]
        public ActionResult Deposit([FromRoute] string accountNumber,[FromQuery] decimal amount,[FromQuery] string? note = "")
        {
            var account = BankAccountController.accounts.FirstOrDefault(a => a.Number == accountNumber);

            if (account == null)
                return NotFound($"No se encontró la cuenta con número {accountNumber}.");

            try
            {
                account.MakeDeposit(amount, DateTime.Now, note ?? "Depósito");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new{message = "Depósito realizado con éxito", balance = account.Balance});
        }

        [HttpPost("{accountNumber}/withdraw")]
        public ActionResult Withdraw([FromRoute] string accountNumber,[FromQuery] decimal amount,[FromQuery] string? note = "")
        {
            var account = BankAccountController.accounts.FirstOrDefault(a => a.Number == accountNumber);

            if (account == null)
                return NotFound($"No se encontró la cuenta con número {accountNumber}.");

            try
            {
                account.MakeWithdrawal(amount, DateTime.Now, note ?? "Retiro");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new{message = "Retiro realizado con éxito",balance = account.Balance});
        }

        [HttpGet("{accountNumber}/balance")]
        public ActionResult GetBalance([FromRoute] string accountNumber)
        {
            var account = BankAccountController.accounts.FirstOrDefault(a => a.Number == accountNumber);

            if (account == null)
                return NotFound($"No se encontró la cuenta con número {accountNumber}.");

            return Ok(new { balance = account.Balance });
        }
    }
}
