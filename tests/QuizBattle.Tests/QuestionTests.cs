using QuizBattle.Domain;

namespace QuizBattle.Tests;

public class QuestionTests
{
    [Fact]
    public void QuestionText_IsNull_ThrowsDomainException()
    {
        try
        {
            var question = new Question(null!, null!, null!, null!);
            Assert.Fail("Expected to throw DomainException with null parameters.");
        }
        catch(DomainException){ }
    }

    [Fact]
    public void QuestionChoices_LessThanThree_ThrowsDomainException()
    {

        try
        {
            var question = new Question(
                "Q1",
                "Question",
                new Choice[] { },
                ""
                );
            Assert.Fail("Must throw when less than three choices");

        }
        catch(DomainException) { }
    }
}
