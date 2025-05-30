using cmet_backend.Common;
using cmet_backend.Controllers;
using cmet_backend.Video;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cmet_backend.Content
{
    [Route("/v1/content")]
    public class ArticleController : BaseApiController
    {
        private readonly IArticleRepository repository;
        private readonly ILogger<ArticleController> logger;

        public ArticleController(IArticleRepository repository, ILogger<ArticleController> logger)
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
    }
}
