using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancelacionDeSubprocesos
{
    // Cancelar mediante controlador de espera
    // Manual Reset Events, no jalo :(
    public class Ejemplo5
    {
        static ManualResetEvent mre;

        public static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            mre = new ManualResetEvent(true);
            ManualResetEventSlim mres = new ManualResetEventSlim(true);


            int eventThatSignaledIndex = WaitHandle.WaitAny(
                new WaitHandle[] { mre, token.WaitHandle },
                new TimeSpan(0, 0, 20));
            try
            {
                // mres is a ManualResetEventSlim
                mres.Wait(token);
            }
            catch (OperationCanceledException)
{
                // Throw immediately to be responsive. The
                // alternative is to do one more item of work,
                // and throw on next iteration, because
                // IsCancellationRequested will be true.
                Console.WriteLine("The wait operation was canceled.");
                throw;
            }
            Console.Write("Working...");

            // Simulating work.
            Thread.SpinWait(500000);

        }
    }
}
