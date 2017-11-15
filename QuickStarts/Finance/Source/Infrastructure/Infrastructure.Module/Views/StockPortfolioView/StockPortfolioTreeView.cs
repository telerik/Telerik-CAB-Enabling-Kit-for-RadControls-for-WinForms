using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Telerik.WinControls.UI;

namespace FinanceApplicationCAB.Infrastructure.Module
{
    [SmartPart]
	public partial class StockPortfolioTreeView : UserControl, IStockPortfolioView
	{
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private FinanceApplicationCAB.Infrastructure.Module.StockPortfolioViewPresenter presenter = null;
        private string textSearch;

		public StockPortfolioTreeView()
		{
			InitializeComponent();

            this.treeView.SelectedNodeChanged += new RadTreeView.RadTreeViewEventHandler(treeView_SelectedNodeChanged);
			this.treeView.NodeMouseEnter += new RadTreeView.TreeViewEventHandler(treeView_NodeMouseEnter);
			this.treeView.NodeMouseLeave += new RadTreeView.TreeViewEventHandler(treeView_NodeMouseLeave);
			this.treeView.MouseLeave += new EventHandler(treeView_MouseLeave);
		}

        void treeView_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            this.selectedItems.Clear();
            if (this.treeView.SelectedNode != null)
            {
                StockItem selectedStock = this.treeView.SelectedNode.Tag as StockItem;
                if (selectedStock != null)
                {
                    this.selectedItems.Add(selectedStock);
                }
            }

            this.OnSelectedItemsChanged();
        }

		protected override void OnLoad(EventArgs e)
		{
			this.presenter.OnViewReady();

			base.OnLoad(e);
		}

		void treeView_MouseLeave(object sender, EventArgs e)
		{
			this.hoveredItems.Clear();
			this.OnHoveredItemsChanged();
		}

		void treeView_NodeMouseLeave(object sender, RadTreeViewEventArgs tvea)
		{
			this.hoveredItems.Clear();
			this.OnHoveredItemsChanged();
		}

		void treeView_NodeMouseEnter(object sender, RadTreeViewEventArgs tvea)
		{
			this.hoveredItems.Add(tvea.Node.Tag as StockItem);
			this.OnHoveredItemsChanged();
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
			RadTreeNode node = new RadTreeNode(caption);
			node.Name = name;
			node.Expanded = true;

			this.treeView.Nodes.Add(node);
		}

		public void AddStockItem(StockItem item, string groupName)
		{
            textSearch = groupName;

            RadTreeNode parent = this.treeView.Find(this.FindByName);
            if (parent != null)
            {
                RadTreeNode node = new RadTreeNode(item.ID);
                node.Tag = item;
                parent.Nodes.Add(node);
            }

            textSearch = string.Empty;
		}

        private bool FindByName(RadTreeNode item)
        {
            if (item.Name == textSearch)
            {
                return true;
            }

            return false;
        }


		public event EventHandler<EventArgs> SelectedItemsChanged;

		private void OnSelectedItemsChanged()
		{
			if (this.SelectedItemsChanged != null)
			{
				this.SelectedItemsChanged(this, EventArgs.Empty);
			}
		}

		private List<StockItem> selectedItems = new List<StockItem>();

		public List<StockItem> SelectedItems
		{
			get
			{
				return this.selectedItems;
			}
		}

		public event EventHandler<EventArgs> HoveredItemsChanged;

		private void OnHoveredItemsChanged()
		{
			if (this.HoveredItemsChanged != null)
			{
				this.HoveredItemsChanged(this, EventArgs.Empty);
			}
		}

		private List<StockItem> hoveredItems = new List<StockItem>();

		public List<StockItem> HoveredItems
		{
			get
			{
				return this.hoveredItems;
			}
		}

		public void ClearSelection()
		{
			this.selectedItems.Clear();
			this.treeView.SelectedNodes.Clear();
		}

		public void SelectItem(StockItem stockItem)
		{
			RadTreeNode stockNode = null;

			RadTreeNode groupNode = this.treeView.Nodes[stockItem.Valuation.Replace(" ", "")];
			if (groupNode == null)
			{
				return;
			}
			foreach (RadTreeNode node in groupNode.Nodes)
			{
				if (node.Tag == stockItem)
				{
					stockNode = node;
					break;
				}
			}
			if (stockNode != null)
			{
				this.selectedItems.Add(stockItem);
				stockNode.Selected = true;
			}
		}

		#endregion
	}
}
