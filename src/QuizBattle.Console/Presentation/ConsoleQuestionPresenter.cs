namespace QuizBattle.Console.Presentation
{
    /// <summary>
    /// Ren Console-implementation som skriver/läser via System.Console.
    /// </summary>
    public sealed class ConsoleQuestionPresenter : IConsoleQuestionPresenter
    {
        public void DisplayQuestion(Question question, int number)
        {
            System.Console.WriteLine($"Fråga {number}: {question.Text}");

            for (var i = 0; i < question.Choices.Count; i++)
            {
                var choice = question.Choices[i];
                System.Console.WriteLine($"  {i + 1}. {choice.Text}");
            }
        }

        public int PromptForAnswer(Question question)
        {
            System.Console.Write($"Ditt svar (1–{question.GetChoiceCount()}): ");

            int pick;
            while (!int.TryParse(System.Console.ReadLine(), out pick) ||
                   pick < 1 ||
                   pick > question.GetChoiceCount())
            {
                System.Console.Write("Ogiltigt val. Försök igen: ");
            }

            return pick;
        }
    }
}
