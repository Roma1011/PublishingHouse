using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data.Services.AuthorServices;
using PublishingHouse.Data.View_Models;
using PublishingHouse.Filters;

namespace PublishingHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorContoller : ControllerBase
    {
        private readonly IAuthorService _authorServiceService;
        public AuthorContoller(IAuthorService authorServiceService) 
        { 
            this._authorServiceService = authorServiceService;
        }
        #region GetAllAuthorEndpoint
        [HttpGet]
        [Route(nameof(GetAllAuthor))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAuthor([Required]short page)
        {
            try
            {
                var result= await _authorServiceService.GetAllAuthor(page);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion
        #region GetAuthorEndpoint
        [HttpGet]
        [Route(nameof(GetAuthorById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAuthorById([Required] long id)
        {
            try
            {
                var result= await _authorServiceService.GetAuthorById(id);
                return Ok(result);
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion
        #region AddAuthorInHandBookEndbook
        [AuthorizationFilter]
        [HttpPost]
        [Route(nameof(AddAuthorInHandBook))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAuthorInHandBook(AuthorVm author)
        {
            try
            {
                var result = await _authorServiceService.AddAuthor(author);
                return Created(string.Empty, result);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (InvalidDataException ex)
            {
                return Conflict(ex.Message);
            }
        }
        #endregion
        #region AddABookForAuthorEndpoint
        [HttpPost]
        [AuthorizationFilter]
        [Route(nameof(AddABookForAuthor))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddABookForAuthor([Required]string authorPersonalNumber,[Required]string isbn)
        {
            try
            {
                await _authorServiceService.AddBookForAuthor(authorPersonalNumber, isbn);
                return Created(string.Empty, "Resource Created Successful");
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.Message);

            }
            catch (InvalidDataContractException ex)
            {
                return Conflict(ex.Message);
            }
        }
        #endregion
        #region AuthorEditEndpoint
        [HttpPut]
        [AuthorizationFilter]
        [Route(nameof(EditAuthor))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditAuthor([Required] long id, AuthorVm author)
        {
            try
            {
                var item = await _authorServiceService.EditAuthor(id, author);
                return Created(string.Empty, item);
            }
            catch (DbUpdateException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
            
        #endregion
        #region RemoveBookForAuthorEndpoint
        [HttpDelete]
        [AuthorizationFilter]
        [Route(nameof(RemoveBookForAuthor))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveBookForAuthor([Required]string authorPersonalNumber,[Required]string isbn)
        {
            try
            {
                await _authorServiceService.DeleteBookForAuthor(authorPersonalNumber, isbn);
                return NoContent();
            }
            catch (InvalidDataException e)
            {
                return Conflict(e.Message);

            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
        }
        #endregion
        #region DeleteAuthorEndpoint
        [HttpDelete]
        [AuthorizationFilter]
        [Route(nameof(DeleteAuthorById))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAuthorById([Required]long id)
        {
            try
            {
                await _authorServiceService.DeleteAuthorById(id);
                return NoContent();
            }
            catch (InvalidDataException  e)
            {
                return NotFound(e.Message);
            }
        }
        #endregion
    }
}
