using QuizBattle.Domain;

namespace QuizBattle.Console;

public class QuestionService
{
    private readonly IQuestionRepository _repo;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _repo = questionRepository;
        EnsureValid();
    }

    public Question GetRandomQuestion()
    {
        var questions = _repo.GetAll();
        return questions[new Random().Next(questions.Count)];
    }

    public List<Question> GetRandomQuestions(int count = 3)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException("Count must not be zero or negative.");
        }

        var questions = _repo.GetAll();

        if (count >= questions.Count)
        {
            throw new ArgumentOutOfRangeException("Count must not be equal to or greater than question count.");
        }

        var result = questions
            .OrderBy(_ => Random.Shared.Next())  // pseudo-slumpordning
            .Take(count)
            .ToList();

        return result;
    }

    private void EnsureValid()
    {
        if (_repo is null)
        {
            throw new QuizBattle.Domain.DomainException("questionRepository must not be null");
        }
    }
}