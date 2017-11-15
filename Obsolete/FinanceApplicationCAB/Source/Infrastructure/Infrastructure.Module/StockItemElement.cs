using System;
using System.Collections.Generic;
using System.Text;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace FinanceApplicationCAB.Infrastructure.Module
{
	public class StockItemElement : RadItem
	{
		public static RadProperty PositiveChangeProperty = RadProperty.Register(
			"PositiveChange", typeof(bool), typeof(StockItemElement), new RadElementPropertyMetadata(
			false, ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.AffectsTheme));

		public static RadProperty IsSelectedProperty = RadProperty.Register(
			"IsSelected", typeof(bool), typeof(StockItemElement), new RadElementPropertyMetadata(
			false, ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.AffectsTheme));

		public static RadProperty SelectedItemProperty = RadProperty.Register(
			"SelectedItemProperty", typeof(StockItemElement), typeof(StockItemElement), new RadElementPropertyMetadata(
			null, ElementPropertyOptions.None));

		public static RadProperty HoveredItemProperty = RadProperty.Register(
			"HoveredItemProperty", typeof(StockItemElement), typeof(StockItemElement), new RadElementPropertyMetadata(
			null, ElementPropertyOptions.None));

		private StockItem stockItem = null;

		static StockItemElement()
		{
			ThemeResolutionService.RegisterThemeFromStorage(ThemeStorageType.Resource, "FinanceApplicationCAB.Infrastructure.Module.StockItemDefault.xml");
		}

		public StockItemElement()
		{
			this.UseNewLayoutSystem = true;
		}

		public StockItem StockItem
		{
			get
			{
				return this.stockItem;
			}
			set
			{
				if (this.stockItem != value && value != null)
				{
					this.stockItem = value;

					this.stockNameText.Text = this.stockItem.ID;
					this.companyNameText.Text = this.stockItem.Description;
					this.priceText.Text = this.stockItem.Price.ToString("f3");
					this.shareText.Text = this.stockItem.Shares.ToString();
					this.totalText.Text = this.stockItem.TotalValue.ToString("f3");
				}
			}
		}

		FillPrimitive fillPrimitive = null;
		BorderPrimitive borderPrimitive = null;

		TextPrimitive stockNameText = null;
		TextPrimitive companyNameText = null;
		TextPrimitive priceText = null;
		TextPrimitive shareText = null;
		TextPrimitive totalText = null;

		protected override void CreateChildElements()
		{
			this.Margin = new Padding(2);

			this.fillPrimitive = new FillPrimitive();
			this.fillPrimitive.Class = "StockItemFill";
			this.fillPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
			this.fillPrimitive.ZIndex = -1;
			this.Children.Add(this.fillPrimitive);

			this.borderPrimitive = new BorderPrimitive();
			this.borderPrimitive.Class = "StockItemBorder";
			this.borderPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
			this.borderPrimitive.ZIndex = 0;
			this.Children.Add(this.borderPrimitive);

			Font boldFont = new Font(this.Font, FontStyle.Bold);

			this.stockNameText = new TextPrimitive();
			this.stockNameText.Class = "StockItemText";
			this.stockNameText.Font = boldFont;
			this.stockNameText.Text = "Stock";

			this.companyNameText = new TextPrimitive();
			this.companyNameText.Class = "StockItemText";
			this.companyNameText.Text = "Company";
			this.companyNameText.BypassLayoutPolicies = true;

			this.priceText = new TextPrimitive();
			this.priceText.Class = "StockItemText";
			this.priceText.Font = boldFont;
			this.priceText.Text = "0.0";

			this.shareText = new TextPrimitive();
			this.shareText.Class = "StockItemText";
			this.shareText.Text = "0";

			this.totalText = new TextPrimitive();
			this.totalText.Class = "StockItemText";
			this.totalText.Font = boldFont;
			this.totalText.Text = "0.0";

			this.Children.Add(this.stockNameText);
			this.Children.Add(this.companyNameText);
			this.Children.Add(this.priceText);
			this.Children.Add(this.shareText);
			this.Children.Add(this.totalText);
		}

		protected override SizeF MeasureOverride(SizeF availableSize)
		{
            base.MeasureOverride(availableSize);
			float preferedHeight = stockNameText.DesiredSize.Height + priceText.DesiredSize.Height;
			return new SizeF(availableSize.Width, preferedHeight + 5);
		}

		protected override SizeF ArrangeOverride(SizeF finalSize)
		{
			SizeF result = base.ArrangeOverride(finalSize);


			this.fillPrimitive.Arrange(new RectangleF(0, 0, this.DesiredSize.Width - this.Margin.Left, this.DesiredSize.Height - this.Margin.Bottom));
			this.borderPrimitive.Arrange(new RectangleF(0, 0, this.DesiredSize.Width, this.DesiredSize.Height));

			SizeF stockNameTextSize = this.stockNameText.DesiredSize;
			SizeF priceTextSize = this.priceText.DesiredSize;

			int padding = 5;
			int halfWidth = (int)this.DesiredSize.Width / 2 - padding;

			this.stockNameText.Arrange( new RectangleF(5, 5, halfWidth, stockNameTextSize.Height));
			this.shareText.Arrange( new RectangleF(5 + halfWidth, 5, halfWidth, stockNameTextSize.Height));

			this.companyNameText.Arrange (new RectangleF(5, 5 + stockNameTextSize.Height, halfWidth, priceTextSize.Height));
			this.priceText.Arrange (new RectangleF(5 + halfWidth, 5 + stockNameTextSize.Height, halfWidth / 2, priceTextSize.Height));
			this.totalText.Arrange( new RectangleF(5 + halfWidth + halfWidth / 2, 5 + stockNameTextSize.Height,
				halfWidth / 2, priceTextSize.Height));

            return result;
		}

		protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
		{
			if (e.Property == StockItemElement.IsSelectedProperty || e.Property == StockItemElement.PositiveChangeProperty)
			{
				for (int i = 0; i < this.Children.Count; i++)
				{
					this.Children[i].SetValue(e.Property, e.NewValue);
				}
			}

			base.OnPropertyChanged(e);
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);

			this.SetValue(StockItemElement.IsSelectedProperty, true);

			RadPanelBar panelBar = this.ElementTree.Control as RadPanelBar;
			if (panelBar != null)
			{
				panelBar.PanelBarElement.SetValue(StockItemElement.SelectedItemProperty, this);
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			RadPanelBar panelBar = this.ElementTree.Control as RadPanelBar;
			if (panelBar != null)
			{
				panelBar.PanelBarElement.SetValue(StockItemElement.HoveredItemProperty, this);
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			RadPanelBar panelBar = this.ElementTree.Control as RadPanelBar;
			if (panelBar != null)
			{
				panelBar.PanelBarElement.SetValue(StockItemElement.HoveredItemProperty, null);
			}
		}
	}
}
