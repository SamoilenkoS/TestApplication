using DataAccessLayer.Models;

namespace BussinessLayer.JWT
{
    public class ValidationResult
    {
        public bool IsSuccessful { get; set; }
        public  UserWithRoles UserWithRoles { get; set; }
    }
}
