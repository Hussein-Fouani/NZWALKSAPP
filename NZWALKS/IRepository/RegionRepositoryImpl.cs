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

        public Task<Regions> CreateRegion(AddRegionDTO region)
        {
            throw new NotImplementedException();
        }

        public Task<Regions> DeleteRegion(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Regions>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Regions> GetRegionByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Regions> UpdateRegion(Guid id, UpdateRegionDTO region)
        {
            throw new NotImplementedException();
        }
    }
}
