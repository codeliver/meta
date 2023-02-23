using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MetaData.Models
{
    public class Metadata
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateGenerated { get; set; }

        public string? ImageURL { get; set; }

        public string? IPAddress { get; set; }

        public string? ResponseCode { get; set; }
    }
}
