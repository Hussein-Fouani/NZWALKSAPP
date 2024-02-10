using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWALKS.CustomAFilter;
using NZWALKS.DB;
using NZWALKS.IRepository;
using NZWALKS.Models;
using NZWALKS.Models.DTO;

namespace NZWALKS.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IMapper mapper;

    private readonly NZDBContext nZDB;
    private readonly IRegionRepository regionRepository;

    public RegionsController(NZDBContext nZDB, IRegionRepository regionRepository, IMapper mapper)
    {
        this.nZDB = nZDB;
        this.regionRepository = regionRepository;
        this.mapper = mapper;
    }

    //Document this method
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var regionsDomain = await regionRepository.GetAllAsync();
        return Ok(mapper.Map<List<RegionDTO>>(regionsDomain));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [ValidateModel]
    public async Task<IActionResult> GetRegionByID([FromRoute] Guid id)
    {
        //var region =nZDB.regions.Find(id);

        var region = await regionRepository.GetRegionByID(id);
        var regionDto = mapper.Map<RegionDTO>(region);

        if (region == null) return NotFound();
        return Ok(regionDto);
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> CreateRegion([FromBody] AddRegionDTO regions)
    {
        //Map or Convert Dto to domain model
        //use domain model to create region
        var reg = mapper.Map<Regions>(regions);
        reg = await regionRepository.CreateRegion(reg);
        var region = mapper.Map<RegionDTO>(reg);

        return CreatedAtAction(nameof(GetRegionByID), new { id = reg.Id }, region);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    public async Task<IActionResult> UpdateRegion([FromBody] UpdateRegionDTO region, [FromRoute] Guid id)
    {
        //map dto to domain
        var regionmodel = mapper.Map<Regions>(region);
        var reg = await regionRepository.UpdateRegion(id, regionmodel);
        if (reg == null) return BadRequest("Data provided Not Supported");

        await nZDB.SaveChangesAsync();
        return Ok(mapper.Map<RegionDTO>(regionmodel));
    }

    [HttpDelete]
    [Route("{Id:Guid}")]
    [ValidateModel]
    public async Task<IActionResult> deleteRegion(Guid Id)
    {
        var region = await regionRepository.DeleteRegion(Id);
        if (region == null) return NotFound();

        return Ok(mapper.Map<RegionDTO>(region));
    }
    
}