using System.Net;
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
    private readonly IMapper _mapper;

    private readonly NzdbContext _nZdb;
    private readonly ILogger<RegionsController> _logger;
    private readonly IRegionRepository _regionRepository;

    public RegionsController(NzdbContext nZdb, ILogger<RegionsController> logger,IRegionRepository regionRepository, IMapper mapper)
    {
        this._nZdb = nZdb;
        _logger = logger;
        this._regionRepository = regionRepository;
        this._mapper = mapper;
    }

    //Document this method
    [HttpGet]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogError("All Records had been requested");
        var regionsDomain = await _regionRepository.GetAllAsync();
        return Ok(_mapper.Map<List<RegionDTO>>(regionsDomain));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [ValidateModel]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
    {
        //var region =nZDB.regions.Find(id);

        try
        {
            var region = await _regionRepository.GetRegionByID(id);
            var regionDto = _mapper.Map<RegionDTO>(region);
            return Ok(regionDto);
        }
        catch (Exception)
        {

            return Problem("RegionNotFound", null, (int)HttpStatusCode.NotFound, "");
        }

        

        
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> CreateRegion([FromBody] AddRegionDTO regions)
    {
        //Map or Convert Dto to domain model
        //use domain model to create region
        var reg = _mapper.Map<Regions>(regions);
        reg = await _regionRepository.CreateRegion(reg);
        var region = _mapper.Map<RegionDTO>(reg);

        return CreatedAtAction(nameof(GetRegionById), new { id = reg.Id }, region);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> UpdateRegion([FromBody] UpdateRegionDTO region, [FromRoute] Guid id)
    {
        //map dto to domain
        var regionmodel = _mapper.Map<Regions>(region);
        var reg = await _regionRepository.UpdateRegion(id, regionmodel);
        if (reg == null) return BadRequest("Data provided Not Supported");

        await _nZdb.SaveChangesAsync();
        return Ok(_mapper.Map<RegionDTO>(regionmodel));
    }

    [HttpDelete]
    [Route("{Id:Guid}")]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> DeleteRegion(Guid Id)
    {
        var region = await _regionRepository.DeleteRegion(Id);
        if (region == null) return NotFound();

        return Ok(_mapper.Map<RegionDTO>(region));
    }
    
}