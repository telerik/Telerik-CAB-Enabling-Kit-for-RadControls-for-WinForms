using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace FinanceApplicationCAB.Infrastructure.Module.Views.StockDetailsView
{
	public partial class StockItemDetailsControl : UserControl
	{
        private RadListDataItem lastTradeItem = new RadListDataItem();
        private RadListDataItem tradeTimeItem = new RadListDataItem();
        private RadListDataItem changeItem = new RadListDataItem();
        private RadListDataItem previousCloseItem = new RadListDataItem();
        private RadListDataItem openItem = new RadListDataItem();
        private RadListDataItem bidItem = new RadListDataItem();
        private RadListDataItem askItem = new RadListDataItem();
        private RadListDataItem volumeItem = new RadListDataItem();
        private RadListDataItem peItem = new RadListDataItem();
        private RadListDataItem epsItem = new RadListDataItem();

		public StockItemDetailsControl()
		{
			InitializeComponent();

			this.radListBox.Font = new Font("Segoe UI", 8.0f);
            this.radListBox.ForeColor = Color.FromArgb(87, 84, 65);

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
            this.radListBox.VisualItemFormatting += new VisualListItemFormattingEventHandler(radListBox_VisualItemFormatting);

			//set the labels
			this.lastTradeItem.Text = "Last Trade:";
            this.tradeTimeItem.Text = "Trade Time:";
            this.changeItem.Text = "Change:";
            this.previousCloseItem.Text = "Previous Close:";
            this.openItem.Text = "Open:";
            this.bidItem.Text = "Bid:";
            this.askItem.Text = "Ask:";
            this.volumeItem.Text = "Volume:";
            this.peItem.Text = "PE:";
            this.epsItem.Text = "EPS:";
		}

        void radListBox_VisualItemFormatting(object sender, VisualItemFormattingEventArgs args)
        {
            args.VisualItem.Text = string.Format(args.VisualItem.Data.Text + " {0}", args.VisualItem.Data.Value); 
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
}
