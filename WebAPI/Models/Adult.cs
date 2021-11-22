using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace WebAPI.Models {
public class Adult : Person {
    public Job JobTitle { get; set; }

    public Adult()
    {
        JobTitle = new Job();
    }
}
}