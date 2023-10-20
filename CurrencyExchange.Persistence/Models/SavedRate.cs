using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Persistence.Models;

public class SavedRate
{
    public int Id { get; set; }

    [Required]
    public Currency Currency { get; set; } = default!;

    [Required]
    public float Rate { get; set; }

    [MaxLength(100)]
    public string? Comment { get; set; }

    [Required] public DateOnly RateDay { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1));

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime? LastUpdated { get; set; } = DateTime.UtcNow;
}