using System.ComponentModel.DataAnnotations;

namespace Models {
public class Person {
    
    public int Id { get; set; }
    [Required, MaxLength(128)]
    
    public string FirstName { get; set; }
    [Required, MaxLength(128)]
    public string LastName { get; set; }
    [Required, MaxLength(128)]
    public string HairColor { get; set; }
    [Required, MaxLength(128)]
    public string EyeColor { get; set; }
    [Required, Range(0,200, ErrorMessage = "Please enter value bigger than 0 and less than 200")]
    public int Age { get; set; }
    [Required, Range(0,int.MaxValue, ErrorMessage = "Please enter value bigger than 0")]
    public float Weight { get; set; }
    [Required, Range(0,300, ErrorMessage = "Please enter positive value lesser than 300")]
    public int Height { get; set; }
    [Required]
    public string Sex { get; set; }
}


}