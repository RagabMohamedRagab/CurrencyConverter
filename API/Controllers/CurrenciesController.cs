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
        private readonly ICurrenciesServices _currencies;

        public CurrenciesController(ICurrenciesServices currencies)
        {
            _currencies = currencies;
        }
        // api/Currencies/AddCurrency
        [HttpPost("AddCurrency")]
        public IActionResult AddCurrency([FromForm] CurrencyExchangeValueDto dto)
        {
            if (dto != null)
            {
                if (_currencies.CreateAsync(dto).Result > 0)
                    return Ok("Congratulations..");
            }
            return BadRequest("Try Again Invalid Data Or We have The Same Currency Name..!");
        }
        // api/Currencies/UpdateCurrency
        [HttpPatch("UpdateCurrency")]
        public IActionResult UpdateCurrency(int Id, [FromForm] CurrencyExchangeValueDto currencyExchange)
        {
            if (Id != 0 && currencyExchange != null)
            {

                if (_currencies.EditAsync(Id, currencyExchange).Result != null)
                    return Ok("Successfully..Update");
            }
            return BadRequest("Valid Data..!!");
        }
        // api/Currencies/DeleteCurrency
        [HttpDelete("DeleteCurrency")]
        public IActionResult DeleteCurrency(int Id)
        {
            if (Id != 0) 
            {
                if (_currencies.DeleteAsync(Id).Result > 0)

                    return Ok("Done.");
            }
            return NotFound("Failed.!");
        }
        // api/Currencies/GetCurrencyByName
        [HttpGet("GetCurrencyByName")]
        public IActionResult GetCurrencyByName(string Name)
        {
            if (!String.IsNullOrEmpty(Name))
            {
                var get_Coin = _currencies.GetByNameAsync(Name).Result;
                if (get_Coin != null)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest("Invalid Name");
        }
        // api/Currencies/GetAllCurrencies
        [HttpGet("GetAllCurrencies")]
        public IActionResult GetAllCurrencies()
        {
            var Currencies = _currencies.GetAllAsync().Result;
            if (Currencies != null)
                return Ok(Currencies);
            return NotFound("Falied..");

        }
        // api/Currencies/GetHighestNCurrencies
        [HttpGet("GetHighestNCurrencies")]
        public IActionResult GetHighestNCurrencies(int Num)
        {
            if (Num > 0)
            {
                var GetHighest = _currencies.GetHighest(Num);
                if (GetHighest != null)
                    return Ok(GetHighest);
            }
            return NotFound("Falied Your Request..!! Try again to enter Another Number.");
        }
        // api/Currencies/GetLowestNCurrencies
        [HttpGet("GetLowestNCurrencies")]
        public IActionResult GetLowestNCurrencies(int Num)
        {
            if (Num > 0)
            {
                var GetHighest = _currencies.GetLowest(Num);
                if (GetHighest != null)
                    return Ok(GetHighest);
            }
            return NotFound("Falied Your Request..!! Try again to enter Another Number.");
        }
        
        [HttpGet("ConvertAmount")]
        public IActionResult ConvertAmount([FromQuery]ConverterDto converterDto)
        {
            if (converterDto != null)
            {
                var GetAmount = _currencies.ConvertAmountAsync(converterDto).Result;
                if (GetAmount != null)
                    return Ok($"your value : {GetAmount}");
            }
            return NotFound("Falied Your Request..!! Try Again with Another ConvertAmount ");
        }


    }
}















