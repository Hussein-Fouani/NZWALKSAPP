using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWALKS.CustomAFilter;
using NZWALKS.IRepository;
using NZWALKS.Models;
using NZWALKS.Models.DTO;
using NZWALKS.Models.WalksDTO;

namespace NZWALKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkrepository;

        public WalkController(IMapper mapper,IWalkRepository walkrepository)
        {
            this.mapper = mapper;
            this.walkrepository = walkrepository;
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalks([FromBody] AddWalkDTO walkDTO){
           
            
                var walk = mapper.Map<Walks>(walkDTO);
                if (walk == null)
                {
                    return null;
                }
                await walkrepository.CreateAsync(walk);
                return Ok(mapper.Map<WalkDTO>(walk));
            
             

        }
        [HttpGet]
        // Get Walk
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {

            var walksdomain = walkrepository.GetAllWalks();
            return Ok(mapper.Map<WalkDTO>(walksdomain));

        }
        [HttpGet]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult?> GetWalkByID(Guid id)
        {
            
                var walk = walkrepository.GetWalkByIDAsync(id);
                if (walk == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<WalkDTO>(walk));
            
        }
        [HttpPut]
        [Route("{i d:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute]Guid id, [FromBody] UpdateWalkDTO walkDTO)
        {
            
                var walk = walkrepository.UpdateAsync(walkDTO, id);
                if (walk == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<WalkDTO>(walk));
         
        }
        [HttpDelete]
        [Route("{Id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteWalk([FromRoute]Guid Id)
        {
            
                var walktodelete = walkrepository.DeleteWalk(Id);
                if (walktodelete == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<WalkDTO>(walktodelete));
            
        }

    }
}
