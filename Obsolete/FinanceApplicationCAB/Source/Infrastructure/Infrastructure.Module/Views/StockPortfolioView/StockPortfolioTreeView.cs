using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace FinanceApplicationCAB.Infrastructure.Module
{
	public partial class StockPortfolioTreeView : UserControl, IStockPortfolioView
	{
		public StockPortfolioTreeView()
		{
			InitializeComponent();

			this.treeView.Selected += new EventHandler(treeView_Selected);
			this.treeView.NodeMouseEnter += new RadTreeView.TreeViewEventHandler(treeView_NodeMouseEnter);
			this.treeView.NodeMouseLeave += new RadTreeView.TreeViewEventHandler(treeView_NodeMouseLeave);
			this.treeView.MouseLeave += new EventHandler(treeView_MouseLeave);

			this.treeView.ThemeName = "Desert";
		}

		protected override void OnLoad(EventArgs e)
		{
			_presenter.OnViewReady();
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

		void treeView_Selected(object sender, EventArgs e)
		{
			this.selectedItems.Clear();
			if(this.treeView.SelectedNode != null)
			{
				StockItem selectedStock = this.treeView.SelectedNode.Tag as StockItem;
				if(selectedStock != null)
				{
					this.selectedItems.Add(selectedStock);
				}
			}

			this.OnSelectedItemsChanged();
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
			RadTreeNode[] foundNodes = this.treeView.Nodes.Find(groupName, false);
			if (foundNodes.Length < 1)
			{
				return;
			}

			RadTreeNode node = new RadTreeNode(item.ID);
			node.Tag = item;
			foundNodes[0].Nodes.Add(node);
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
