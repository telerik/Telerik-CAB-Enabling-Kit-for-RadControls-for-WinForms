using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FinanceApplicationCAB.Infrastructure.Module
{
	class StockPortfolio
	{
		private Dictionary<string, StockItemCollection> valuationGroups = new Dictionary<string, StockItemCollection>();

		public Dictionary<string, StockItemCollection> ValuationGroups
		{
			get
			{
				return this.valuationGroups;
			}
		}

		public void LoadData(string fileName)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(fileName);
			XmlNodeList stockNodes = xmlDocument.SelectNodes("Portfolio/Equity");

			foreach (XmlNode stockNode in stockNodes)
			{
				this.AddXmlItem(stockNode);
			}
		}

		private void AddXmlItem(XmlNode node)
		{
			if (node == null)
			{
				return;
			}

			XmlElement element = node as XmlElement;

			string symbol = StockXmlHelper.GetEquitySymbol(element);

			if (this.FindStocksFromID(symbol) == null)
			{
				StockItem item = new StockItem();
				item.LoadFromXmlNode(node);

				if (this.valuationGroups.ContainsKey(item.Valuation) == false)
				{
					this.valuationGroups[item.Valuation] = new StockItemCollection();
				}
				this.valuationGroups[item.Valuation].Add(item);
			}
		}

		private StockItem FindStocksFromID(string id)
		{
			StockItem result = null;

			foreach (StockItemCollection stockItems in this.valuationGroups.Values)
			{
				result = stockItems.Find(delegate(StockItem stockItem) { return stockItem.ID == id; });
				if (result != null)
				{
					break;
				}
			}

			return result;
		}
	}
}
