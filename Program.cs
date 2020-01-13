using System;
using System.Threading.Tasks;
using SpeedTreader;

namespace algo_v2_c_
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new AlpacaTrader("PKAQKOMY2D90SOX6YVRO", "xO/FKKOQuGfS2RTy/XNGzGwumzVXDxLO0IndrFjD", "https://paper-api.alpaca.markets");
            await client.Initilize();
            Console.Read();
        }
    }
}
