using NZWALKS.DB;
using NZWALKS.Models;

namespace NZWALKS.IRepository
{
    public class LocalImageUpload : IImageRepository
    {
        private readonly IWebHostEnvironment ev;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly NzdbContext dBContext;

        public LocalImageUpload(IWebHostEnvironment ev,IHttpContextAccessor contextAccessor,NzdbContext dBContext)
        {
            this.ev = ev;
            this.contextAccessor = contextAccessor;
            this.dBContext = dBContext;
        }
        public async Task<Image> UploadImage(Image image)
        {
            var localPath = Path.Combine(ev.ContentRootPath, "Images",$"{image.FileName}{image.FileExtension}" );
            using var stream = new FileStream(localPath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urnfieldPath = $"{contextAccessor.HttpContext.Request.Scheme}//{contextAccessor.HttpContext.Request.Host}{contextAccessor.HttpContext.Request.PathBase}/ Image/{image.FileName}{image.FileExtension}";

            await dBContext.Images.AddAsync(image);
            await dBContext.SaveChangesAsync();
            return image;

            

        }
    }
}
