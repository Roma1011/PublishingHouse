using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data.Models;
using PublishingHouse.Data.Services.ContryHandBookService;
using PublishingHouse.Data.View_Models;
using PublishingHouse.Filters;

namespace PublishingHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryHandBookController : ControllerBase
    {
        private readonly ICountryHandBookService _handBookService;
        public CountryHandBookController(ICountryHandBookService handBookService)
        {
            this._handBookService= handBookService;
        }
        #region AddCountryEndpoint
        [AuthorizationFilter]
        [HttpPost]
        [Route(nameof(AddContry))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CountryHandBookVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddContry(CountryHandBookVm country)
        {
            try
            {
                await _handBookService.CreateCountry(country);
                return Created(string.Empty, country);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        #endregion
    }
}
