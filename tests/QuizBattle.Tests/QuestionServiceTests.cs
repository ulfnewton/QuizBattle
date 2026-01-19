using QuizBattle.Application.Interfaces;
using QuizBattle.Application.Services;
using QuizBattle.Console;
using QuizBattle.Infrastructure;
using QuizBattle.Infrastructure.Repositories;
using Xunit;
namespace QuizBattle.Tests
{
    public class QuestionServiceTests
    {
        [Fact]
        public async Task GetRandomQuestions_CountGreaterOrEqualToTotal_Throws()
        {
            var repo = new InMemoryQuestionRepository();
            IQuestionService service = new QuestionService(repo); // vi specificerar att vi har en IQuestionService för att slippa kasta om senare

            // För närvarande finns 3 seedade frågor. Begär 4 => ska kasta.

            // använd ThrowsAsync med await för att hantera det förväntade felet
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetRandomQuestionsAsync(4));            
        }
    }
}
