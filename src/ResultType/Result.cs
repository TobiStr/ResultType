// Copyright (c) 2022 Tobias Streng, MIT license

namespace ResultType;

/// <summary>
/// Used to determine, whether a method executes successfully or not. Has implicit cast implemented:
/// Return <see cref="Exception"/> (or inherited type) to return a Result with Success set to 'false'.
/// </summary>
public class Result
{
    protected readonly Exception? error;

    /// <summary>
    /// Indicates if the operation was successful.
    /// </summary>
    public bool Success { get; protected set; }

    /// <summary>
    /// Gets the message of the Exception, if available.
    /// </summary>
    public string Message { get => error?.Message ?? ""; }

    protected Result(bool success, Exception? error)
    {
        Success = success;
        this.error = error;
    }

    /// <summary>
    /// Gets the error payload of the failed result.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public Exception GetError() => error
        ?? throw new InvalidOperationException($"Error property for this Result not set.");

    /// <summary>
    /// Returns a positive Result.
    /// </summary>
    public static Result Ok
    {
        get => new Result(true, null);
    }

    /// <summary>
    /// Returns a negative Result with an Exception as payload.
    /// </summary>
    public static Result Error(Exception error)
    {
        return new Result(false, error);
    }

    /// <summary>
    /// Implicit cast from type Exception (or inherited) to Result with Success set to 'false'.
    /// </summary>
    public static implicit operator Result(Exception exception) =>
        new Result(false, exception);
}

/// <summary>
/// Used to determine, whether a method executes successfully or not. Has implicit casts implemented:
/// Return <typeparamref name="TPayload"/> to return a successful Result with this payload.
/// Return <see cref="Exception"/> (or inherited type) to return a Result with Success set to 'false'.
/// </summary>
/// <typeparam name="TPayload"></typeparam>
public sealed class Result<TPayload> : Result
    where TPayload : class
{
    private readonly TPayload? payload;

    private Result(TPayload? payload, Exception? error, bool success) : base(success, error)
    {
        this.payload = payload;
    }

    public Result(TPayload payload) : base(true, null)
    {
        this.payload = payload ?? throw new ArgumentNullException(nameof(payload));
    }

    public Result(Exception error) : base(false, error)
    {
    }

    /// <summary>
    /// Gets the underlying payload of the successful result.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public TPayload GetOk() => Success
        ? payload ?? throw new InvalidOperationException($"Payload for Result<{typeof(TPayload)}> was not set.")
        : throw new InvalidOperationException($"Operation for Result<{typeof(TPayload)}> was not successful.");

    /// <summary>
    /// Gets the error payload of the failed result.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public new Exception GetError() => error
        ?? throw new InvalidOperationException($"Error property for Result<{typeof(TPayload)}> not set.");

    /// <summary>
    /// Returns a positive Result with an object as payload.
    /// </summary>
    public new static Result<TPayload> Ok(TPayload payload)
    {
        return new Result<TPayload>(payload, null, true);
    }

    /// <summary>
    /// Returns a negative Result with an Exception as payload.
    /// </summary>
    public new static Result<TPayload> Error(Exception error)
    {
        return new Result<TPayload>(null, error, false);
    }

    /// <summary>
    /// Implicit cast from type <typeparamref name="TPayload"/> to Result with Success set to 'true'.
    /// </summary>
    public static implicit operator Result<TPayload>(TPayload payload) =>
        new(payload, null, true);

    /// <summary>
    /// Implicit cast from type Exception (or inherited) to Result<TPayload> with Success set to 'false'.
    /// </summary>
    public static implicit operator Result<TPayload>(Exception exception) =>
        new(exception);
}