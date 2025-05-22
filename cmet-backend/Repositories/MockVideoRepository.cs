using cmet_backend.Entities;
using System.Collections;

namespace cmet_backend.Repositories
{
    public class MockVideoRepository : IVideoRepository
    {
        private Dictionary<string, VideoMaterial> database;

        public MockVideoRepository()
        {
            database = new Dictionary<string, VideoMaterial>();
            var id1 = Guid.NewGuid().ToString();
            database.Add(id1, new VideoMaterial()
            {
                Id = id1,
                Description = "Мастер-класс в рамках конкурса « Лучший преподаватель ДГУ 2024» Омаровой Зульфии Омаровны, старшего преподавателя кафедры общегуманитарных и социально- экономических дисциплин колледжа ДГУ (Победитель конкурса) (дата проведения: ноябрь 2024г.)",
                link = "https://rutube.ru/video/db52c0c5093c4f2e49e7882fc004a9d1/"
            });
            var id2 = Guid.NewGuid().ToString();
            database.Add(id2, new VideoMaterial()
            {
                Id = id2,
                Description = "Мастер- класс в рамках конкурса « Лучший преподаватель ДГУ 2024» Эмировой Дианы Мирзеевны, доцента кафедры английской филологии ФИЯ (Призер конкурса) ( дата проведения: ноябрь 2024г.)",
                link = "https://rutube.ru/video/817a8b3426ba46cdfb7314cde58807e6/"
            });
        }

        public Task AddAsync(VideoMaterial material)
        {
            var result = database.TryAdd(material.Id, material);
            return Task.FromResult(result);
        }

        public Task DeleteAsync(string id)
        {
            database.Remove(id);
            return Task.CompletedTask;
        }

        public Task<bool> existsById(string id)
        {
            return Task.FromResult(database.ContainsKey(id));
        }

        public Task<IEnumerable<VideoMaterial>> GetAllAsync()
        {
            return Task.FromResult(database.Values.AsEnumerable());
        }

        public Task<VideoMaterial?> GetByIdAsync(string id)
        {
            return Task.FromResult(database.GetValueOrDefault(id, null));
        }

        public Task UpdateAsync(VideoMaterial material)
        {
            database[material.Id] = material;
            return Task.CompletedTask;
        }
    }
}
