using System.ComponentModel.DataAnnotations;
using LapisApi.App.Centers.Model;
using LapisApi.App.Countries.Model;
namespace LapisApi.Data.Models
{
  public class City
  {
    [Key]
    public int Id { get; set; }

    public required string NameAr { get; set; }
    public required string NameEn { get; set; }

    public bool IsActive { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public ICollection<Center> Centers { get; set; } = new List<Center>(); 
  }
}