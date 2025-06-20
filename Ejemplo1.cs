using System;
using System.Threading;

namespace CancelacionDeSubprocesos
{

    // Ejemplo de cancelacion con metodo.
    public class Ejemplo1()
    {
        public static void Main(string[] args)
        {

            // Creamos el token
            CancellationTokenSource cts = new CancellationTokenSource();


            // Pasaremos el token para que sea cancelable la operacion Y le damos un trabajo
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoSomeWork), cts.Token);

            // Le daremos 2.5s de delay
            Thread.Sleep(2500);

            // Cancelamos el token
            cts.Cancel();
            Console.WriteLine("Cancelacion del token en progreso... ");
            Thread.Sleep(2500);

            // La cancelacion toma su tiempo, dependiendo de factores como en que se estaba trabajando
            // con 2.5s debería haber sido suficiente para la cancelacion completa y para poder borrar el token.
            cts.Dispose();
        }

        // El metodo que será el trabajo del token, puede o no recibir un objeto
        static void DoSomeWork(object? obj)
        {
            // En este caso necesitamos que haya un objeto como parametro
            if (obj is null)
            {
                return;
            }

            // Transformamos nuestro objeto en el token que vamos a manipular
            CancellationToken token = (CancellationToken)obj;

            // Se hara una iteracion de 100,000 veces, y el token estara a la escucha
            // para decir en que momento el token fue cancelado
            for (int i = 0; i < 100000; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine($"En la iteracion {i + 1}, se solicito que el token se cancelara");

                    // En caso de querer o tener más progreso en proceso, aquí debería cancelarce todo
                    // para manejar de manera correcta cualquier tipo de documento abirto y evitar excepciones

                    //Terminado de hacer eso, se rompe el for y se sale
                    break;
                }

                // Con este metodo simulamos un "trabajo" para no hacer que se termine tan rapido el subproceso
                Thread.SpinWait(500000);
            }

        }
    }
}