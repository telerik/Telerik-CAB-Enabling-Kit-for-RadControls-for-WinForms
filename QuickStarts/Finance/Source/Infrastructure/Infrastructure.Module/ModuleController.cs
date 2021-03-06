﻿//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add CAB Module" recipe.
//
// This class contains placeholder methods for the common module initialization 
// tasks, such as adding services, or user-interface element
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.scsf.2007may/SCSF/html/03-01-010-How_to_Create_Smart_Client_Solutions.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using FinanceApplicationCAB.Infrastructure.Interface;
using FinanceApplicationCAB.Infrastructure.Interface.Constants;
using FinanceApplicationCAB.Infrastructure.Module.Services;
using Microsoft.Practices.CompositeUI.Commands;
using Telerik.WinControls.CompositeUI;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;

namespace FinanceApplicationCAB.Infrastructure.Module
{
	public class ModuleController : WorkItemController
	{
		public override void Run()
		{
			this.LoadStockPortfolio();
			this.AddServices();
			this.ExtendMenu();
			this.ExtendToolStrip();
			this.AddViews();
		}

		private void LoadStockPortfolio()
		{
			StockPortfolio stockPortfolio = new StockPortfolio();
			stockPortfolio.LoadData("Equity.xml");

			this.WorkItem.State[StateKeys.StockPortfolio] = stockPortfolio;
		}

		private void AddServices()
		{
			//TODO: add services provided by the Module. See: Add or AddNew method in WorkItem.Services collection or 
			//		See: ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.2005Nov.cab/CAB/html/03-020-Adding%20Services.htm

			ISelectionService selectionService = new SelectionService(this.WorkItem);
			this.WorkItem.Services.Add<ISelectionService>(selectionService);
		}

		private void ExtendMenu()
		{
			//TODO: add menu items here, normally by calling the "Add" method on
			//		on the WorkItem.UIExtensionSites collection. For an example 
			//		See: ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.scsf.2007may/SCSF/html/02-04-340-Showing_UIElements.htm

			RadMenuItem showStocksInMenuItem = new RadMenuItem("Show Stocks In");
			this.WorkItem.UIExtensionSites[UIExtensionSiteNames.MainMenu].Add<RadMenuItem>(showStocksInMenuItem);

			RadMenuItem treeViewMenuItem = new RadMenuItem("TreeView");
			this.WorkItem.Commands[CommandNames.ShowStocksInTreeView].AddInvoker(treeViewMenuItem, "Click");

			RadMenuItem panelBarMenuItem = new RadMenuItem("PanelBar");
			this.WorkItem.Commands[CommandNames.ShowStocksInPanelBar].AddInvoker(panelBarMenuItem, "Click");

			showStocksInMenuItem.Items.Add(panelBarMenuItem);
			showStocksInMenuItem.Items.Add(treeViewMenuItem);

			this.WorkItem.Commands[CommandNames.ShowStocksInPanelBar].Status = CommandStatus.Disabled;
			this.WorkItem.Commands[CommandNames.ShowStocksInTreeView].Status = CommandStatus.Enabled;
		}

        DockWindowSmartPartInfo PortfolioViewInfo
		{
			get
			{
                DockWindowSmartPartInfo portfolioInfo = new DockWindowSmartPartInfo();
				portfolioInfo.Title = "Stock Portfolio";
                portfolioInfo.DockType = DockType.ToolWindow;
                portfolioInfo.Size = new System.Drawing.Size(250, 300);
				portfolioInfo.DockPosition = DockPosition.Left;

				return portfolioInfo;
			}
		}

		[CommandHandler(CommandNames.ShowStocksInPanelBar)]
		public void panelBarMenuItem_Click(object sender, EventArgs e)
		{
			object portfolioTreeView = this.WorkItem.SmartParts["StockPortfolioTreeView"];
			this.WorkItem.Workspaces[WorkspaceNames.DockWorkspace].Hide(portfolioTreeView);

			this.ShowViewInWorkspace<StockPortfolioView>("StockPortfolioView", WorkspaceNames.DockWorkspace, this.PortfolioViewInfo);

			this.WorkItem.Commands[CommandNames.ShowStocksInPanelBar].Status = CommandStatus.Disabled;
			this.WorkItem.Commands[CommandNames.ShowStocksInTreeView].Status = CommandStatus.Enabled;
		}

