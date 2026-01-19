using QuizBattle.Domain.Entities;
using QuizBattle.Domain.Repositories;

namespace DefaultNamespace;

public class StratQuizHandler
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuizSessionRepository _sessionRepository;

    public StartQuizHandler(
        IQuestionRepository questionRepository,
        IQuizSessionRepository sessionRepository)
    {
        _questionRepository = questionRepository;
        _sessionRepository = sessionRepository;
    }

    public StartQuizResult Handle(StartQuizCommand command)
    {
        if (command.NumberOfQuestions <= 0)
            throw new ArgumentException("Antal frågor måste vara större än 0");

        var questions = _questionRepository
            .GetRandom(command.NumberOfQuestions);

        var session = new QuizSession(questions);

        _sessionRepository.Save(session);

        return new StartQuizResult(session.Id, questions);
    }
}