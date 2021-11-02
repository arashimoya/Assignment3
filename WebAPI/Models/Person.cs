using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models {
public class Person {
   
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    [Required, MaxLength(128)]
    [JsonPropertyName("FirstName")]
    public string FirstName { get; set; }
    [Required, MaxLength(128)]
    [JsonPropertyName("LastName")]
    public string LastName { get; set; }
    [Required, MaxLength(128)]
    [JsonPropertyName("HairColor")]
    public string HairColor { get; set; }
    [Required, MaxLength(128)]
    [JsonPropertyName("EyeColor")]
    public string EyeColor { get; set; }
    [Required, Range(0,200, ErrorMessage = "Please enter value bigger than 0 and less than 200")]
    [JsonPropertyName("Age")]
    public int Age { get; set; }
    [Required, Range(0,int.MaxValue, ErrorMessage = "Please enter value bigger than 0")]
    [JsonPropertyName("Weight")]
    public float Weight { get; set; }
    [Required, Range(0,300, ErrorMessage = "Please enter positive value lesser than 300")]
    [JsonPropertyName("Height")]
    public int Height { get; set; }
    [Required]
    [JsonPropertyName("Sex")]
    public string Sex { get; set; }
}


}