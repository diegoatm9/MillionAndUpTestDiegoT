using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MillionAndUp.Models;
using MillionAndUp.Services;

namespace MillionAndUp.Controllers
{
    //DTRUJILLO - Add Authorization to controller (This can be changed if we have some roles already created)
    [Authorize(Policy = "AllowEveryone")]
    [ApiController]
    [Route("api/properties")]
    public class PropertyController : ControllerBase
    {

        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpPost]
        public ActionResult<Property> CreateProperty([FromBody] Property property)
        {
            var createdProperty = _propertyService.CreateProperty(property);
            return CreatedAtAction(nameof(GetProperty), new { id = createdProperty.IdProperty }, createdProperty);
        }

        [HttpPost("Image")]
        public async Task<IActionResult> UploadImage([FromForm] ImageModel imageModel)
        {
            try
            {
                var imageId = await _propertyService.CreatePropertyImageAsync(imageModel);

                return Ok(new { ImageId = imageId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPatch("{propertyId}/price")]
        public async Task<IActionResult> ChangePriceAsync(int propertyId, int newPrice)
        {
          
            try
            {
                var result = await _propertyService.ChangePriceAsync(propertyId, newPrice);

                if (result)
                {
                    return Ok("Property updated successfully");
                }
                else
                {
                    return NotFound("Property not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{propertyId}/UpdateProperty")]
        public async Task<IActionResult> UpdatePropertyAsync(int propertyId, Property updatedProperty)
        {
            try
            {
                var result = await _propertyService.UpdateProperty(propertyId, updatedProperty);

                if (result)
                {
                    return Ok("Property updated successfully");
                }
                else
                {
                    return NotFound("Property not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public ActionResult<List<Property>> ListPropertiesWithFilters([FromQuery] string? filterCriteria)
        {
            var properties = _propertyService.ListPropertiesWithFilters(filterCriteria);
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public ActionResult<Property> GetProperty(int id)
        {
            var property = _propertyService.GetPropertyById(id);
            if (property == null)
            {
                return NotFound();
            }

            return Ok(property);
        }
        [HttpGet("{id}/propertyImage")]
        public ActionResult<PropertyImage> GetPropertyImage(int id)
        {
            var propertyImage = _propertyService.GetPropertyImageById(id);
            if (propertyImage == null)
            {
                return NotFound();
            }

            return Ok(propertyImage);
        }
    }
}
