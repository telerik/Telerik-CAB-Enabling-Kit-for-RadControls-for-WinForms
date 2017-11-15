using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using FinanceApplicationCAB.Infrastructure.Interface.Constants;
using FinanceApplicationCAB.Infrastructure.Library.UI;
using Telerik.CAB.WinForms.WorkSpaces;

namespace FinanceApplicationCAB.Infrastructure.Layout
{
	public class Module : ModuleInit
	{
		private WorkItem _rootWorkItem;

		[InjectionConstructor]
		public Module([ServiceDependency] WorkItem rootWorkItem)
		{
			_rootWorkItem = rootWorkItem;
		}

		public override void Load()
		{
			base.Load();

			// Add layout view to the shell
			ShellLayoutView _shellLayout = _rootWorkItem.Items.AddNew<ShellLayoutView>();
			_rootWorkItem.Workspaces[WorkspaceNames.LayoutWorkspace].Show(_shellLayout);

			//// Add window workspace to be used for modal windows
			//WindowWorkspace wsp = new WindowWorkspace(_shellLayout.ParentForm);
			//_rootWorkItem.Workspaces.Add(wsp, WorkspaceNames.ModalWindows);

			RadDockableWorkspace dockableWorkspace = new RadDockableWorkspace(_shellLayout.DockingManager, _shellLayout);
			_rootWorkItem.Workspaces.Add(dockableWorkspace, WorkspaceNames.DockableWorkspace);
		}
	}
}
