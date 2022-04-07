using System.ComponentModel.DataAnnotations;

namespace LuxuryCarsApp.Data.Models.UserModels
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public int Years { get; set; }
        public Role Role { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
