using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWALKS.DB;
using NZWALKS.Models;
using NZWALKS.Models.DTO;

namespace NZWALKS.IRepository
{
    public class RegionRepositoryImpl : IRegionRepository
    {
        private readonly NZDBContext nZDB;
        public RegionRepositoryImpl(NZDBContext nZDB)
        {
            this.nZDB = nZDB;
        }

        public async Task<Regions> CreateRegion(Regions region)
        {
           await nZDB.regions.AddAsync(region); 
            await nZDB.SaveChangesAsync();
            return region;

        }

        public async Task<Regions?> DeleteRegion(Guid id)
        {
            var region = await nZDB.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region != null)
            {
                nZDB.regions.Remove(region);
                await nZDB.SaveChangesAsync();
                return region;
            }
            return null;
        }

        public async Task<List<Regions>> GetAllAsync()
        {
            return  await nZDB.regions.ToListAsync();
        }

        public async Task<Regions?> GetRegionByID(Guid id)
        {
            return await nZDB.regions.FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<Regions?> UpdateRegion(Guid id, Regions region)
        {
            var regions = await nZDB.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region != null)
            {
                region.Code = region.Code;
                region.Name = region.Name;
                region.RegionImageURI = region.RegionImageURI;
                await nZDB.SaveChangesAsync();
                return regions;
            }
            return null;
        }
    }
}
