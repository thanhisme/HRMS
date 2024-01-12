using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Users")]
    [Index(nameof(Email), IsUnique = true, Name = "Username")]
    public class User : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string EmployeeCode { get; set; } = string.Empty;

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Tel { get; set; }

        public DateTime WorkStartDate { get; set; }
    }
}
