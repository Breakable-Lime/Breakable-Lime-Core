using System;
using System.ComponentModel.DataAnnotations;

namespace BreakableLime.Repository.Models
{
    public class ContainerImage
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy-hh:mm}")]
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        
        public ApplicationIdentityUser Owner { get; set; }
        
        public string ImageName { get; set; }
        public string ImageTag { get; set; }
    }
}