using cmet_backend.Article;
using cmet_backend.Attachment;
using cmet_backend.Common;
using cmet_backend.Controllers;
using cmet_backend.Video;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cmet_backend.Content
{
    [Route("/v1/article")]
    public class ArticleController : BaseApiController
    {
        private readonly IArticleRepository repository;
        private readonly IAttachmentRepository attachmentRepository;
        private readonly ILogger<ArticleController> logger;

        public ArticleController(
            IArticleRepository repository, 
            ILogger<ArticleController> logger,
            IAttachmentRepository attachmentRepository)
        {
            this.repository = repository;
            this.logger = logger;
            this.attachmentRepository = attachmentRepository;
        }

        /// <summary>
        /// Получение информации о статье по id
        /// </summary>
        /// <param name="id">Идентификатор видео</param>
        /// <returns>Информация о видео вместе со ссылкой</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id)
        {
            logger.LogInformation($"Получение статьи с id {id}");

            var article = await repository.GetByIdAsync(id);
            if (article == null)
            {
                return ApiError("NOT_FOUND", $"Article with id {id} not found", 404);
            }

            var attachments = await attachmentRepository.GetByReferenceAsync(id, "article");

            var dto = new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text,
                CreatedAt = article.CreatedAt,
                Files = attachments.Select(a => new AttachmentDto
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList()
            };

            return ApiOk(dto);
        }

        /// <summary>
        /// Получение всех статей
        /// </summary>
        /// <returns>Список статей</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("Получение всех статей");

            var articles = await repository.GetAllAsync();

            // Получаем список всех reference_id статей
            var articleIds = articles.Select(a => a.Id).ToList();

            // Получаем все вложения для статей
            var attachments = await attachmentRepository.GetByReferencesAsync(articleIds, "article");

            // Формируем DTO
            var result = articles.Select(article =>
            {
                var files = attachments
                    .Where(a => a.ReferenceId == article.Id)
                    .Select(a => new AttachmentDto
                    {
                        Id = a.Id,
                        Name = a.Name
                    }).ToList();

                return new ArticleDto
                {
                    Id = article.Id,
                    Title = article.Title,
                    Text = article.Text,
                    CreatedAt = article.CreatedAt,
                    Files = files
                };
            }).ToList();

            return ApiOk(result);
        }

        /// <summary>
        /// Добавление новой статьи
        /// </summary>
        /// <param name="article">Данные новой статьи</param>
        /// <returns>Созданная статья</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] ArticleRq request)
        {
            var articleId = Guid.NewGuid().ToString();

            var article = new ArticleEntity
            {
                Id = articleId,
                Title = request.Title,
                Text = request.Text,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(article);

            // Добавляем привязанные файлы (если есть)
            if (request.Files != null && request.Files.Any())
            {
                var attachments = request.Files.Select(f => new AttachmentEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = f.Name,
                    ReferenceId = articleId,
                    ReferenceType = "article",
                    UploadedAt = DateTime.UtcNow
                }).ToList();

                await attachmentRepository.AddRangeAsync(attachments);
            }

            return ApiOk(new { article.Id });
        }

        /// <summary>
        /// Обновление существующей статьи
        /// </summary>
        /// <param name="id">ID статьи</param>
        /// <param name="updated">Обновлённые данные</param>
        /// <returns>Обновлённая статья</returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(string id, [FromBody] ArticleDto request)
        {
            var article = await repository.GetByIdAsync(id);
            if (article == null)
                return ApiError("NOT_FOUND", $"Article with id {id} not found", 404);

            article.Title = request.Title;
            article.Text = request.Text;

            await repository.UpdateAsync(article);

            var oldAttachments = await attachmentRepository.GetByReferenceAsync(id, "article");
            if (oldAttachments.Any())
            {
                var oldIds = oldAttachments.Select(a => a.Id).ToList();
                await attachmentRepository.DeleteByIdsAsync(oldIds);
            }
            if (request.Files != null && request.Files.Any())
            {
                var newAttachments = request.Files.Select(f => new AttachmentEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = f.Name,
                    ReferenceId = id,
                    ReferenceType = "article",
                    UploadedAt = DateTime.UtcNow
                }).ToList();

                await attachmentRepository.AddRangeAsync(newAttachments);
            }

            return ApiOk();
        }

        /// <summary>
        /// Удаление статьи
        /// </summary>
        /// <param name="id">ID статьи</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await repository.GetByIdAsync(id);
            if (existing == null)
                return ApiError("NOT_FOUND", $"Article with id {id} not found", 404);

            logger.LogInformation($"Удаление статьи с id {id}");
            await repository.DeleteAsync(id);

            return ApiOk(new { message = $"Article with id {id} deleted." });
        }
    }
}
