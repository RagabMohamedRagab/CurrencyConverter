using BLL.Dtos;
using BOL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrenciesServices<CurrencyDto> _currencies;

        public CurrenciesController(ICurrenciesServices<CurrencyDto> currencies)
        {
            _currencies = currencies;
        }
        // api/Currencies/GetAllCurrencies
        [HttpGet("GetAllCurrencies")]
        public IActionResult GetAllCurrencies()
        {
            return Ok(_currencies.GetAllAsync().Result);
        }
        // api/Currencies/GetCurrencyByName
        [HttpGet("GetCurrencyByName")]
        public IActionResult GetCurrencyByName(string Name)
        {
            if (!String.IsNullOrEmpty(Name))
            {
                return Ok(_currencies.GetByNameAsync(Name).Result);
            }
            return BadRequest("Invalid Name");
        }
        // api/Currencies/AddCurrency
        [HttpPost("AddCurrency")]
        public IActionResult AddCurrency([FromForm]CurrencyDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid Request");
            }
            if (_currencies.CreateAsync(dto).Result > 0)
            {
                return Ok("Congratulation");
            }
            return NotFound("Try Again ...!");
        }
        // api/Currencies/DeleteCurrency
        [HttpDelete("DeleteCurrency")]
        public IActionResult DeleteCurrency(string Name)
        {
            if (Name == null)
            {
                return BadRequest();
            }
            var get_currency = _currencies.GetByNameAsync(Name).Result;

            if (_currencies.DeleteAsync(get_currency).Result > 0)
            {
                return Ok("Done.");
            }
            return NotFound();
        }
    }
}


