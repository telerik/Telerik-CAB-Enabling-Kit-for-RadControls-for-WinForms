//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// This class is the concrete implementation of a View in the Model-View-Presenter 
// pattern. Communication between the Presenter and this class is acheived through 
// an interface to facilitate separation and testability.
// Note that the Presenter generated by the same recipe, will automatically be created
// by CAB through [CreateNew] and bidirectional references will be added.
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.scsf.2007may/SCSF/html/02-09-010-ModelViewPresenter_MVP.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using FinanceApplicationCAB.Infrastructure.Interface;
using Telerik.Charting;
using System.Collections.Generic;
using Telerik.WinControls.UI;
using Telerik.Charting.Styles;
using System.Drawing;

namespace FinanceApplicationCAB.Infrastructure.Module
{
	public partial class StockChartView : UserControl, IStockChartView
	{
		public StockChartView()
		{
			InitializeComponent();

			this.radChart.PlotArea.XAxis.AutoScale = false;
            this.radChart.Skin = "Desert";
			//ChartAxisItem smallItem = new ChartAxisItem();
			//smallItem.Value = 1M;
			//smallItem.TextBlock.Text = "Small Cap";
			//this.radChart.PlotArea.XAxis.Items.Add(smallItem);
			//ChartAxisItem midItem = new ChartAxisItem();
			//midItem.Value = 2M;
			//midItem.TextBlock.Text = "Mid Cap";
			//this.radChart.PlotArea.XAxis.Items.Add(midItem);
			//ChartAxisItem largeItem = new ChartAxisItem();
			//largeItem.Value = 3M;
			//largeItem.TextBlock.Text = "Large Cap";
			//this.radChart.PlotArea.XAxis.Items.Add(largeItem);
			
            //this.SetChartSeriesProperties();
            //this.SetPlotAreaProperties(this.radChart.PlotArea);
            //this.SetChartTitleProperties(this.radChart.ChartTitle);
            //this.SetLegendAppearance(this.radChart.Legend.Appearance);

            //this.radChart.Appearance.Border.Width = 0.0f;
            //this.radChart.Appearance.Corners = new Corners(CornerType.Round, CornerType.Round, 
            //    CornerType.Round, CornerType.Round, 6);
            //this.radChart.Appearance.FillStyle.MainColor = Color.FromArgb(223, 228, 215);
            //this.radChart.Appearance.FillStyle.SecondColor = Color.FromArgb(193, 204, 181);
            //this.radChart.Appearance.FillStyle.FillType = FillType.Gradient;
            //this.radChart.Appearance.FillStyle.FillSettings.GradientMode = GradientFillStyle.Vertical;
		}

		#region Chart Appearance
		private void SetLegendAppearance(StyleExtendedLabel appearance)
		{
			appearance.Border.Color = Color.FromArgb(153, 255, 255, 255);
			appearance.Dimensions.Margins = new ChartMargins(new Unit(18, UnitType.Percentage), 
				new Unit(4, UnitType.Percentage), new Unit(1, UnitType.Pixel), new Unit(1, UnitType.Pixel));
			appearance.Dimensions.Paddings = new ChartPaddings(new Unit(2, UnitType.Pixel), 
				new Unit(8, UnitType.Pixel), new Unit(6, UnitType.Pixel), new Unit(3, UnitType.Pixel));
			appearance.Position.AlignedPosition = AlignedPositions.TopRight;
			appearance.FillStyle.MainColor = Color.FromArgb(188, 199, 175);
			appearance.FillStyle.SecondColor = Color.FromArgb(179, 191, 164);
			appearance.FillStyle.FillSettings.GradientMode = GradientFillStyle.Vertical;

			appearance.ItemTextAppearance.TextProperties.Color = Color.FromArgb(51, 51, 51);
		}

		private void SetChartTitleProperties(ChartTitle title)
		{
			//title.TextBlock.Text="Chart Title"
			title.TextBlock.Appearance.TextProperties.Color = Color.FromArgb(102, 182, 0);
			title.TextBlock.Appearance.TextProperties.Font = new Font("Arial", 14);

			title.Appearance.FillStyle.FillType = FillType.Solid;
			title.Appearance.FillStyle.MainColor = Color.Transparent;

			title.TextBlock.Appearance.Border.Visible = false;
			title.TextBlock.Appearance.Dimensions.Margins = new ChartMargins(new Unit(4, UnitType.Percentage),
				new Unit(10, UnitType.Pixel), new Unit(14, UnitType.Pixel), new Unit(7.6, UnitType.Percentage));
			title.TextBlock.Appearance.Position.AlignedPosition = AlignedPositions.TopLeft;
		}

