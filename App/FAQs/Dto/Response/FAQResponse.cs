using LapisApi.App.MediaFiles.Dto;
namespace LapisApi.App.FAQs.Dto.Response;

public class FAQsResponse
{
  public int Id { get; set; }
  public string QuestionEn { get; set; }
  public string QuestionAr { get; set; }
  public string AnswerEn { get; set; }
  public string AnswerAr { get; set; }
  public DateTime CreatedAt { get; set; }
  public bool IsActive { get; set; } = true;
}