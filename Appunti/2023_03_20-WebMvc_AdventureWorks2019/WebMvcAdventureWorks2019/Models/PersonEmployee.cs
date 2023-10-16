using System.ComponentModel.DataAnnotations;

namespace WebMvcAdventureWorks2019.Models
{
    public class PersonEmployee
    {
        [Key]
        public int BusinessEntityId { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? LoginID { get; set; }

        public string? JobTitle { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
