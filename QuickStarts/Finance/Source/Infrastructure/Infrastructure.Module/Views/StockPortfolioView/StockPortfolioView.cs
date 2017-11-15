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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Telerik.WinControls.UI;

namespace FinanceApplicationCAB.Infrastructure.Module
{
    [SmartPart]
	public partial class StockPortfolioView : UserControl, IStockPortfolioView
	{
        private List<StockItemElement> hoveredStockElements = new List<StockItemElement>();
        private List<StockItem> selectedItems = new List<StockItem>();
        private List<StockItem> hoveredItems = null;
        private StockPortfolioViewPresenter presenter = null;
        private Font font;

        public event EventHandler<EventArgs> SelectedItemsChanged;
        public event EventHandler<EventArgs> HoveredItemsChanged;

		public StockPortfolioView()
		{
			InitializeComponent();

            this.radPageView.ViewMode = PageViewMode.Stack;
		}

		private void OnSelectedItemsChanged()
		{
			if (this.SelectedItemsChanged != null)
			{
				this.SelectedItemsChanged(this, EventArgs.Empty);
			}
		}

		public List<StockItem> SelectedItems
		{
			get
			{
				return this.selectedItems;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			this.presenter.OnViewReady();
			base.OnLoad(e);
		}

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public StockPortfolioViewPresenter Presenter
        {
            set
            {
                this.presenter = value;
                this.presenter.View = this;
            }
        }

		#region IStockPortfolioView Members

		public void AddValuationGroup(string caption, string name)
		{
            RadPageViewPage page = new RadPageViewPage();
			page.Text = caption;
			page.Name = name;
			this.radPageView.Pages.Add(page);

            RadGridView gridView = CreateGridView();
            page.Tag = gridView.DataSource;
            page.Controls.Add(gridView);
		}

        private RadGridView CreateGridView()
        {
            RadGridView gridView = new RadGridView();
            gridView.Dock = DockStyle.Fill;
            gridView.ShowColumnHeaders = false;
            gridView.AllowAddNewRow = false;
            gridView.EnableGrouping = false;
            gridView.AutoGenerateColumns = false;
            gridView.ReadOnly = true;
            gridView.SelectionMode = GridViewSelectionMode.FullRowSelect;

            gridView.Columns.Add("ID", "ID", "ID");
            gridView.Columns.Add("Shares", "Shares", "Shares");
            gridView.Columns.Add("Description", "Description", "Description");
            gridView.Columns.Add("Price", "Price", "Price");
            gridView.Columns.Add("TotalValue", "TotalValue", "TotalValue");
            gridView.Columns[0].Width = 80;
            gridView.Columns[2].Width = 80;
            gridView.Columns[3].FormatString = "{0:C}";
            gridView.Columns[4].Width = 80;
            gridView.Columns[4].FormatString = "{0:C}";

            gridView.CellFormatting += new CellFormattingEventHandler(gridView_CellFormatting);
            gridView.CurrentRowChanged += new CurrentRowChangedEventHandler(gridView_CurrentRowChanged);

            HtmlViewDefinition htmlView = new HtmlViewDefinition();
            htmlView.RowTemplate.Rows.Add(new RowDefinition());
            htmlView.RowTemplate.Rows[0].Cells.Add(new CellDefinition("ID"));
            htmlView.RowTemplate.Rows[0].Cells.Add(new CellDefinition("Shares"));
            htmlView.RowTemplate.Rows.Add(new RowDefinition());
            htmlView.RowTemplate.Rows[1].Cells.Add(new CellDefinition("Description"));
            htmlView.RowTemplate.Rows[1].Cells.Add(new CellDefinition("Price"));
            htmlView.RowTemplate.Rows[1].Cells.Add(new CellDefinition("TotalValue"));
            gridView.ViewDefinition = htmlView;

            gridView.DataSource = new BindingList<StockItem>();
            return gridView;
        }

        void gridView_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            this.ClearSelection();

            StockItem stockItem = e.CurrentRow.DataBoundItem as StockItem;
            this.selectedItems.Add(stockItem);

            OnSelectedItemsChanged();
        }

        void gridView_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (this.font == null)
            {
                this.font = new Font(e.CellElement.Font, FontStyle.Bold);
            }

            StockItem stockItem = (StockItem)e.CellElement.RowInfo.DataBoundItem;
            e.CellElement.DrawText = true;
            e.CellElement.DrawBorder = true;
            e.CellElement.BorderLeftWidth = 0;
            e.CellElement.BorderTopWidth = 0;
            e.CellElement.BorderRightWidth = 0;

            if (e.ColumnIndex == 0 || e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                e.CellElement.Font = this.font;
            }
            
            if (stockItem.Change > 0)
            {
                e.CellElement.ForeColor = Color.Green;
            }
            else
            {
                e.CellElement.ForeColor = Color.Red;
            }

            if (e.ColumnIndex < 2)
            {
                e.CellElement.BorderBottomWidth = 0;
            }
        }

		public void AddStockItem(StockItem item, string groupName)
		{
            RadPageViewPage parent = this.radPageView.Pages[groupName];
            if (parent != null)
            {
                BindingList<StockItem> items = parent.Tag as BindingList<StockItem>;
                items.Add(item);
            }
		}

		private void OnHoveredItemsChanged()
		{
			if(this.HoveredItemsChanged != null)
			{
				this.HoveredItemsChanged(this, EventArgs.Empty);
			}
		}

		public List<StockItem> HoveredItems
		{
			get
			{
				if (this.hoveredItems == null)
				{
					this.hoveredItems = new List<StockItem>();
					for (int i = 0; i < this.hoveredStockElements.Count; i++)
					{
						this.hoveredItems.Add(this.hoveredStockElements[i].StockItem);
					}
				}

				return this.hoveredItems;
			}
		}

		public void ClearSelection()
		{
			this.selectedItems.Clear();
			foreach (RadPageViewPage page in this.radPageView.Pages)
			{
                RadGridView gridView = page.Controls[0] as RadGridView;
                foreach (GridViewRowInfo row in gridView.ChildRows)
                {
                    row.IsSelected = false;
                }
			}
		}

		public void SelectItem(StockItem stockItem)
		{
			string groupName = stockItem.Valuation.Replace(" ", "");
			RadPageViewPage page = this.radPageView.Pages[groupName];
            if (page != null)
            {
                RadGridView gridView = page.Controls[0] as RadGridView;
                foreach (GridViewRowInfo row in gridView.ChildRows)
                {
                    row.IsSelected = true;
                }
            }
		}

		#endregion
	}
}
