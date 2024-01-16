using Microsoft.AspNetCore.Mvc;
using MillionAndUp.Models;
using System.Net.Mime;

namespace MillionAndUp.Services
{
    public interface IPropertyService
    {
        Task<bool> ChangePriceAsync(int propertyId, int newPrice);
        Property CreateProperty(Property property);
        List<Property> ListPropertiesWithFilters(string filterCriteria);
        Task<bool> UpdateProperty(int propertyId, Property updatedProperty);
        object GetPropertyById(int id);
        Task<int> CreatePropertyImageAsync(ImageModel propertyImage);
        object GetPropertyImageById(int id);
    }
}