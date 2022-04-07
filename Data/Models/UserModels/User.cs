using LuxuryCarsApp.Data.Models.UserModels;

namespace LuxuryCarsApp.Models.UserModels
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public Role Role { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
