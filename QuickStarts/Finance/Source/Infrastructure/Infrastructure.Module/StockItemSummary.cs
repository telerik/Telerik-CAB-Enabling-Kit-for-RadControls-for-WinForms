
namespace FinanceApplicationCAB.Infrastructure.Module
{
	public class StockItemSummary
	{
		private string lastTrade = string.Empty;
		private string tradeTime = string.Empty;
		private string change = string.Empty;
		private string previousClose = string.Empty;
		private string open = string.Empty;
		private string bid = string.Empty;
		private string ask = string.Empty;
		private string volume = string.Empty;
		private string pe = string.Empty;
		private string eps = string.Empty;
		private string smallPic = string.Empty;

		public string LastTrade
		{
			get
			{
				return this.lastTrade;
			}
			set
			{
				this.lastTrade = value;
			}
		}

		public string TradeTime
		{
			get
			{
				return this.tradeTime;
			}
			set
			{
				this.tradeTime = value;
			}
		}

		public string Change
		{
			get
			{
				return this.change;
			}
			set
			{
				this.change = value;
			}
		}

		public string PreviousClose
		{
			get
			{
				return this.previousClose;
			}
			set
			{
				this.previousClose = value;
			}
		}

		public string Open
		{
			get
			{
				return this.open;
			}
			set
			{
				this.open = value;
			}
		}

		public string Bid
		{
			get
			{
				return this.bid;
			}
			set
			{
				this.bid = value;
			}
		}

		public string Ask
		{
			get
			{
				return this.ask;
			}
			set
			{
				this.ask = value;
			}
		}

		public string Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				this.volume = value;
			}
		}

		public string PE
		{
			get
			{
				return this.pe;
			}
			set
			{
				this.pe = value;
			}
		}

		public string EPS
		{
			get
			{
				return this.eps;
			}
			set
			{
				this.eps = value;
			}
		}

		public string SmallPic
		{
			get
			{
				return this.smallPic;
			}
			set
			{
				this.smallPic = value;
			}
		}
	}
}
