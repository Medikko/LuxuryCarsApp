namespace LuxuryCarsApp.Data.Models.UserModels
{ 
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
