using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assess2.Models
{
    [Table("Directors")]
    public class Directors
    {
        [Key]
        public int director_id { get; set; }
        public string director_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public int no_of_movies { get; set; }
        public string response { get; set; }
    }
}
