using cmet_backend.Common;
using cmet_backend.Video;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cmet_backend.Controllers
{
    /// <summary>
    /// Сервис управления видеоматериалами
    /// </summary>
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

        /// <summary>
        /// Получение информации о видео по id
        /// </summary>
        /// <param name="id">Идентификатор видео</param>
        /// <returns>Информация о видео вместе со ссылкой</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
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

        /// <summary>
        /// Получение всех видео
        /// </summary>
        /// <returns>Список видео</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return ApiOk(repository.GetAllAsync().Result);
        }

        /// <summary>
        /// Добавление видео в список материалов
        /// </summary>
        /// <param name="videoData">Данные видео</param>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add(VideoMaterialData videoData)
        {
            var entity = new VideoMaterialEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Description = videoData.Description,
                CreatedAt = videoData.CreatedAt,
                Link = videoData.Link
            };
            await repository.AddAsync(entity);
            return ApiOk();
        }

        /// <summary>
        /// Изменение данных видео по id
        /// </summary>
        /// <param name="id">Идентификатор видео</param>
        /// <param name="videoData">Новые данные видео</param>
        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Patch(string id, VideoMaterialData videoData)
        {
            if (!(await repository.ExistsById(id)))
            {
                return ApiError("NOT_FOUND", $"VideoMaterial with id {id} not found", 404);
            }
            var entity = new VideoMaterialEntity()
            {
                Id = id,
                Description = videoData.Description,
                CreatedAt = videoData.CreatedAt,
                Link = videoData.Link
            };
            await repository.UpdateAsync(entity);
            return ApiOk();
        }

        /// <summary>
        /// Удаление видео из списка материалов по id
        /// </summary>
        /// <param name="id">Идентификатор видео</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            logger.LogInformation($"Удаление видеоматериала с id {id}");
            if (!(await repository.ExistsById(id)))
            {
                return ApiError("NOT_FOUND", $"VideoMaterial with id {id} not found", 404);
            }
            await repository.DeleteAsync(id);

            return ApiOk();
        }
    }
}
