using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRespository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnv;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRespository(IWebHostEnvironment webHostEnv, 
            IHttpContextAccessor httpContextAccessor,
            NZWalksDbContext dbContext)
        {
            this.webHostEnv = webHostEnv;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }


        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine
            (
                webHostEnv.ContentRootPath, 
                "Images", 
                $"{image.FileName}{image.FileExtension}"                
            );

            // Upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}/Images/" +
                $"{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            // Add Image to Images Table of DB
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;

        }
    }
}
