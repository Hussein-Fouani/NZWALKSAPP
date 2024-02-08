using NZWALKS.Models;
using NZWALKS.Models.DTO;

namespace NZWALKS.IRepository
{
    public interface IRegionRepository
    {
        Task<List<Regions>> GetAllAsync();
        Task<Regions> GetRegionByID(Guid id);
        Task<Regions> CreateRegion(Regions region);
        Task<Regions?> UpdateRegion(Guid id, Regions region);
        Task<Regions?> DeleteRegion(Guid id);
    }
}
