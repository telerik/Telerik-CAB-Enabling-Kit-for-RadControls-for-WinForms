using FinanceApplicationCAB.Infrastructure.Interface.Constants;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace FinanceApplicationCAB.Infrastructure.Layout
{
	public class Module : ModuleInit
	{
		private WorkItem rootWorkItem;

		[InjectionConstructor]
		public Module([ServiceDependency] WorkItem rootWorkItem)
		{
			this.rootWorkItem = rootWorkItem;
		}

		public override void Load()
		{
			base.Load();

			// Add layout view to the shell
			ShellLayoutView _shellLayout = rootWorkItem.Items.AddNew<ShellLayoutView>();
			rootWorkItem.Workspaces[WorkspaceNames.LayoutWorkspace].Show(_shellLayout);

            rootWorkItem.Workspaces.Add(_shellLayout.DockWorkspace, WorkspaceNames.DockWorkspace);
		}
	}
}
