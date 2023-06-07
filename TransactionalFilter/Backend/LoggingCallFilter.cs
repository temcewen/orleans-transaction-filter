using Microsoft.Extensions.Logging;
using System.Transactions;

namespace TransactionalFilter.Backend
{
    public class LoggingCallFilter : IIncomingGrainCallFilter
    {

        public LoggingCallFilter()
        {
            // TODO: Determine environment (debug/stage/production).
        }
        public async Task Invoke(IIncomingGrainCallContext context)
        {
            try
            {
                if (context.Grain.GetType() == typeof(MessageGrain))
                {
                    Console.WriteLine("MessageGrain call invoked");
                }
                await context.Invoke();
            }
            catch (Exception exception)
            {
                // This code is never reached.

                Console.WriteLine("Exception handled here");
            }
        }
    }
}
