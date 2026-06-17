using System.ComponentModel.DataAnnotations;
using LapisApi.Data.Models;
namespace LapisApi.App.Countries.Model;

public class Country
{
  [Key]
  public int Id { get; set; }

  public required string NameAr { get; set; }
  public required string NameEn { get; set; }

  public string? NotesAr { get; set; } = null;
  public string? NotesEn { get; set; } = null;
  public bool IsActive { get; set; }
  public bool IsAutomaticAcceptance { get; set; }
  public required decimal MaximumTransferLimit { get; set; }

  public decimal CommissionRate { get; set; }
  public ICollection<City> Cities { get; set; } = new List<City>();
}