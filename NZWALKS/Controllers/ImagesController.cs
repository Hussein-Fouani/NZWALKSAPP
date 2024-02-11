using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWALKS.IRepository;
using NZWALKS.Models;

namespace NZWALKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public ImagesController(IMapper mapper,IImageRepository imageRepository)
        {
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto image)
        {
            validateImage(image);
            if (ModelState.IsValid)
            {
                 var imagedomain = mapper.Map<Image>(image);
                await imageRepository.UploadImage(imagedomain);
                return Ok(imagedomain);

            }
           
            return BadRequest(ModelState);
        }       
        private void validateImage(ImageUploadRequestDto image)
        {
           var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            if (!allowedExtensions.Contains(Path.GetExtension(image.FileName)))
            {
               ModelState.AddModelError("File", "Invalid file extension."); 
            }
            if (image.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size exceeds 10MB.");
            }
        }
    }
}
