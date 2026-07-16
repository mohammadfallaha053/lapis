namespace LapisApi.App.FAQs.Model
{
  public class FAQ
  {
    public int Id { get; set; }
    public string QuestionAr { get; set; }
    public string QuestionEn { get; set; }
    public string AnswerAr { get; set; }
    public string AnswerEn { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
  }
}