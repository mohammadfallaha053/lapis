using System.ComponentModel.DataAnnotations;
namespace LapisApi.App.FAQs.Dto.Request.Commands;

public class FAQsCreateCommand
{
  public string QuestionAr { get; set; }
  public string QuestionEn { get; set; }
  public string AnswerAr { get; set; }
  public string AnswerEn { get; set; }
}