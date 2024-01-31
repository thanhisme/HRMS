using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    // Missing the information about the insurance
    [Table("Users")]
    [Index(nameof(Email), IsUnique = true, Name = "Username")]
    public class User : BaseEntity
    {
        public enum GenderType
        {
            MALE,
            FEMALE,
        }

        public enum MaritalStatusType
        {
            SINGLE,
            MARRIED,
            DIVORCED,
            WIDOWED,
        }

        public enum EducationType
        {
            COLLEGE,
            UNIVERSITY,
            MASTER,
            DOCTOR,
        }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public GenderType Gender { get; set; } = GenderType.MALE;

        public string Email { get; set; } = string.Empty;

        public string CompanyEmail { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Tel { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; } = DateTime.UtcNow;

        public string Avatar { get; set; } = string.Empty;

        public string BirthPlace { get; set; } = string.Empty;

        public string IdentityCard { get; set; } = string.Empty;

        public string IdentityCardIssuedBy { get; set; } = string.Empty;

        public DateTime IdentityCardIssuedDate { get; set; } = DateTime.UtcNow;

        public string TaxCode { get; set; } = string.Empty;

        public string BankAccount { get; set; } = string.Empty;

        public string BankName { get; set; } = string.Empty;

        public MaritalStatusType MaritalStatus { get; set; } = MaritalStatusType.SINGLE;

        public string Nation { get; set; } = string.Empty;

        public string Religion { get; set; } = string.Empty;

        public string Nationality { get; set; } = string.Empty;

        public string SocialAccount { get; set; } = string.Empty;

        public EducationType Education { get; set; } = EducationType.UNIVERSITY;

        public string University { get; set; } = string.Empty;

        public List<Attachment> Certificates { get; set; } = new List<Attachment>();

        public DateTime GraduationDate { get; set; } = DateTime.UtcNow;
    }
}
