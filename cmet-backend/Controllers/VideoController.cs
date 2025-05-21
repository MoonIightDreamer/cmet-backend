using cmet_backend.Entities;
using cmet_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace cmet_backend.Controllers
{
    [Route("api/[controller]")]
    public class VideoController : BaseApiController
    {
        //private readonly IVideoRepository _repo;

        public VideoController()
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //var material = await _repo.GetByIdAsync(id);
            //if (material == null)
            //    return ApiError("NOT_FOUND", $"VideoMaterial with id {id} not found", 404);

            if (id.StartsWith("e"))
            {
                throw new Exception("Неизвестная ошибка, тест");
            }

            return ApiOk(new VideoMaterial()
            {
                Id = "id_uuid_v4",
                Description = "Мастер-класс в рамках конкурса « Лучший преподаватель ДГУ 2024» Омаровой Зульфии Омаровны, старшего преподавателя кафедры общегуманитарных и социально- экономических дисциплин колледжа ДГУ (Победитель конкурса) (дата проведения: ноябрь 2024г.)",
                link = "https://rutube.ru/video/db52c0c5093c4f2e49e7882fc004a9d1/"
            });
        }
    }
}
