namespace LapisApi.App.Settings.Dto;

public class SettingResponse
{
  public string? PhoneNumber { get; set; } 
  public string? Email { get; set; } 
  public string? Address { get; set; }
  
  public string? AboutUs_Ar { get; set; } 
  public string? AboutUs_En { get; set; } 
    
  public string? ContactUs_Ar { get; set; } 
  public string? ContactUs_En { get; set; } 
    
  public string? FacebookUrl { get; set; }
  public string? InstagramUrl { get; set; }
  public string? TwitterUrl { get; set; }
  public string? YoutubeUrl { get; set; }
  public string? TiktokUrl { get; set; }
  
  public double? Latitude { get; set; }
  public double? Longitude { get; set; }
  
  public bool FAQsTabToggle { get; set; } = true;
  public bool ServicesTabToggle { get; set; } = true;
  public bool GalleriesTabToggle { get; set; } = true;
  public bool YouTubeGalleriesTabToggle { get; set; } = true;
  public bool TestimonialsTabToggle { get; set; } = true;
  public bool OurSpecialistsTabToggle { get; set; } = false;
}