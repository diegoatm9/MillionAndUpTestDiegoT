using MillionAndUp.Data;
using MillionAndUp.Models;

namespace MillionAndUp.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly PropertyDbContext _dbContext;


        public PropertyService(PropertyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Property CreateProperty(Property property)
        {
            _dbContext.Property.Add(property);
            _dbContext.SaveChanges();
            return property;
        }

        public async Task<int> CreatePropertyImageAsync(ImageModel propertyImage)
        {
            using (var memoryStream = new MemoryStream())
            {
                await propertyImage.Image.CopyToAsync(memoryStream);
 
                var image = new PropertyImage
                {
                    Enabled = true,
                    IdProperty = propertyImage.IdProperty,
                    file = memoryStream.ToArray()
                };

                _dbContext.PropertyImage.Add(image);
                await _dbContext.SaveChangesAsync();

                return (int)image.IdPropertyImage;
            }
        }

        public List<Property> ListPropertiesWithFilters(string filterCriteria)
        {
            filterCriteria ??= "";   
            var filter = filterCriteria.Split("=", 2);
           
            switch (filter[0])
            {
                case "Price":
                  return _dbContext.Property
                 .Where(p => p.Price == Int32.Parse(filter[1]))
                 .ToList();
                case "Name":
                  return _dbContext.Property
                  .Where(p => p.Name == filter[1])
                  .ToList();
                case "Address":
                  return _dbContext.Property
                  .Where(p => p.Address == filter[1])
                  .ToList();
                case "CodeInternal":
                    return _dbContext.Property
                   .Where(p => p.CodeInternal == Int32.Parse(filter[1]))
                   .ToList();
                case "Year":
                    return _dbContext.Property
                   .Where(p => p.Year == Int32.Parse(filter[1]))
                   .ToList();
                case "IdOwner":
                    return _dbContext.Property
                   .Where(p => p.IdOwner == Int32.Parse(filter[1]))
                   .ToList();
                default:
                    return _dbContext.Property 
                   .ToList();
            }
        }

        public object GetPropertyById(int id)
        {
            return _dbContext.Property
                .Where(p => p.IdProperty == id)
                .ToList();
        }
        public object GetPropertyImageById(int id)
        {
            return _dbContext.PropertyImage
                .Where(p => p.IdPropertyImage == id)
                .ToList();
        }

        public async Task<bool> ChangePriceAsync(int propertyId, int newPrice)
        {
            var property = await _dbContext.Property.FindAsync(propertyId);

            if (property == null)
            {
                return false; 
            }

            if (property.Price != 0)
            {
                property.Price = newPrice;
            }

            await _dbContext.SaveChangesAsync();
            return true; // Actualización exitosa
        }

        public async Task<bool> UpdateProperty(int propertyId, Property updatedProperty)
        {
            var property = _dbContext.Property.Find(propertyId);


            if (property != null)
            {
                property.Address = updatedProperty.Address is null ? property.Address : updatedProperty.Address;
                property.Price = updatedProperty.Price is null ? property.Price : updatedProperty.Price;
                property.Name = updatedProperty.Name is null ? property.Name : updatedProperty.Name;
                property.Year = updatedProperty.Year is null ? property.Year : updatedProperty.Year;
                property.CodeInternal = updatedProperty.CodeInternal is null ? property.CodeInternal : updatedProperty.CodeInternal;
                property.IdOwner = updatedProperty.IdOwner is null? property.IdOwner : updatedProperty.IdOwner;

                await _dbContext.SaveChangesAsync();
                return true; 
            }
            else
            {
                return false;
            }
        }
    }
}
