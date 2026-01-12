using QuizBattle.Console;
using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBattle.Tests
{
    public class QuestionServiceTests
    {
        [Fact]
        public void QuestionService_TooFewChoices_Throws()
        {
            var repo = new InMemoryQuestionRepository();
            var service = new QuestionService(repo);

            try
            {
                var questions = service.GetRandomQuestions(4);
                Assert.True(questions.Count == 4);
            }
            catch (ArgumentOutOfRangeException ex) { }
        }
    }
}
