using demoWebAPI.API.data;
using demoWebAPI.API.model.domain;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace demoWebAPI.API.Repositories.Implementation
{


    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext dbContext;

        public StateRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<State> CreateAsync(State state)
        {
            
            await dbContext.States.AddAsync(state);
            await dbContext.SaveChangesAsync();
            return await dbContext.States.Include(s => s.Country).FirstAsync(s => s.Id == state.Id);
            

        }

        public async Task<State?> DeleteById(Guid id)
        {
            var isExist = await dbContext.States.FirstOrDefaultAsync(x => x.Id == id);
            if(isExist == null)
            {
                return null;
            }
            dbContext.States.Remove(isExist);
            await dbContext.SaveChangesAsync();
            return isExist;

        }

        public async Task<State?> EditStateById(State state)
        {
            var isExist = await dbContext.States.FirstOrDefaultAsync(x => x.Id == state.Id);
            if(isExist == null)
            {
                return null;
            }
            dbContext.Entry(isExist).CurrentValues.SetValues(state);

            //update the category 
            
            await dbContext.SaveChangesAsync();
            await dbContext.Entry(isExist)
            .Reference(s => s.Country)
            .LoadAsync();
            return isExist;
        }

        public async Task<IEnumerable<State>> getAll(int pageNumber, int pageSize)
        {
            return await dbContext.States.Include(s=>s.Country).
                Skip((pageNumber - 1)*pageSize).
                Take(pageSize).
                ToListAsync();
        }


        public async Task<List<State>> getByCountryId(Guid id)
        {
            return await dbContext.States.Include(s=>s.Country).Where(s => s.CountryId == id).ToListAsync();
        }



        public async Task<State?> GetById(Guid id)
        {
            return await dbContext.States.Include(s => s.Country).FirstOrDefaultAsync(x => x.Id == id);
        }

        //for pagination
        public async Task<int> getTotalCount()
        {
            return await dbContext.States.CountAsync();
        }

        
    }
}
