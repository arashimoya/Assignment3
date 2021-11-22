using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        
        public int Salary { get; set; }
    }
}