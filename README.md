# ResultType
A Proposal of a Result Type in .NET

## When to use

This Result Type is useful in scenarios, where you want to mildly enforce checking, if the underlying method has executed successfully.
Additionally you will get a cleaner error-handling and reduce the risk of NullReferenceExceptions.

## Usage

### 1. Copy the class definitions from `Result.cs` and `ResultExtensions.cs` to your code base and make them accessible publicly.

### 2. Use `Result` or `Result<T>` as return value for all relevant functions:

```
private Result<Person> GetPersonFromDatabase(Guid id)
{
    try
    {
        return database.GetPersonById(id);
    }
    catch (Exception ex)
    {
        return ex;
    }
}
```

As you can see, the Result Type also has implicit casts implemented, that will reduce the amount of code needed.

### 3. Use the `.Check()` Extension Method to quickly check and throw.
### 4. Return any thrown exceptions from `.Check()` without worries about the StackTrace, as it gets preserved.

## Features

- Implicit castings allow you to directly return the desired type, which will be casted either to a positive `Result<T>`, when you return an object of type `<T>`, or to a negative `Result<T>` / `Result`, when you return an `Exception` object.
- The Extension Method `Check()` quickly allows you to check if a `Result` was successfull and throw the underlying exception (**while preserving the StackTrace**) if it was not.

```
try
{
    Result result = GetResult().Check();
}
catch (Exception ex)
{
    //Here the Exception includes the StackTrace of all underlying (rethrowing) Methods
    //See StackTraceTest for examples
    logger.LogError(ex.ToString());
}

```
- `Result<T>` is inheriting `Result`, so you can always cast to a normal `Result` if you don't need the payload `<T>` anymore.