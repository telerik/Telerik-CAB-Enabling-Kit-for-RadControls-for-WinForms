using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.WinForms;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    public class RadFormMdiWorkspace : RadFormWorkspace
    {
        private RadForm parentMdiForm;

		/// <summary>
		/// Constructor specifying the parent form of the MDI child.
		/// </summary>
        public RadFormMdiWorkspace(RadForm parentForm)
			: base()
		{
			this.parentMdiForm = parentForm;
			this.parentMdiForm.IsMdiContainer = true;
		}

		/// <summary>
		/// Gets the parent MDI form.
		/// </summary>
		public RadForm ParentMdiForm
		{
			get { return parentMdiForm; }
		}

		/// <summary>
		/// Shows the form as a child of the specified <see cref="ParentMdiForm"/>.
		/// </summary>
		/// <param name="smartPart">The <see cref="Control"/> to show in the workspace.</param>
		/// <param name="smartPartInfo">The information to use to show the smart part.</param>
		protected override void OnShow(Control smartPart, WindowSmartPartInfo smartPartInfo)
		{
            smartPart.Dock = DockStyle.Fill;
			Form mdiChild = this.GetOrCreateForm(smartPart);
			mdiChild.MdiParent = parentMdiForm;

			this.SetWindowProperties(mdiChild, smartPartInfo);
			mdiChild.Show();
			this.SetWindowLocation(mdiChild, smartPartInfo);
			mdiChild.BringToFront();
		}
    }
}
