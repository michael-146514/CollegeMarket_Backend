using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Role
    {
        [Key]
        public int id { get; set; }
       public string UserRole { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
