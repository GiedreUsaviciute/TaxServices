using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxAPI.Data;
using TaxAPI.Models;

namespace TaxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxRepository _repository;

        public TaxController(ITaxRepository taxRepository)
        {
            _repository = taxRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(MunicipalityTax), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var taxes = await _repository.AllAsync();

            MunicipalityTax a = new MunicipalityTax { Id = Guid.NewGuid(), Name = "Vilnius", Daily = new DailyTax { Value = 0.2, Dates = new List<DateTime> { DateTime.Parse("2020.01.01") } } };

            await _repository.AddAsync(a);

            if (taxes == null || !taxes.Any())
            {
                return NotFound("No tax data found");
            }

            return Ok(taxes);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] MunicipalityTaxBody tax)
        {
            if (tax == null)
            {
                return BadRequest("No tax data provided");
            }

            try
            {
                var entity = RemapEntity(tax);
                await _repository.AddAsync(entity);

                return Ok(tax);
            }
            catch (Exception e)
            {
                return BadRequest($"Invalid Request Body Model. Error: {e.Message}");
            }
        }

        //upload a json file and import data from it
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFromUpload(FileData file)
        {
            if (file == null || file.Data == null)
            {
                return BadRequest("No file data provided");
            }

            try
            {
                var tasks = file.Data.ToList().Select(async item =>
                {
                    var entity = RemapEntity(item);
                    await _repository.AddAsync(entity);
                });
              
                var allTasks = Task.WhenAll(tasks);

                await allTasks;

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Data import failed. Error: {e.Message}");
            }
        }

        [HttpPost("find")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByNameAndDate([FromBody] TaxRequestBody body)
        {
            if (body == null)
            {
                return BadRequest("Request body cannot be empty");
            }

            if (string.IsNullOrEmpty(body.Municipality) || body.Date == null)
            {
                return BadRequest("Invalid request data");
            }

            var tax = await _repository.FirstOrDefault(body.Municipality);

            if (tax == null)
            {
                return NotFound($"Tax entry for {body.Municipality} not found");
            }

            try
            {
                if (DateTime.TryParse(body.Date, out DateTime reqDate))
                {
                    var dateFound = false;

                    if (tax.Daily?.Dates?.ToList().Count > 0)
                    {
                        dateFound = tax.Daily.Dates.ToList().Any(d => d == reqDate);

                        if (dateFound) return Ok(tax.Daily.Value);
                    }

                    if (tax.Weekly != null)
                    {
                        dateFound = ValidateTimePeriod(reqDate, tax.Weekly.StartDate, tax.Weekly.EndDate);
                        if (dateFound) return Ok(tax.Weekly.Value);
                    }

                    if (tax.Monthly != null)
                    {
                        dateFound = ValidateTimePeriod(reqDate, tax.Monthly.StartDate, tax.Monthly.EndDate);
                        if (dateFound) return Ok(tax.Monthly.Value);
                    }

                    if (tax.Yearly != null)
                    {
                        dateFound = ValidateTimePeriod(reqDate, tax.Yearly.StartDate, tax.Yearly.EndDate);
                        if (dateFound) return Ok(tax.Yearly.Value);
                    }

                    return NotFound($"Could not find a tax value for Municipality {body.Municipality} and date: {body.Date}");
                }
                else
                {
                    return BadRequest($"Invalid date: {body.Date}");
                }
            }
            catch (Exception e)
            {
                return NotFound($"Could not find an entry for Municipality {body.Municipality} and date: {body.Date}");
            }
        }

        private bool ValidateTimePeriod(DateTime date, DateTime startDate, DateTime endDate)
        {
            return date >= startDate && date <= endDate;
        }

        private MunicipalityTax RemapEntity(MunicipalityTaxBody body)
        {
            var entity = new MunicipalityTax
            {
                Id = Guid.NewGuid(),
                Name = body.Name
            };

            if (body.Weekly != null)
            {
                entity.Weekly = new PeriodicTax { Value = body.Weekly.Value, StartDate = DateTime.Parse(body.Weekly.StartDate), EndDate = DateTime.Parse(body.Weekly.EndDate) };
            }

            if (body.Monthly != null)
            {
                entity.Monthly = new PeriodicTax { Value = body.Monthly.Value, StartDate = DateTime.Parse(body.Monthly.StartDate), EndDate = DateTime.Parse(body.Monthly.EndDate) };
            }

            if (body.Yearly != null)
            {
                entity.Yearly = new PeriodicTax { Value = body.Yearly.Value, StartDate = DateTime.Parse(body.Yearly.StartDate), EndDate = DateTime.Parse(body.Yearly.EndDate) };
            }

            if (body.Daily != null)
            {
                var listOfDates = body.Daily.Dates.ToList().Select(d => DateTime.Parse(d));

                entity.Daily = new DailyTax { Value = body.Daily.Value, Dates = listOfDates };
            }

            return entity;
        }
    }
}