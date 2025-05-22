using cmet_backend.Entities;
using cmet_backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace cmet_backend.Controllers
{
    [Route("v1/video")]
    public class VideoController : BaseApiController
    {
        private readonly IVideoRepository repository;
        private readonly ILogger<VideoController> logger;

        public VideoController(IVideoRepository repository, ILogger<VideoController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            logger.LogInformation($"Получние видеоматериала с id {id}");
            var material = await repository.GetByIdAsync(id);
            if (material == null)
            {
                return ApiError("NOT_FOUND", $"VideoMaterial with id {id} not found", 404);
            }

            return ApiOk(material);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return ApiOk(repository.GetAllAsync().Result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(VideoMaterialData videoData)
        {
            var entity = new VideoMaterial()
            {
                Id = Guid.NewGuid().ToString(),
                Description = videoData.Description,
                CreatedAt = videoData.CreatedAt,
                link = videoData.Link
            };
            await repository.AddAsync(entity);
            return ApiOk();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, VideoMaterialData videoData)
        {
            var entity = new VideoMaterial()
            {
                Id = id,
                Description = videoData.Description,
                CreatedAt = videoData.CreatedAt,
                link = videoData.Link
            };
            await repository.UpdateAsync(entity);
            return ApiOk();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            logger.LogInformation($"Удаление видеоматериала с id {id}");
            if (!(await repository.existsById(id)))
            {
                return ApiError("NOT_FOUND", $"VideoMaterial with id {id} not found", 404);
            }
            await repository.DeleteAsync(id);

            return ApiOk();
        }
    }
}
