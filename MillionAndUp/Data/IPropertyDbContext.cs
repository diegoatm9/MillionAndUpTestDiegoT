using Microsoft.EntityFrameworkCore;
using MillionAndUp.Models;

namespace MillionAndUp.Data
{
    public interface IPropertyDbContext
    {
        DbSet<Owner> Owner { get; set; }
        DbSet<Property> Property { get; set; }
        DbSet<PropertyImage> PropertyImage { get; set; }
        DbSet<PropertyTrace> PropertyTrace { get; set; }
        int SaveChanges(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}