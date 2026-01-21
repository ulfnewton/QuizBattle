using System;
using System.Collections.Generic;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartSession;

public sealed record StartQuizResult(Guid SessionId, IReadOnlyList<Question> Questions);