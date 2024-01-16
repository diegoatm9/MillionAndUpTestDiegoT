
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MillionAndUp.Data;
using MillionAndUp.Models;
using MillionAndUp.Services;

namespace TestMillionAndUp
{
    public class Tests
    {
        private readonly DbContextOptions<PropertyDbContext> _dbContext;

        public Tests()
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            // Puedes acceder a la cadena de conexión así:
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // DTRUJILLO - Add dbContext to test all scenarios (TODO: Create an interface for the DbContext and inject dependency)
            _dbContext = new DbContextOptionsBuilder<PropertyDbContext>()
            .UseSqlServer(connectionString)
            .Options;
        }
        [Test]
        public void CreateProperty_OK()
        {
            // Arrange

            using (var context = new PropertyDbContext(_dbContext))
            {
                
                var propertyService = new PropertyService(context);

                //// Act
                var propertyData = new Property()
                {
                    IdProperty = 6,
                    Address = "TestAdress6",
                    CodeInternal = 6,
                    IdOwner = 1,
                    Name = "CASA06",
                    Price = 1200000,
                    Year = 2010
                };
                var createdProperty = propertyService.CreateProperty(propertyData);

                // Assert
                Assert.IsNotNull(createdProperty);
            }
        }
        [Test]
        public void GetPropertyById_OK()
        {
            // Arrange

            using (var context = new PropertyDbContext(_dbContext))
            {
               
                var propertyService = new PropertyService(context);

                //// Act
                var propertyData = 3;
                var createdProperty = propertyService.GetPropertyById(propertyData);

                // Assert
                Assert.IsNotNull(createdProperty);
            }
        }
        [Test]
        public void ListPropertiesWithFilters_OK()
        {
            // Arrange

            using (var context = new PropertyDbContext(_dbContext))
            {
                var propertyService = new PropertyService(context);

                //// Act
                var propertyData = "";
                var createdProperty = propertyService.ListPropertiesWithFilters(propertyData);

                // Assert
                Assert.IsNotNull(createdProperty);
            }
        }
        [Test]
        public async Task ChangePrice_OKAsync()
        {
            // Arrange

            using (var context = new PropertyDbContext(_dbContext))
            {
                var propertyService = new PropertyService(context);

                //// Act
                var propertyId = 2;
                var newPrice = 1100000;
                var createdProperty = await propertyService.ChangePriceAsync(propertyId, newPrice);

                // Assert
                Assert.IsNotNull(createdProperty);
            }
        }
        [Test]
        public async Task UpdateProperty_OKAsync()
        {
            // Arrange

            using (var context = new PropertyDbContext(_dbContext))
            {
                var propertyService = new PropertyService(context);

                //// Act
                var propertyData = new Property()
                {
                    IdProperty = 2,
                    Address = "TestAdress3",
                    CodeInternal = 3,
                    IdOwner = 1,
                    Name = "CASA03",
                    Price = 1000000,
                    Year = 2023
                };
                var createdProperty = await propertyService.UpdateProperty(propertyData.IdProperty, propertyData);

                // Assert
                Assert.IsNotNull(createdProperty);
            }
        }
        [Test]
        public async Task CreatePropertyImageAsync_OKAsync()
        {
            // Arrange

            using (var context = new PropertyDbContext(_dbContext))
            {
                var propertyService = new PropertyService(context);

                //// Act
                var imageModelData = new ImageModel()
                {
                    IdProperty = 2,
                    Image = null
                };
                var createdProperty = await propertyService.CreatePropertyImageAsync(imageModelData);

                // Assert
                Assert.IsNotNull(createdProperty);
            }
        }
    }
}