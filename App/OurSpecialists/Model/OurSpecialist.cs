namespace LapisApi.App.OurSpecialists.Model
{
  public class OurSpecialist
  {
    public int Id { get; set; }

    public string NameAr { get; set; } 
    public string NameEn { get; set; } 
    
    public string DescriptionAr { get; set; } 
    public string DescriptionEn { get; set; } 
    
    public string SpecialistAr { get; set; }
    public string SpecialistEn { get; set; } 
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
  }
}