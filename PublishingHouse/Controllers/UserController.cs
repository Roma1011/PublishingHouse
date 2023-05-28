using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data.Models;
using PublishingHouse.Data.Services.UserServices;
using PublishingHouse.Data.View_Models;
using PublishingHouse.Filters;

namespace PublishingHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        { 
            this._userService = userService;
        }
        #region RegistrationEndPoint
        [AuthorizationFilter]
        [HttpPost]
        [Route(nameof(Register))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RegistrationUserVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Register(RegistrationUserVm user)
        {
            try
            {
                var userResult=await _userService.Registration(user);
                return Created(string.Empty, userResult);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidDataException ex)
            {
                return Conflict(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
        #region LoginEndPoint
        [HttpPost]
        [Route(nameof(Login))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginUserVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Login(LoginUserVm user)
        {
            try
            {
                var result = await _userService.LogIn(user);
                return Ok(new
                {
                    Result = result,
                    tokenExpiryTime = DateTime.Now.AddDays(1)
                });
            }

            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}