		private void SetPlotAreaProperties(ChartPlotArea plotArea)
		{
			plotArea.YAxis.AxisMode = ChartYAxisMode.Extended;
			plotArea.YAxis.MaxItemsCount = 2;
			plotArea.YAxis.MaxValue = 1.5;
			plotArea.YAxis.Step = 0.5;
			
			plotArea.YAxis.Appearance.Color = Color.FromArgb(0, 255, 255, 255);
			plotArea.YAxis.Appearance.MajorTick.Color = Color.FromArgb(153, 255, 255, 255);
			plotArea.YAxis.Appearance.MinorTick.Color = Color.FromArgb(153, 255, 255, 255);
			plotArea.YAxis.Appearance.TextAppearance.TextProperties.Color = Color.FromArgb(81, 84, 79);
			plotArea.YAxis.Appearance.MajorGridLines.Color = Color.FromArgb(153, 255, 255, 255);
			plotArea.YAxis.Appearance.MinorGridLines.Color = Color.FromArgb(153, 255, 255, 255);
			
			//plotArea.YAxis.AxisLabel.TextBlock.Text="Y Axis"
			//plotArea.YAxis.AxisLabel.TextBlock.Visible="False"
			plotArea.YAxis.AxisLabel.TextBlock.Appearance.TextProperties.Color = Color.FromArgb(81, 84, 79);

			//TODO: check if these settings are correct
			plotArea.YAxis2.MaxValue = 6.0;
			plotArea.YAxis2.MinValue = 1.0;
			plotArea.YAxis2.Step = 1.0;

			plotArea.XAxis.MaxValue = 5.0;
			plotArea.XAxis.Step = 1.0;
			plotArea.XAxis.Appearance.Color = Color.FromArgb(228, 233, 223);
			plotArea.XAxis.Appearance.MajorTick.Color = Color.FromArgb(153, 255, 255, 255);
			plotArea.XAxis.Appearance.MajorTick.Visible = true;
			plotArea.XAxis.Appearance.TextAppearance.TextProperties.Color = Color.FromArgb(81, 84, 79);
			plotArea.XAxis.Appearance.MajorGridLines.Color = Color.FromArgb(153, 255, 255, 255);
			plotArea.XAxis.Appearance.MajorGridLines.Visible = false;

			//plotArea.XAxis.AxisLabel.TextBlock.Text="X Axis"
			//plotArea.XAxis.AxisLabel.TextBlock.Visible="False"
			plotArea.XAxis.AxisLabel.TextBlock.Appearance.TextProperties.Color = Color.FromArgb(81, 84, 79);

			plotArea.Appearance.Border.Color = Color.FromArgb(153, 255, 255, 255);
			plotArea.Appearance.Dimensions.Margins = new ChartMargins(new Unit(18, UnitType.Percentage), 
				new Unit(110, UnitType.Pixel), new Unit(12, UnitType.Percentage), new Unit(10, UnitType.Percentage));
			plotArea.Appearance.FillStyle.MainColor = Color.FromArgb(188, 199, 175);
			plotArea.Appearance.FillStyle.SecondColor = Color.FromArgb(174, 187, 159);
			plotArea.Appearance.FillStyle.FillSettings.GradientMode = GradientFillStyle.Vertical;
		}

		private void SetChartSeriesProperties()
		{
			ChartSeries series = this.radChart.GetSeries(0);

			series.Appearance.Border.Color = Color.FromArgb(170, 208, 246);
			series.Appearance.TextAppearance.TextProperties.Color = Color.FromArgb(81, 84, 79);
			
			series.Appearance.FillStyle.FillType = FillType.ComplexGradient;
			series.Appearance.FillStyle.FillSettings.ComplexGradient.AddRange(
				new GradientElement[] {
				new GradientElement(Color.FromArgb(222, 247, 255), 0.0f), 
				new GradientElement(Color.FromArgb(210, 242, 253), 0.5f),
				new GradientElement(Color.FromArgb(177, 217, 237), 1.0f)
			});
		}
		#endregion Chart Appearance

		protected override void OnLoad(EventArgs e)
		{
			_presenter.OnViewReady();
			base.OnLoad(e);
		}

		#region IStockChartView Members

		private List<StockItem> chartItems = null;

		public void SetChartItems(List<StockItem> chartItems)
		{
			this.chartItems = chartItems;

			ChartSeries series = this.radChart.Series[0]; //.GetChartSeries(0);
			series.Items.Clear();
			this.radChart.PlotArea.XAxis.Items.Clear();

			foreach (StockItem stockItem in chartItems)
			{
				ChartSeriesItem chartSeriesItem = new ChartSeriesItem();
				//chartSeriesItem.XValue = xValue;
				chartSeriesItem.YValue = Math.Round(stockItem.Change, 2);
				series.Items.Add(chartSeriesItem);

				this.radChart.PlotArea.XAxis.Items.Add(new ChartAxisItem(stockItem.ID));
			}

			this.radChart.Refresh();
		}

		public void SetChartTitle(string title)
		{
			this.radChart.ChartTitle.TextBlock.Text = title;
			this.radChart.Refresh();
		}

		private List<StockItem> hoveredItems = new List<StockItem>();
		//Color originalColor = Color.Empty;
		Color hoveredColor = Color.Red;

		public void SetHoveredItems(List<StockItem> hoveredItems)
		{
			if(this.chartItems == null)
			{
				return;
			}

			ChartSeries series = this.radChart.Series[0]; //.GetChartSeries(0);

			for (int i = 0; i < this.chartItems.Count; i++)
			{
				if (this.hoveredItems.Contains(this.chartItems[i]))
				{
					series.Items[i].Appearance.FillStyle.FillType = series.Appearance.FillStyle.FillType;
					series.Items[i].Appearance.FillStyle.MainColor = series.Appearance.FillStyle.MainColor;
					series.Items[i].Appearance.FillStyle.SecondColor = series.Appearance.FillStyle.SecondColor;
					//series.Items[i].Appearance.FillStyle.FillType = FillType.Gradient;
					//series.Items[i].Appearance.FillStyle.MainColor = this.originalColor;
				}
			}

			this.hoveredItems.Clear();
			this.hoveredItems.AddRange(hoveredItems);

			//this.originalColor = series.Items[0].Appearance.FillStyle.MainColor;
			for (int i = 0; i < this.chartItems.Count; i++)
			{
				if (this.hoveredItems.Contains(this.chartItems[i]))
				{
					series.Items[i].Appearance.FillStyle.MainColor = this.hoveredColor;
					series.Items[i].Appearance.FillStyle.FillType = FillType.Solid;
				}
			}

			this.radChart.Refresh();
		}

		#endregion
	}
}
