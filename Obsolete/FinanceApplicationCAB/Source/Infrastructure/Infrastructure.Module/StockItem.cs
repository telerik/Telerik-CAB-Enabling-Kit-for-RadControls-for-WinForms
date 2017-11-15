using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Xml.XPath;
using System.Globalization;

namespace FinanceApplicationCAB.Infrastructure.Module
{
	public class StockItem : INotifyPropertyChanged, IComparable<StockItem>
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public string ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
				this.NotifyPropertyChanged("ID");
			}
		}

		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
				this.NotifyPropertyChanged("Description");
			}
		}

		public double Price
		{
			get
			{
				if (this.shares <= 0)
				{
					return 0.0;
				}
				return this.totalValue / this.shares;
			}
		}

		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
				this.NotifyPropertyChanged("Title");
			}
		}

		private int shares = 0;

		public int Shares
		{
			get
			{
				return this.shares;
			}
			set
			{
				this.shares = value;
				this.NotifyPropertyChanged("Shares");
				this.NotifyPropertyChanged("Price");
			}
		}

		private double totalValue = 0.0;

		public double TotalValue
		{
			get
			{
				return this.totalValue;
			}
			set
			{
				this.totalValue = value;
				this.NotifyPropertyChanged("TotalValue");
				this.NotifyPropertyChanged("Price");
			}
		}

		private string valuation = string.Empty;

		public string Valuation
		{
			get
			{
				return this.valuation;
			}
			set
			{
				this.valuation = value;
				this.NotifyPropertyChanged("Valuation");
			}
		}

		private string sector = string.Empty;

		public string Sector
		{
			get
			{
				return this.sector;
			}
			set
			{
				this.sector = value;
				this.NotifyPropertyChanged("Sector");
			}
		}

		private string newsFeed = string.Empty;

		public string NewsFeed
		{
			get
			{
				return this.newsFeed;
			}
			set
			{
				this.newsFeed = value;
				this.NotifyPropertyChanged("NewsFeed");
			}
		}

		private double change;

		public double Change
		{
			get
			{
				return this.change;
			}
			set
			{
				this.change = value;
				this.NotifyPropertyChanged("Change");
			}
		}

		private StockItemSummary summary = new StockItemSummary();

		public StockItemSummary Summary
		{
			get
			{
				return this.summary;
			}
		}

		private void NotifyPropertyChanged(string propName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}

		private string id = "Default ID";
		private string title = "Default Title";
		private string description = "Default Description";

		public void LoadFromXmlNode(XmlNode node)
		{
			if(node == null)
			{
				return;
			}

			XPathNavigator navigator = node.CreateNavigator();
			this.ID = navigator.Evaluate("string(Symbol/text())").ToString();
			this.Description = navigator.Evaluate("string(Name/text())").ToString();
			this.Shares = int.Parse(navigator.Evaluate("string(Shares/text())").ToString(), CultureInfo.InvariantCulture);
			this.TotalValue = double.Parse(navigator.Evaluate("string(TotalValue/text())").ToString(), CultureInfo.InvariantCulture);
			this.Valuation = navigator.Evaluate("string(Valuation/text())").ToString();
			this.Sector = navigator.Evaluate("string(Sector/text())").ToString();
			this.NewsFeed = node["NewsFeed"].InnerXml;
			this.Change = double.Parse(navigator.Evaluate("string(Summary/Change/text())").ToString(), CultureInfo.InvariantCulture);

			this.LoadSummaryFromXmlNode(node);
		}

		private void LoadSummaryFromXmlNode(XmlNode node)
		{
			this.summary.Change = this.Change.ToString("f5");

			XPathNavigator navigator = node.CreateNavigator();

			this.summary.LastTrade = double.Parse(navigator.Evaluate("string(Summary/LastTrade/text())").ToString(), 
				CultureInfo.InvariantCulture).ToString("f5");
			this.summary.TradeTime = navigator.Evaluate("string(Summary/TradeTime/text())").ToString();
			this.summary.PreviousClose = navigator.Evaluate("string(Summary/PreviousClose/text())").ToString();
			this.summary.Open = double.Parse(navigator.Evaluate("string(Summary/Open/text())").ToString(), 
				CultureInfo.InvariantCulture).ToString("f5");
			this.summary.Bid = navigator.Evaluate("string(Summary/Bid/text())").ToString();
			this.summary.Ask = navigator.Evaluate("string(Summary/Ask/text())").ToString();
			this.summary.Volume = navigator.Evaluate("string(Summary/Volume/text())").ToString();
			this.summary.PE = navigator.Evaluate("string(Summary/PE/text())").ToString();
			this.summary.EPS = double.Parse(navigator.Evaluate("string(Summary/EPS/text())").ToString(), 
				CultureInfo.InvariantCulture).ToString("f5");
			this.summary.SmallPic = node["Chart2D"].Attributes["SmallPic"].Value;
		}

		#region IComparable<StockItem> Members

		int IComparable<StockItem>.CompareTo(StockItem other)
		{
			return this.id.CompareTo(other.id);
		}

		#endregion
	}
}
