using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data.Models.ProductModel;
using PublishingHouse.Data.Models.ProductModel.ProductHelperEntities;
using PublishingHouse.Data.Services.ProductServices;
using PublishingHouse.Data.View_Models;
using PublishingHouse.Filters;

namespace PublishingHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        #region CreateProductEndPoint
        [HttpPost]
        [AuthorizationFilter]
        [Route(nameof(CreateProductResource))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>CreateProductResource(ProductVM product)
        {
            try
            {
                var productResult = await _service.CreateProduct(product);
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
        #region CreateProductTypeResource
        [HttpPost]
        [AuthorizationFilter]
        [Route(nameof(CreateProductTypeResource))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult>CreateProductTypeResource(string type)
        {
            try
            {
                var resultType = await _service.CreateProductType(type);
                return Created(String.Empty, resultType);
            }
            catch (InvalidDataException e)
            {
                return Conflict(e.Message);
            }
            catch (DbUpdateException e)
            {
                return Conflict(e.Message);
            }
        }

        #endregion
        
    }
}