using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWALKS.DB;
using NZWALKS.Models;

namespace NZWALKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZDBContext nZDB;

        public RegionsController(NZDBContext nZDB)
        {
            this.nZDB = nZDB;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var region= nZDB.regions.ToList();
            return Ok(region);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionByID([FromRoute]Guid id)
        {
            nZDB.regions.Find(id); 
        }
            
    }
}
 