using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data.Services;
using PublishingHouse.Data.View_Models;
using PublishingHouse.Filters;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenderController : ControllerBase
{
    private readonly IGenderService _genderService;
    public GenderController(IGenderService genderService)
    {
        _genderService= genderService;
    }
    /// <summary>
    /// სქესის ენდპოინთში შესაძლებელია ნებისმიერი სქესის დამატება რადგან ვფიქრობ
    /// რომ აბსტრაქციისთის შესაძლებელია ეს ენდფოინთი გამოადგეს დეველოპერს გაფართოებისთვის
    /// აქ არ მოქმედებს მხოლოდ male female 
    /// OCP აბსტრაქცია
    /// </summary>
    #region CreateGenderResourceEndpoint
    //[AuthorizationFilter]
    [HttpPost]
    [Route(nameof(CreateGenderResource))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CountryHandBookVm))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateGenderResource([Required]string sex)
    {
        try
        {
            await _genderService.AddGender(sex);
            return Created(string.Empty,"The sex successfully created");
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
}