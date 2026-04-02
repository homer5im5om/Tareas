using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace CancelacionDeSubprocesos
{
    // Escuchador web
    public class Ejemplo4
    {
        public static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            EmpezarPedidoWeb(cts.Token);
            // La cancelación de la web, hara que se cancele el pedido
            cts.Cancel();
        }

        static void EmpezarPedidoWeb(CancellationToken token)
        {
            var client = new HttpClient();
            token.Register(() =>
            {
                client.CancelPendingRequests();
                Console.WriteLine("Pedido cancelado!!");
            });
            Console.WriteLine("Empezando pedido...");
            client.GetStringAsync(new Uri("http://www.contoso.com"));
        }

    }
}
