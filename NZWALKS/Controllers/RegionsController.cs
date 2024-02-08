using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWALKS.DB;
using NZWALKS.IRepository;
using NZWALKS.Models;
using NZWALKS.Models.DTO;

namespace NZWALKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        
        private readonly NZDBContext nZDB;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZDBContext nZDB,IRegionRepository regionRepository)
        {
            this.nZDB = nZDB;
            this.regionRepository = regionRepository;
        }
        //Document this method
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            var regiondto = new List<RegionDTO>();
            foreach (var item in regionsDomain)
            {
              regiondto.Add (new RegionDTO
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.Name,
                    RegionImageURI = item.RegionImageURI
                });
            }
            return Ok(regiondto);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionByID([FromRoute]Guid id)
        {

             //var region =nZDB.regions.Find(id);

            var region =await nZDB.regions.FirstOrDefaultAsync(x=>x.Id==id);
            RegionDTO regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageURI = region.RegionImageURI
            };
            if (region == null)
            {
                return NotFound();
            }
            return Ok(regionDTO);
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionDTO regions)
        {
            //Map or Convert Dto to domain model
            //use domain model to create region
            var reg = new Regions
            {
                Code = regions.Code,
                Name = regions.Name,
                RegionImageURI = regions.RegionImageURI
            };
            await nZDB.regions.AddAsync(reg);
          await  nZDB.SaveChangesAsync();
            var region= new RegionDTO
            {
                Id=reg.Id,
                Code = reg.Code,
                Name = reg.Name,
                RegionImageURI = reg.RegionImageURI
            };
            return CreatedAtAction(nameof(GetRegionByID),new {id=reg.Id}, region);
                
    }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromBody] UpdateRegionDTO region, [FromRoute] Guid id)
        { 
            var reg =await nZDB.regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (reg == null)
            {
                return BadRequest("Data provided Not Supported");
            }
            //Map DTO To domain
             reg.Code=region.Code;
             reg.Name = region.Name;
             reg.RegionImageURI = region.RegionImageURI;
            
            var converted = new RegionDTO {
                Id =reg.Id,
                Name =reg.Name,
                Code=reg.Code,
                RegionImageURI = reg.RegionImageURI
                }; 
          await   nZDB.SaveChangesAsync();
            return Ok(converted);   

        }
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> deleteRegion(Guid Id)
        {
            var region =await nZDB.regions.FirstOrDefaultAsync (i=>i.Id==Id);
            if (region == null) { 
                return NotFound();
                }
            nZDB.regions.Remove(region);
            await nZDB.SaveChangesAsync();
            return NoContent();
        }
}
}
 