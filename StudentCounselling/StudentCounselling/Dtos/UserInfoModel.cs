using System.ComponentModel.DataAnnotations;

namespace StudentCounselling.Dtos
{
    public class CreateUserModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserNumber { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string RoleId { get; set; }
        public string DepartmentId { get; set; }
    }
    public class UpdateUserModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string DepartmentId { get; set; }
        public string ImageUrl { get; set; }
    }
}
