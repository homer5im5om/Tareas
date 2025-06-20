using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CancelacionDeSubprocesos
{
    // Se usara otro ejemplo de tokens cancelables
    // Cancelacion por metodo del objeto propio
    public class Ejemplo2
    {
        
        public static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            // Este es un ejemplo de token cancelable, pero con su propio metodo
            // para cancelar procesos
            var obj1 = new ObjetoCancelable("1");
            var obj2 = new ObjetoCancelable("2");
            var obj3 = new ObjetoCancelable("3");

            // Registramos el metodo de cancelacion del propio objeto.
            token.Register(() => obj1.Cancel());
            token.Register(() => obj2.Cancel());
            token.Register(() => obj3.Cancel());

            // Cancelamos el token que tenemos
            cts.Cancel();

            // Liberamos o terminamos cosas pendientes

            //Boramos el objeto
            cts.Dispose();
        }


    }

    class ObjetoCancelable
    {
        public string id;

        public ObjetoCancelable(string id) {
            this.id = id;
        }

        public void Cancel() {
            Console.WriteLine($"Objeto {id} ha sido cancelado.");
            // Si es necesario, terminar la cancelación aquí.
        }
    }
}
