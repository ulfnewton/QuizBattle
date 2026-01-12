using QuizBattle.Domain;

namespace QuizBattle.Console.Extensions
{
    // små extensions för att hålla Program.cs läsbar
    public static class QuestionViewExtensions
    {
        public static int ChoicesCount(this Question q) => q.Choices.Count;
        public static Choice ChoiceAt(this Question q, int index) => q.Choices[index];
    }
}
