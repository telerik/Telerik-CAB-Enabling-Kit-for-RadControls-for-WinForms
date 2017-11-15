using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.WinForms;
using Telerik.CAB.WinForms.Commands;
using Telerik.CAB.WinForms.UIElements;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Telerik.CAB.WinForms
{
    /// <summary>
    /// Defines an abstact cab application which shows a shell based on a Form that uses RadControls for 
    /// WinForms components.
    /// </summary>
    /// <typeparam name="TWorkItem">The type of the root application work item.</typeparam>
    /// <typeparam name="TShell">The type for the shell to use.</typeparam>
    public abstract class RadWindowsFormsApplication<TWorkItem, TShell> : 
        WindowsFormsApplication<TWorkItem, TShell> where TWorkItem : WorkItem, new()
    {
        protected RadWindowsFormsApplication()
            : base()
        {
        }

        # region AfterShellCreated

        /// <summary>
        /// Refer <see cref="CabShellApplication{T,S}.AfterShellCreated"/>.
        /// </summary>
        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();

            //Registers Command Adapters.
            ICommandAdapterMapService mapService = this.RootWorkItem.Services.Get<ICommandAdapterMapService>();
            mapService.Register(typeof(RadMenuItem), typeof(RadMenuItemCommandAdapter));
            mapService.Register(typeof(RadTreeNode), typeof(RadTreeNodeCommandAdapter));
			mapService.Register(typeof(RadElement), typeof(RadElementCommandAdapter));

			// Registers UIElement Adapter Factories.
			IUIElementAdapterFactoryCatalog catalog = this.RootWorkItem.Services.Get<IUIElementAdapterFactoryCatalog>();
			catalog.RegisterFactory(new RadMenuUIAdapterFactory());
			catalog.RegisterFactory(new RadToolStripAdapterFactory());
			catalog.RegisterFactory(new RadTreeViewUIAdapterFactory());
			catalog.RegisterFactory(new RadPanelBarUIAdapterFactory());
        }

        #endregion
    }
}
