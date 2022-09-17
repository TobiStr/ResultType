namespace ResultType.Tests;

public class ResultTests
{
    [Test]
    public void GetOkTest()
    {
        var okResult = Result.Ok;
        var okResult2 = Result<string>.Ok("Test");

        Assert.True(okResult.Success);
        Assert.True(okResult2.Success);
    }

    [Test]
    public void GetErrorTest()
    {
        var exception = new Exception("Test");
        var errorResult = Result.Error(exception);
        var errorResult2 = Result<string>.Error(exception);

        Assert.False(errorResult.Success);
        Assert.False(errorResult2.Success);
    }

    [Test]
    public void ImplicitCastTest()
    {
        var exception = new Exception("Test");
        Result errorResult = exception;
        Result<string> errorResult2 = exception;
        Result<string> okResult2 = "Test";

        Assert.False(errorResult.Success);
        Assert.False(errorResult2.Success);
        Assert.True(okResult2.Success);
    }

    [Test]
    public void ThrowTest()
    {
        var okResult = Result.Ok;
        var okResult2 = Result<string>.Ok("Test");

        var exception = new Exception("Test");
        Result errorResult = exception;
        Result<string> errorResult2 = exception;

        Assert.DoesNotThrow(() => okResult.Check());
        Assert.DoesNotThrow(() => okResult2.Check());

        Assert.Throws(exception.GetType(), () => errorResult.Check());
        Assert.Throws(exception.GetType(), () => errorResult2.Check());
    }
}