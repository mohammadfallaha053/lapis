namespace LapisApi.App.Services.Model
{
  public class Service
  {
    public int Id { get; set; }

    public string NameAr { get; set; }
    public string NameEn { get; set; }

    public string SimpleDescriptionAr { get; set; }
    public string SimpleDescriptionEn { get; set; }

    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }

    public int Order { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
  }
}