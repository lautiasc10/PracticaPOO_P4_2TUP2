using Core.Entities;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        public static List<BankAccount> accounts = new List<BankAccount>();

        [HttpPost]
        public ActionResult<BankAccount> CreateAccount([FromBody] BankAccountDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Owner))
                return BadRequest("El propietario de la cuenta es obligatorio.");

            if (dto.InitialBalance <= 0)
                return BadRequest("El saldo inicial debe ser mayor a 0.");

            // Creamos la entidad usando los datos del DTO
            var account = new BankAccount(dto.Owner, dto.InitialBalance);

            accounts.Add(account);

            return CreatedAtAction("GetAccountById", new { account.Id }, account);
        }

        [HttpGet("{id}")]
        public ActionResult<BankAccount> GetAccountById([FromRoute] int id)
        {
            var account = accounts.FirstOrDefault(a => a.Id == id);
            if (account == null) return NotFound($"No se encontró la cuenta con el ID {id}.");

            return account;
        }
    }
}
