using System.ComponentModel.DataAnnotations;
namespace LapisApi.App.FAQs.Dto.Request.Commands;

public class FAQsUpdateCommand
{
  public string QuestionAr { get; set; }
  public string QuestionEn { get; set; }
  public string AnswerAr { get; set; }
  public string AnswerEn { get; set; }
  
  public bool IsActive { get; set; }
}