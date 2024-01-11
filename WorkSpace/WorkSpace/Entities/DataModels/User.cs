using Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class User : AuditableEntity<long>
    {
        [StringLength(16)]
        [Column("first_name", TypeName = "varchar")]
        public string FirstName { get; set; } = null!;

        [StringLength(16)]
        [Column("last_name", TypeName = "varchar")]
        public string LastName { get; set; } = null!;

        [StringLength(128)]
        [Column("email", TypeName = "varchar")]
        public string Email { get; set; } = null!;

        [MaxLength(255)]
        [Column("password", TypeName = "varchar")]
        public string Password { get; set; } = null!;

        [StringLength(13)]
        [Column("phone_number", TypeName = "varchar")]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(512)]
        [Column("address", TypeName = "varchar")]
        public string Address { get; set; } = null!;

        [Column("dob")]
        public DateTimeOffset? DOB { get; set; }

        [Column("role")]
        public byte Role { get; set; }

        [Column("status")]
        public byte Status { get; set; }
    }
}
