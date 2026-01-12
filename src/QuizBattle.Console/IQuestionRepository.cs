using QuizBattle.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizBattle.Console
{
    public interface IQuestionRepository
    {
        List<Question> GetAll();
    }
}
