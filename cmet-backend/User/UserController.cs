using cmet_backend.Common;
using Microsoft.AspNetCore.Mvc;

namespace cmet_backend.User
{
    [Route("/v1/user")]
    public class UserController : BaseApiController
    {
        private readonly IUserRepository repository;
        private readonly ILogger<UserController> logger;

        public UserController(IUserRepository repository, ILogger<UserController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await repository.GetAllAsync();
            return Ok(users.Select(u => new UserResponse
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await repository.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = $"User with id {id} not found" });

            return Ok(new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRq request)
        {
            var user = new UserEntity
            {
                Id = Guid.NewGuid().ToString(),
                Username = request.Username,
                Email = request.Email,
                Password = request.Password, // лучше хэшировать
                Role = request.Role,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(user);
            return Ok(new { message = "User created", user.Id });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UserRq request)
        {
            var user = await repository.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = $"User with id {id} not found" });

            user.Username = request.Username;
            user.Email = request.Email;
            user.Password = request.Password; // снова, лучше хэшировать
            user.Role = request.Role;

            await repository.UpdateAsync(user);
            return Ok(new { message = "User updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await repository.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = $"User with id {id} not found" });

            await repository.DeleteAsync(id);
            return Ok(new { message = "User deleted" });
        }
    }
}
