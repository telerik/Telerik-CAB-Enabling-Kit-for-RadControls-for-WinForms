//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory 2010
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Windows.Forms;
using BankTellerCommon;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Telerik.WinControls.UI;

namespace BankTellerModule
{
	// This is the initialization class for the Module. Any classes in the assembly that
	// derive from Microsoft.Practices.CompositeUI.ModuleInit will automatically
	// be created and called for initialization.

	public class BankTellerModuleInit : ModuleInit
	{
		private WorkItem workItem;
		
		[InjectionConstructor]
		public BankTellerModuleInit([ServiceDependency] WorkItem workItem)
		{
			this.workItem = workItem;
		}

		public override void Load()
		{
			AddCustomerMenuItem();

			//Retrieve well known workspaces
			IWorkspace sideBarWorkspace = workItem.Workspaces[WorkspacesConstants.SHELL_SIDEBAR];
			IWorkspace contentWorkspace = workItem.Workspaces[WorkspacesConstants.SHELL_CONTENT];

			BankTellerWorkItem bankTellerWorkItem = workItem.WorkItems.AddNew<BankTellerWorkItem>();
			bankTellerWorkItem.Show(sideBarWorkspace, contentWorkspace);
		}

		private void AddCustomerMenuItem()
		{
            RadMenuItem customerItem = new RadMenuItem("Customer");
			workItem.UIExtensionSites[UIExtensionConstants.FILE].Add(customerItem);
			workItem.UIExtensionSites.RegisterSite(Properties.Resources.CustomerMenuExtensionSite, customerItem.Items);
		}
	}
}
