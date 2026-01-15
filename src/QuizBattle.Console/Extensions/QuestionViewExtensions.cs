namespace QuizBattle.Console.Extensions
{
    /// <summary>
    /// Små extensions för att hålla Program.cs läsbar.
    /// </summary>
    public static class QuestionExtensions
    {
        public static int GetChoiceCount(this Question question) => question.Choices.Count;

        public static Choice GetChoiceAt(this Question question, int index) => question.Choices[index];
    }
}
