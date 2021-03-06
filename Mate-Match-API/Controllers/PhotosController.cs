using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Mate_Match_API.Data.Interfaces;
using Mate_Match_API.Data.Models;
using Mate_Match_API.DTOs;
using Mate_Match_API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Mate_Match_API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IDatingRepository repo;
        private readonly IOptions<CloudinarySettings> cloudinarySettings;
        private readonly IMapper mapper;
        private Cloudinary cloudinary;

        public PhotosController(IDatingRepository repo, IOptions<CloudinarySettings> cloudinarySettings, IMapper mapper)
        {
            this.mapper = mapper;
            this.cloudinarySettings = cloudinarySettings;
            this.repo = repo;

            Account acc = new Account(
                this.cloudinarySettings.Value.CloudName,
                this.cloudinarySettings.Value.ApiKey,
                this.cloudinarySettings.Value.ApiSecret
            );

            this.cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name="GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id){
            var photoFromRepo = await  this.repo.GetPhoto(id);
            var photo = this.mapper.Map<PhotosForReturnDto>(photoFromRepo);
            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var userFromRepo = await this.repo.GetUser(userId);

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();
            if(file.Length>0){
                using(var stream = file.OpenReadStream()){
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = this.cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = this.mapper.Map<Photo>(photoForCreationDto);

            if(!userFromRepo.Photos.Any(u=> u.IsMain))
                photo.IsMain = true;

            userFromRepo.Photos.Add(photo);

            if(await this.repo.SaveAll()){
                var photoToReturn = this.mapper.Map<PhotosForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id} , photoToReturn);
            }

            return BadRequest("Could not upload Photo");
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await this.repo.GetUser(userId);

            if (!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await this.repo.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("This is already the main photo");

            var currentMainPhoto = await this.repo.GetMainPhotoForUser(userId);
            currentMainPhoto.IsMain = false;

            photoFromRepo.IsMain = true;

            if (await this.repo.SaveAll())
                return NoContent();

            return BadRequest("Could not set photo to main");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await this.repo.GetUser(userId);

            if (!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await this.repo.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("You cannot delete your main photo");

            if (photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);

                var result = this.cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    this.repo.Delete(photoFromRepo);
                }
            }

            if (photoFromRepo.PublicId == null)
            {
                this.repo.Delete(photoFromRepo);
            }

            if (await this.repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete the photo");
        }

    }
}