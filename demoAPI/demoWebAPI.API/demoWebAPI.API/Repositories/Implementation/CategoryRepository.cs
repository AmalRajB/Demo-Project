using demoWebAPI.API.data;
using demoWebAPI.API.model.domain;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace demoWebAPI.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<productCategory> CreateAsync(productCategory category)
        {
            await dbContext.productCategories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
            
        }

        public async Task<productCategory?> DeleteById(Guid id)
        {
            var isExist = await dbContext.productCategories.FirstOrDefaultAsync(x => x.id == id);
            if (isExist == null)
            {
                return null;
            }
            dbContext.productCategories.Remove(isExist);
            await dbContext.SaveChangesAsync();
            return isExist;
        }

        public async Task<IEnumerable<productCategory>> GetAllData()
        {
            return await dbContext.productCategories.ToListAsync();
            
        }

        public async Task<productCategory> GetById(Guid id)
        {
            return await dbContext.productCategories.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<bool> isExist(Guid id)
        {
            return await dbContext.productCategories.AnyAsync(c=>c.id == id);
        }

        public async Task<productCategory?> UpdateCategory(productCategory category)
        {
            var isExist = await dbContext.productCategories.FirstOrDefaultAsync(x => x.id == category.id);
            if (isExist == null)
            {
                return null;
            }
            dbContext.Entry(isExist).CurrentValues.SetValues(category);
            await dbContext.SaveChangesAsync();
            return category;

        }
    }
}
