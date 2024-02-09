using NZWALKS.Models;
using NZWALKS.Models.DTO;
using NZWALKS.Models.WalksDTO;

namespace NZWALKS.IRepository
{
    public interface IWalkRepository
    {
      Task<Walks>  CreateAsync(Walks walkDTO);
        Task<Walks>  UpdateAsync(UpdateWalkDTO walkDTO,Guid Id);
        Task<List<Walks>> GetAllWalks();
        Task<Walks?> GetWalkByIDAsync(Guid id);
        Task<Walks?> DeleteWalk(Guid id);
    }

    
}
