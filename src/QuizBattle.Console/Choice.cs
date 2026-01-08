namespace QuizBattle.Console
{
    public class Choice
    {
        public Choice(string code, string option)
        {
            Code = code;
            Option = option;
        }

        public string Code { get; }
        public string Option { get; }
    }
}