using Microsoft.AspNetCore.Mvc;
using NZWALKS.Models;
using NZWALKS.Models.DTO;
using NZWALKS.Models.WalksDTO;

namespace NZWALKS.IRepository
{
    public interface IWalkRepository
    {
      Task<Walks>  CreateAsync(Walks walkDTO);
        Task<Walks>  UpdateAsync(UpdateWalkDTO walkDTO,Guid Id);
        Task<List<Walks>> GetAllWalks(string? filter=null,string? filterQuery=null, [FromQuery] string? SortingQuery=null, [FromQuery] bool? fromQuery=true, [FromQuery] int pagenb = 1, [FromQuery] int pagsize = 20);
        Task<Walks?> GetWalkByIDAsync(Guid id);
        Task<Walks?> DeleteWalk(Guid id);
    }

    
}
