using System.Collections.Generic;

namespace QuizBattle.Application.Features.FinishSession
{
  public sealed record FinishSessionResult(
      int Score,
      int AnsweredCount
    );
}