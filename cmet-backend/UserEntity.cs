using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace cmet_backend
{
    [Table(name: "\"user\"")]
    public class UserEntity
    {
        [Key]
        [Column(name: "id")]
        public string Id { get; set; }
    }
}
