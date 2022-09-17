namespace ResultType.Tests;

public class StackTraceTests
{
    [Test]
    public void StackTraceContainsThrowingMethodNameTest()
    {
        var result = Stack1();

        var error = result.GetError();

        Assert.That(error.StackTrace!.Contains(nameof(StackTraceTestInternal)));
        Assert.That(error is InvalidOperationException);
    }

    [Test]
    public void StackTraceContainsAllMethodNamesTest()
    {
        var result = RethrowStack1();

        var error = result.GetError();

        Assert.That(error.StackTrace!.Contains(nameof(StackTraceTestInternal)));
        Assert.That(error.StackTrace!.Contains(nameof(RethrowStack1)));
        Assert.That(error.StackTrace!.Contains(nameof(RethrowStack2)));
        Assert.That(error is InvalidOperationException);
    }

    private Result RethrowStack1()
    {
        try
        {
            var result = RethrowStack2();
            result.Check();
            return result;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    private Result RethrowStack2()
    {
        try
        {
            var result = StackTraceTestInternal();
            result.Check();
            return result;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    private Result Stack1() => Stack2();

    private Result Stack2() => StackTraceTestInternal();

    private Result StackTraceTestInternal()
    {
        try
        {
            throw new InvalidOperationException("Test");
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}