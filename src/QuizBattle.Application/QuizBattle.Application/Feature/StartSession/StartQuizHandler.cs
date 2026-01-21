using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession
{
    public class StartQuizHandler
    {
        private readonly IQuestionService _questionService;
        private readonly ISessionRepository _sessions;

        public StartQuizHandler(IQuestionService questionService, ISessionRepository sessions)
        {
            _questionService = questionService;
            _sessions = sessions;
        }

        public async Task<StartQuizResult> HandleAsync(StartQuizCommand cmd, CancellationToken ct = default)
        {
            try
            {
                var questions = await _questionService.GetRandomQuestionsAsync(
                    count: cmd.QuestionCount,
                    category: cmd.Category,
                    difficulty: cmd.Difficulty,
                    ct: ct);

                var session = QuizSession.Create(cmd.QuestionCount);

                await _sessions.SaveAsync(session, ct);

                return new StartQuizResult(true, session.Id, questions.ToList());
            }
            catch (DomainException ex)
            {
                // Domänfel (t.ex. det finns inte tillräckligt många frågor att ställa)
                return StartQuizResult.Fail(ex.Message);
            }
            catch(ArgumentNullException ex)
            {
                // Validerings fel (t.ex. session är null)
                return StartQuizResult.Fail(ex.Message);
            }
        }
    }
}
