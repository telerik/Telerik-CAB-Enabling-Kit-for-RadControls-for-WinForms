using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Layouts;
using System.Globalization;
using System.Reflection;
using Telerik.WinControls.UI;

namespace FinanceApplicationCAB.Infrastructure.Module.Views.StockDetailsView
{
	public partial class StockItemDetailsControl : UserControl
	{
		private StockDetailsItem lastTradeItem = new StockDetailsItem();
		private StockDetailsItem tradeTimeItem = new StockDetailsItem();
		private StockDetailsItem changeItem = new StockDetailsItem();
		private StockDetailsItem previousCloseItem = new StockDetailsItem();
		private StockDetailsItem openItem = new StockDetailsItem();
		private StockDetailsItem bidItem = new StockDetailsItem();
		private StockDetailsItem askItem = new StockDetailsItem();
		private StockDetailsItem volumeItem = new StockDetailsItem();
		private StockDetailsItem peItem = new StockDetailsItem();
		private StockDetailsItem epsItem = new StockDetailsItem();

		public StockItemDetailsControl()
		{
			InitializeComponent();

			this.radListBox.Font = new Font("Segoe UI", 8.0f);
            this.radListBox.ForeColor = Color.FromArgb(87, 84, 65);

			RadListBoxElement listBoxElement = this.radListBox.RootElement.Children[0] as RadListBoxElement;
			if (listBoxElement != null)
			{
				listBoxElement.HorizontalScrollState = ScrollState.AlwaysHide;
				listBoxElement.VerticalScrollState = ScrollState.AlwaysHide;


				ClassSelector borderSelector = new ClassSelector("RadScrollViewBorder");
				BorderPrimitive border = borderSelector.GetSelectedElements(listBoxElement).First.Value as BorderPrimitive;
				if (border != null)
				{
					border.Visibility = ElementVisibility.Collapsed;
				}
			}

			this.radListBox.Items.Add(this.lastTradeItem);
			this.radListBox.Items.Add(this.tradeTimeItem);
			this.radListBox.Items.Add(this.changeItem);
			this.radListBox.Items.Add(this.previousCloseItem);
			this.radListBox.Items.Add(this.openItem);
			this.radListBox.Items.Add(this.bidItem);
			this.radListBox.Items.Add(this.askItem);
			this.radListBox.Items.Add(this.volumeItem);
			this.radListBox.Items.Add(this.peItem);
			this.radListBox.Items.Add(this.epsItem);

			//set the labels
			this.lastTradeItem.Label = "Last Trade:";
			this.tradeTimeItem.Label = "Trade Time:";
			this.changeItem.Label = "Change:";
			this.previousCloseItem.Label = "Previous Close:";
			this.openItem.Label = "Open:";
			this.bidItem.Label = "Bid:";
			this.askItem.Label = "Ask:";
			this.volumeItem.Label = "Volume:";
			this.peItem.Label = "PE:";
			this.epsItem.Label = "EPS:";
		}

		private StockItemSummary summary = null;

		public StockItemSummary Summary
		{
			get
			{
				return this.summary;
			}
			set
			{
				if (this.summary != value)
				{
					this.summary = value;

					if (this.summary == null)
					{
						this.ClearValues();
					}
					else
					{
						this.UpdateValues();
					}
				}
			}
		}

		private void UpdateValues()
		{
			if (this.summary == null)
			{
				this.ClearValues();
				return;
			}

			this.lastTradeItem.Value = this.summary.LastTrade;
			this.tradeTimeItem.Value = this.summary.TradeTime;

			this.changeItem.Value = this.summary.Change;

			double change = double.Parse(this.summary.Change, CultureInfo.InvariantCulture);
            this.changeItem.ForeColor = (change > 0.0) ? Color.FromArgb(63, 157, 63) : Color.FromArgb(202, 67, 67);

			this.previousCloseItem.Value = this.summary.PreviousClose;
			this.openItem.Value = this.summary.Open;
			this.bidItem.Value = this.summary.Bid;
			this.askItem.Value = this.summary.Ask;
			this.volumeItem.Value = this.summary.Volume;
			this.peItem.Value = this.summary.PE;
			this.epsItem.Value = this.summary.EPS;

			Assembly containingAssembly = Assembly.GetAssembly(this.GetType());
			string imagePath = "FinanceApplicationCAB.Infrastructure.Module.Resources." + this.summary.SmallPic;
			Image image = Image.FromStream(containingAssembly.GetManifestResourceStream(imagePath));
			this.pictureBox.Image = image;
		}

		private void ClearValues()
		{
			this.lastTradeItem.Value = string.Empty;
			this.tradeTimeItem.Value = string.Empty;
			
			this.changeItem.Value = string.Empty;
			this.changeItem.ForeColor = Color.Black;

			this.previousCloseItem.Value = string.Empty;
			this.openItem.Value = string.Empty;
			this.bidItem.Value = string.Empty;
			this.askItem.Value = string.Empty;
			this.volumeItem.Value = string.Empty;
			this.peItem.Value = string.Empty;
			this.epsItem.Value = string.Empty;

			this.pictureBox.Image = null;
		}
	}

	public class StockDetailsItem : RadItem
	{
		private TextPrimitive labelPrimitive = null;
		private TextPrimitive valuePrimitive = null;
		private BoxLayout stripLayout = null;

		public string Label
		{
			get
			{
				return this.labelPrimitive.Text;
			}
			set
			{
				this.labelPrimitive.Text = value;
			}
		}

		public string Value
		{
			get
			{
				return this.valuePrimitive.Text;
			}
			set
			{
				this.valuePrimitive.Text = value;
			}
		}

		protected override void CreateChildElements()
		{
			//base.CreateChildElements();

			////remove the base items that we do not need
			//ImageAndTextLayoutPanel layoutPanel = (ImageAndTextLayoutPanel)base.Children[2];
			//StripLayoutPanel stripPanel = (StripLayoutPanel)layoutPanel.Children[1];
			//TextPrimitive itemText = (TextPrimitive)stripPanel.Children[0];
			//stripPanel.Children.Remove(itemText);

			//now place our items in the element hierarchy
			Font boldFont = new Font(this.Font, FontStyle.Bold);

			this.labelPrimitive = new TextPrimitive();
			this.labelPrimitive.Font = boldFont;
	//		this.labelPrimitive.AutoSize = false;
	//		this.labelPrimitive.Size = new Size(180, 18);
	//		this.labelPrimitive.Alignment = ContentAlignment.MiddleLeft;

			this.valuePrimitive = new TextPrimitive();
	//		this.valuePrimitive.Alignment = ContentAlignment.MiddleCenter;

			this.stripLayout = new BoxLayout();

			this.stripLayout.Children.Add(this.labelPrimitive);
			this.stripLayout.Children.Add(this.valuePrimitive);

			//stripPanel.Children.Insert(0, this.stripLayout);
			this.Children.Add(this.stripLayout);
		}
	}
}
