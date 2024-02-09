using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWALKS.DB;
using NZWALKS.Models;
using NZWALKS.Models.DTO;
using NZWALKS.Models.WalksDTO;

namespace NZWALKS.IRepository
{
    public class WalksRepositoryImpl : IWalkRepository
    {
        private readonly NZDBContext nZDB;
        private readonly IMapper mapper;

        public WalksRepositoryImpl(NZDBContext nZDB,IMapper mapper)
        {
            this.nZDB = nZDB;
            this.mapper = mapper;
        }
        public async Task<Walks> CreateAsync(Walks walkDTO)
        {
            
            await nZDB.Walks.AddAsync(walkDTO);
           await   nZDB.SaveChangesAsync();
           return walkDTO;
        }

        public async Task<Walks?> DeleteWalk(Guid id)
        {
            var walk = await nZDB.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
            nZDB.Walks.Remove(walk);
            await nZDB.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walks>> GetAllWalks(string? filter = null, string? filterQuery = null, [FromQuery] string? SortingQuery = null, [FromQuery] bool? fromQuery= true)
        {
            //var getall = await nZDB.Walks.Include("regions").Include("difficulty").ToListAsync();
            var walks = nZDB.Walks.Include("regions").Include("difficulty").AsQueryable();
            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterQuery))
            {
                switch (filter.ToLower())
                {
                    case "region":
                        walks = walks.Where(x => x.regions.Name.ToLower().Contains(filterQuery.ToLower()));
                        break;
                    case "difficulty":
                        walks = walks.Where(x => x.difficulty.Name.ToLower().Contains(filterQuery.ToLower()));
                        break;
                    default:
                        break;
                }
            }
            if (string.IsNullOrEmpty(SortingQuery)==false)
            {
                if(SortingQuery.ToLower()=="Name")
                {
                    walks = (bool)fromQuery ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
               
                else if (SortingQuery.ToLower() == "length")
                {
                    walks= (bool)fromQuery ? walks.OrderBy(x=>x.LengthInKm):walks.OrderByDescending(x=>x.LengthInKm);
                }
            }
            return await walks.ToListAsync();
        }

        public Task<Walks?> GetWalkByIDAsync(Guid id)
        {
            var getwalk = nZDB.Walks.Include("regions").Include("difficulty").FirstOrDefaultAsync(x => x.Id == id);
            return getwalk;
            }

        public async Task<Walks> UpdateAsync(UpdateWalkDTO walkDTO, Guid Id)
        {
            var walk = await nZDB.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (walk == null)
            {
                return null;
            }
            walk.Name = walkDTO.Name;
            walk.Description = walkDTO.Description;
            walk.WalkImageURI = walkDTO.WalkImageURI;
            walk.RegionId = walkDTO.RegionId;
            await nZDB.SaveChangesAsync();
            return walk; 
            
        }
    }
}
