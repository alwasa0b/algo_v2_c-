using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alpaca.Markets;

namespace SpeedTreader
{
    public class AlpacaTrader
    {
        private readonly string key;
        private readonly string secret;
        private readonly string url;
        private RestClient client;
        private SockClient alpacaStreamingClient;
        private PolygonSockClient polygonStreamingClient;

        private Dictionary<string, List<Strategy>> stratgies = new Dictionary<string, List<Strategy>>();

        public AlpacaTrader(string key, string secret, string url)
        {
            this.key = key;
            this.secret = secret;
            this.url = url;
        }

        public async Task Initilize()
        {
            this.client = new Alpaca.Markets.RestClient(this.key, this.secret, this.url);
            this.alpacaStreamingClient = new Alpaca.Markets.SockClient(this.key, this.secret, this.url);
            this.polygonStreamingClient = new Alpaca.Markets.PolygonSockClient(this.key);
            this.alpacaStreamingClient.ConnectAndAuthenticateAsync().Wait();
            polygonStreamingClient.ConnectAndAuthenticateAsync().Wait();
            this.alpacaStreamingClient.OnTradeUpdate += HandleTradeUpdate;
            this.polygonStreamingClient.QuoteReceived += HandleQuoteUpdate;

            await AddStrategy(new DipStrategy(this.client, "TSLA", "TEST", DateTime.Now, DateTime.Now));
            await AddStrategy(new DipStrategy(this.client, "AAPL", "TEST", DateTime.Now, DateTime.Now));
        }

        public async Task AddStrategy(Strategy strategy)
        {
            this.polygonStreamingClient.SubscribeQuote(strategy.symbol);
            if (stratgies.ContainsKey(strategy.symbol))
                stratgies[strategy.symbol].Add(strategy);
            else
                stratgies[strategy.symbol] = new List<Strategy> { strategy };
        }

        public void HandleQuoteUpdate(IStreamQuote quote)
        {
            foreach (var element in stratgies[quote.Symbol])
            {
                element.QuoteUpdate(quote);
            }
        }

        internal void HandleTradeUpdate(ITradeUpdate trade)
        {
           foreach (var element in stratgies[trade.Order.Symbol])
            {
                element.TradeUpdates(trade);
            }
        }
    }
}
