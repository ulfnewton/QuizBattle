namespace QuizBattle.Console.Presentation
{
    /// <summary>
    /// Console-specifik presenter som ansvarar för att
    /// rendera frågor och läsa in användarens val.
    /// Lever endast i Console-klienten.
    /// </summary>
    public interface IConsoleQuestionPresenter
    {
        void DisplayQuestion(Question question, int number);
        int PromptForAnswer(Question question);
    }
}
