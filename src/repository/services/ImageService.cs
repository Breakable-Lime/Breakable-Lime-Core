using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BreakableLime.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BreakableLime.Repository.services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ImageService> _logger;

        public ImageService(ApplicationDbContext dbContext, ILogger<ImageService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
        public async Task<bool> CreateImage(ContainerImage image, CancellationToken cancellationToken)
        {
            //check is valid TODO:Add fluent validation
            var userExists = await _dbContext.Users.AnyAsync(c => c.Id == image.Owner.Id, cancellationToken);
            if (!userExists)
            {
                _logger.LogError("Unable to create Image: No owner exists with the id {@Id}", image.Id);
                return false;
            }
            
            //Try create
            var result = await _dbContext.Images.AddAsync(image, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            //report

            if (result != null)
            {
                _logger.LogInformation("Created Image with Id {@Id}", image.Id);
                return true;
            }

            _logger.LogError("Unable to create image {@Image}", image);
            return false;
        }

        public async Task<bool> DeleteImage(ContainerImage image, CancellationToken cancellationToken)
        {
            //Check if valid
            var imageExists = await _dbContext.Images.AnyAsync(c => c.Id == image.Id, cancellationToken);

            if (!imageExists)
            {
                _logger.LogError("Unable to delete image: No such image exists {@Id}", image.Id);
                return false;
            }

            //delete
            var result = _dbContext.Images.Remove(image);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            
            //return result
            if (result != null)
            {
                _logger.LogInformation("Deleted image with id {@Id}", image.Id);
                return true;
            }
            
            _logger.LogInformation("Unable to delete image with id {@Id}", image.Id);
            return true;
        }

        public async Task<ContainerImage> EditImage(ContainerImage image, CancellationToken cancellationToken)
        {
            //Check if id exists
            var imageExists = await _dbContext.Images.AnyAsync(c => c.Id == image.Id, cancellationToken);
            
            //check is valid
            var userExists = await _dbContext.Users.AnyAsync(c => c.Id == image.Owner.Id, cancellationToken);

            if (!imageExists || !userExists)
            {
                _logger.LogError("Unable to Edit Image; user {user}; image {image}", userExists, imageExists);
                return null;
            }
            
            
            //update
            var newImage = _dbContext.Images.Update(image);
            await _dbContext.SaveChangesAsync(cancellationToken);


            //return new ContainerImage
            if (newImage?.Entity != null)
            {
                _logger.LogInformation("Edited image with id {Id}", image.Id);
                return newImage.Entity;
            }
            
            _logger.LogError("Unable to edit image {@Image}", image);
            return null;
        }
    }
}