using NZWALKS.Models;

namespace NZWALKS.IRepository
{
    public interface IImageRepository
    {
        Task<Image> UploadImage(Image image); 
    }
}
