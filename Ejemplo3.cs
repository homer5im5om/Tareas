using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace CancelacionDeSubprocesos
{
    // Se usara otro ejemplo de tokens cancelables
    // Cancelacion por sondeo
    public class Ejemplo3
    {

        public static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;


        }

        // Esta forma de cancelacion es en caso de suprocesos largos y que hayan bastantes
        // Esta forma es en estilo "sondeo"
        static void NestedLoops(Rectangle rect, CancellationToken token)
        {
            for (int col = 0; col < rect.Width && !token.IsCancellationRequested; col++)
            {
                for (int row = 0; row < rect.Height; row++)
                {
                    // Simulamos trabajo
                    Thread.SpinWait(5_000);
                    Console.Write("{0},{1} ", col, row);
                }
            }

            // El for se detendra en cuanto termine con un elemento de subproceso.
            // si se termino por que hay un token cancelado, se terminara TODO el trabajo
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("\r\nLa operacion ha sido cancelada");
                Console.WriteLine("Presiona cualquier tecla para salir");
            }
        }


    }
    
}
