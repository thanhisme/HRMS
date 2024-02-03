namespace Infrastructure.Models.ResponseModels.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string EmployeeCode { get; set; }

        public string Position { get; set; }

        public string Department { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
