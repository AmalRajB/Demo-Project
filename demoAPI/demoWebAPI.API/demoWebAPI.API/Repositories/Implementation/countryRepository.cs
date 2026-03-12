using Azure.Core;
using demoWebAPI.API.data;
using demoWebAPI.API.model.domain;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace demoWebAPI.API.Repositories.Implementation
{
    
    public class countryRepository : IcountryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public countryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Country?> checkCountry(string countryName)
        {
            var normalizedName = countryName.Trim().ToLower();
            return await dbContext.Countries
            .FirstOrDefaultAsync(x =>
            x.CountryName.Trim().ToLower() == normalizedName);
        }

        public async Task<Country> CreateAsync(Country country)
        {
            await dbContext.Countries.AddAsync(country);
            await dbContext.SaveChangesAsync();
            return country;
        }

        public async Task<Country?> DeleteById(Guid id)
        {
            var Existingcountry = await dbContext.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (Existingcountry == null)
            {
                return null;
            }
            dbContext.Countries.Remove(Existingcountry);
            await dbContext.SaveChangesAsync();
            return (Existingcountry);
        }

        public async Task<Country?> EditbyId(Country country)
        {
            var ExistingCountry = await dbContext.Countries.FirstOrDefaultAsync(x => x.Id == country.Id);
            if(ExistingCountry != null)
            {
                dbContext.Entry(ExistingCountry).CurrentValues.SetValues(country);
                await dbContext.SaveChangesAsync();
                return ExistingCountry;

            }
            return null;

        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await dbContext.Countries.AnyAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return await dbContext.Countries.ToListAsync();
        }

        public async Task<Country?> GetById(Guid id)
        {
            return await dbContext.Countries.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
