using System.ComponentModel.DataAnnotations;

namespace CustomValidator;

public class Cat
{
    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string Name { get; set; }

    [Range(0, 20)]
    public int Age { get; set; }

    [StringLength(10)]
    public string Color { get; set; }
}
