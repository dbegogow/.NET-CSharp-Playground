file interface IQuestion
{
    int ProvideAnswer();
}

file class HiddenQuestion
{
    public int GenerateAnswer() => 60;
}

public class Question : IQuestion
{
    public int ProvideAnswer()
    {
        var hiddenQuestion = new HiddenQuestion();
        return hiddenQuestion.GenerateAnswer();
    }
}