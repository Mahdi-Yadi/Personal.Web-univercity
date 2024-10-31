using System.ComponentModel.DataAnnotations;

namespace Web.Data.Models.Projects
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageName { get; set; }

        public string Text { get; set; }


    }
}
