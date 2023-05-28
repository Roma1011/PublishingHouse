using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data.Models.ProductModel.ProductHelperEntities;
using PublishingHouse.Data.Services.PublisherHousServices;
using PublishingHouse.Data.View_Models;
using PublishingHouse.Filters;

namespace PublishingHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherHouseHandBookController:ControllerBase
    {
        private readonly IPublisherHouseService _service;
        public PublisherHouseHandBookController(IPublisherHouseService service)
        {
            _service = service;
        }
        #region CreatePublisherHouseEndPoint
        [HttpPost]
        [AuthorizationFilter]
        [Route(nameof(CreateHandBookPublisherHouseResource))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>CreateHandBookPublisherHouseResource(PublishingHouseHandBookVM publishingHouseHandBookVM)
        {
            try
            {
                var productResult = await _service.AddPublishingHouse(publishingHouseHandBookVM);
                return Created(String.Empty, productResult);
            }
            catch (InvalidDataException e)
            {
                return Conflict(e.Message);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}
