namespace ResultType.Tests;

public class UseCases
{
    [Test]
    public void SampleUseCase()
    {
        var result = CopyPerson();

        if (!result.Success)
            Console.WriteLine($"{nameof(CopyPerson)} was not successfull: {result.Message}");
    }

    [Test]
    public void SampleFailingUseCase()
    {
        var result = CopyPersonThrowing();

        if (!result.Success)
            Console.WriteLine($"{nameof(CopyPerson)} was not successfull: {result.Message}");
    }

    private Result CopyPerson()
    {
        try
        {
            var personResult = GetPersonFromDatabase()
                .Check();

            return InsertPersonToOtherDatabase(personResult.GetOk())
                .Check();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    private Result CopyPersonThrowing()
    {
        try
        {
            var personResult = GetPersonFromDatabaseThrowing()
                .Check();

            return InsertPersonToOtherDatabase(personResult.GetOk())
                .Check();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    private Result<Person> GetPersonFromDatabase()
    {
        try
        {
            return new Person("Test", 20);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    private Result<Person> GetPersonFromDatabaseThrowing()
    {
        try
        {
            throw new InvalidDataException("Person not available!");
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    private Result InsertPersonToOtherDatabase(Person person)
    {
        try
        {
            return Result.Ok;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}

internal record Person(string Name, int Age);