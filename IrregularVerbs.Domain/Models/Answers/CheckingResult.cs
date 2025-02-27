namespace IrregularVerbs.Domain.Models.Answers;

public readonly struct CheckingResult
{
    public int AllAnswersCount { get; }
    public int CorrectAnswersCount { get; }

    public CheckingResult()
    {
        AllAnswersCount = CorrectAnswersCount = 0;
    }
    
    public CheckingResult(int allAnswersCount, int correctAnswersCount) 
    {
        AllAnswersCount = allAnswersCount;
        CorrectAnswersCount = correctAnswersCount;
    }
}