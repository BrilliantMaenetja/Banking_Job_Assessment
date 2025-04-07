using Polly;
using Polly.Retry;

namespace Messaging.Shared.Policies
{
    public static class RetryPolicies
    {
        public static RetryPolicy GetBasicRetryPolicy(string operation = "RabbitMQ Operation")
        {
            return Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)), // 2s, 4s, 8s
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        Console.WriteLine($"[Retry {retryCount}] {operation} failed. Waiting {timeSpan.TotalSeconds} seconds. Error: {exception.Message}");
                    });
        }
    }
}
