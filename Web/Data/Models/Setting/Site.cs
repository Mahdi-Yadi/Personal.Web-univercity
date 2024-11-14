using System.ComponentModel.DataAnnotations;

namespace Web.Data.Models.Setting
{
    public class Site
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageNameLogo { get; set; }

        public string Bio { get; set; }

        public string About { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }

    }
}