		[CommandHandler(CommandNames.ShowStocksInTreeView)]
		public void treeViewMenuItem_Click(object sender, EventArgs e)
		{
			object panelBarView = this.WorkItem.SmartParts["StockPortfolioView"];
			this.WorkItem.Workspaces[WorkspaceNames.DockWorkspace].Hide(panelBarView);

			this.ShowViewInWorkspace<StockPortfolioTreeView>("StockPortfolioTreeView", WorkspaceNames.DockWorkspace, this.PortfolioViewInfo);

			this.WorkItem.Commands[CommandNames.ShowStocksInPanelBar].Status = CommandStatus.Enabled;
			this.WorkItem.Commands[CommandNames.ShowStocksInTreeView].Status = CommandStatus.Disabled;
		}

		private void ExtendToolStrip()
		{
			//TODO: add new items to the ToolStrip in the Shell. See the UIExtensionSites collection in the WorkItem. 
			//		See: ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.scsf.2007may/SCSF/html/02-04-340-Showing_UIElements.htm
		}

		private void AddViews()
		{
			//TODO: create the Module views, add them to the WorkItem and show them in 
			//		a Workspace. See: ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.scsf.2007may/SCSF/html/03-01-040-How_to_Add_a_View_with_a_Presenter.htm
			
            this.ShowViewInWorkspace<StockPortfolioView>("StockPortfolioView", WorkspaceNames.DockWorkspace, this.PortfolioViewInfo);

            DockWindowSmartPartInfo gridInfo = new DockWindowSmartPartInfo();
            gridInfo.Title = "Stock Details Grid";
            gridInfo.Name = "StockGridView";
            this.ShowViewInWorkspace<StockGridView>("StockGridView", WorkspaceNames.DockWorkspace, gridInfo);

            DockWindowSmartPartInfo chartInfo = new DockWindowSmartPartInfo();
			chartInfo.Title = "Stock Chart";
            chartInfo.Name = "StockChartView";
            chartInfo.DockTarget = gridInfo;
            chartInfo.DockPosition = DockPosition.Bottom;
			this.ShowViewInWorkspace<StockChartView>("StockChartView", WorkspaceNames.DockWorkspace, chartInfo);

            DockWindowSmartPartInfo detailsInfo = new DockWindowSmartPartInfo();
			detailsInfo.Title = "Stock Details";
            detailsInfo.Name = "StockDetailsView";
            detailsInfo.DockType = DockType.ToolWindow;
            detailsInfo.Size = new System.Drawing.Size(200, 200);
			detailsInfo.DockPosition = DockPosition.Right;
			this.ShowViewInWorkspace<StockDetailsView>("StockDetailsView", WorkspaceNames.DockWorkspace, detailsInfo);

            DockWindowSmartPartInfo newsInfo = new DockWindowSmartPartInfo();
            newsInfo.Title = "News and Reports";
            newsInfo.Name = "StockNewsView";
            newsInfo.DockType = DockType.ToolWindow;
            newsInfo.DockTarget = detailsInfo;
            newsInfo.Size = new System.Drawing.Size(300, 300);
			newsInfo.DockPosition = DockPosition.Bottom;
			this.ShowViewInWorkspace<StockNewsView>("StockNewsView", WorkspaceNames.DockWorkspace, newsInfo);
		}

		//TODO: Add CommandHandlers and/or Event Subscriptions
		//		See ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.scsf.2007may/SCSF/html/02-04-350-Registering_Commands.htm
		//		See ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.scsf.2007may/SCSF/html/02-04-320-Publishing_and_Subscribing_to_Events.htm
	}
}
