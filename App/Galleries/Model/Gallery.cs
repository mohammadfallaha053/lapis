namespace LapisApi.App.Galleries.Model
{
  public class Gallery
  {
    public int Id { get; set; }
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
  }
}