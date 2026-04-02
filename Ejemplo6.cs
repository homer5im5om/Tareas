using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancelacionDeSubprocesos
{
    public class Ejemplo6
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        public static void Main(string[] args)
        {

        }

        public void DoWork(CancellationToken externalToken)
        {
            // Create a new token that combines the internal and external tokens.
            this.internalToken = internalTokenSource.Token;
            this.externalToken = externalToken;
            using (CancellationTokenSource linkedCts =
            CancellationTokenSource.CreateLinkedTokenSource(internalToken, external‐
Token))
            {
                try
                {
                    DoWorkInternal(linkedCts.Token);
                }
                catch (OperationCanceledException)
                {
                    if (internalToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Operation timed out.");
                    }
                    else if (externalToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancelling per user request.");
                        externalToken.ThrowIfCancellationRequested();
                    }
                }
            }
        }
    }
}
