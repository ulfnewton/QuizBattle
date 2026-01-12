using System;
using System.Collections.Generic;
using System.Text;

using QuizBattle.Domain;

namespace QuizBattle.Console
{
    public class InMemoryQuestionRepository : IQuestionRepository
    {
        public List<Question> GetAll() => QuestionUtils.SeedQuestions();
    }
}
