using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Persistence.Models;

public class Currency
{
    public int Id { get; set; }
    
    [Required]
    public string CurrencyName { get; set; }
    
    [Required]
    public int Unit { get; set; }

    [Required]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public List<SavedRate> SavedRates { get; set; }
}