using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MetaData.Models
{
    public class Uploader
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("IP Address")]
        public string? IP_Address { get; set; }

        [DisplayName("Date Visited")]
        public DateTime? Date_Visited { get; set; }

        [DisplayName("Number of Uploads")]
        public int? Number_Of_Uploads { get; set; }
    }
}
