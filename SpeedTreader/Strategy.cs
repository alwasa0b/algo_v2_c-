using System;
using System.Threading.Tasks;
using Alpaca.Markets;

namespace SpeedTreader
{
    public abstract class Strategy
    {
        protected readonly RestClient api;
        public readonly string symbol;
        protected readonly string id;
        protected readonly DateTime start_Time;
        protected readonly DateTime end_Time;
        protected int quote_interval;

        protected int second_interval;
        protected int minute_interval;
        protected int trade_interval;
        private IStreamQuote CurrentQuote;

        public Strategy(RestClient api, string symbol, string id, DateTime start_time, DateTime end_time)
        {
            this.api = api;
            this.symbol = symbol;
            this.id = id;
            this.start_Time = start_time;
            this.end_Time = end_time;

            this.quote_interval = 1;
            this.second_interval = 0;
            this.minute_interval = 0;
            this.trade_interval = 0;
            this.symbol = symbol;
            // this.open_orders = {}
            // this.close_orders = {}
            // this.fills = {}
            // this.quote = None
        }

        internal void QuoteUpdate(IStreamQuote quote)
        {
            this.CurrentQuote = quote;
        }

        internal void TradeUpdates(ITradeUpdate trade)
        {
           
        }
    }
}