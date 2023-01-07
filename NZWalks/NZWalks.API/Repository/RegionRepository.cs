using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region =await GetAsync( id);
            if (region == null)
                return null;
             nZWalksDbContext.Remove(region);
           await nZWalksDbContext.SaveChangesAsync();
            return region;

        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return  await ( nZWalksDbContext.Regions.ToListAsync());
           
        }

        public async Task<Region> GetAsync(Guid id)
        {
            var region= await nZWalksDbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null)
                return null;
            return region;
        }

        public Task<Region> PatchAsync(Region region)
        {
            throw new NotImplementedException();
        }

        public async Task<Region> PostAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> PutAsync(Guid id, Region region)
        {
            var existingRegion= await nZWalksDbContext.Regions.FirstOrDefaultAsync(r => r.Id ==id);
            if (existingRegion == null)
                return null;
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;
            await nZWalksDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
