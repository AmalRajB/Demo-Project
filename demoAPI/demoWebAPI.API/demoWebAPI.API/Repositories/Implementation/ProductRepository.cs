using demoWebAPI.API.data;
using demoWebAPI.API.model.domain;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace demoWebAPI.API.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Products> CreateProduct(Products products)
        {

            await dbContext.products.AddAsync(products);
            await dbContext.SaveChangesAsync();
            var data =  await dbContext.products.Include(x => x.category).FirstAsync(p => p.id == products.id);
            return data;

        }

        public async Task<Products?> DeleteById(Guid id)
        {
            var is_existdata = await dbContext.products.FirstOrDefaultAsync(p => p.id == id);
            if (is_existdata == null)
            {
                return null;
            }
            dbContext.products.Remove(is_existdata);
            await dbContext.SaveChangesAsync();
            return is_existdata;
        }

        public async Task<Products?> EditByid(Products products)
        {
            var isExist = await dbContext.products.FirstOrDefaultAsync(x => x.id == products.id);
            if (isExist == null)
            {
                return null;
            }
            dbContext.Entry(isExist).CurrentValues.SetValues(products);

            await dbContext.SaveChangesAsync();
            await dbContext.Entry(isExist)
            .Reference(s => s.category)
            .LoadAsync();
            return isExist;

        }

        public async Task<List<Products>> GetAll(int pageNumber, int pageSize, Guid? categoryId, string? search)
        {
            var query = dbContext.products
            .Include(p => p.category)
            .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.categoryId == categoryId);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.productName.Contains(search));
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        //public async Task<List<Products>> GetByCategory(Guid id)
        //{
        //    return await dbContext.products
        //        .Include(p => p.category)
        //        .Where(p => p.categoryId ==id)
        //        .ToListAsync();
        //}

        public async Task<Products?> GetById(Guid id)
        {
            var IsdataExist = await dbContext.products.Include(s => s.category).FirstOrDefaultAsync(p => p.id == id);
            if (IsdataExist == null)
            {
                return null;
            }
            return IsdataExist;
        }

        public async Task<int> getTotalCount(Guid? categoryId, string? search)
        {
            var query = dbContext.products.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.categoryId == categoryId);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.productName.Contains(search));
            }

            return await query.CountAsync();
        }
    }
}
