using System;
using Alpaca.Markets;

namespace SpeedTreader
{
    public class DipStrategy : Strategy
    {
        public DipStrategy(RestClient api, string symbol, string id, DateTime start_time, DateTime end_time) : base(api, symbol, id, start_time, end_time)
        {
        }
    }
}