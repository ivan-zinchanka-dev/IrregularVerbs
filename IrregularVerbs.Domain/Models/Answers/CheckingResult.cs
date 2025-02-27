namespace IrregularVerbs.Domain.Models.Answers;

public struct CheckingResult
{
    public int AllAnswersCount { get; set; }
    public int CorrectAnswersCount { get; set; }
    
    public CheckingResult(int allAnswersCount, int correctAnswersCount) 
    {
        AllAnswersCount = allAnswersCount;
        CorrectAnswersCount = correctAnswersCount;
    }
}