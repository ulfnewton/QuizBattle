using System;

namespace QuizBattle.Application.Features.FinishSession;

public sealed record FinishQuizResult(int Score, int AnsweredCount, DateTime FinishedAtUtc);

