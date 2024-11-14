using Polly;
using Polly.Retry;
using System.Diagnostics;

namespace ClimateSenseMAUI.Policies
{


    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }

        public ClientPolicy()
        {
            ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(
               res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

    }
}
