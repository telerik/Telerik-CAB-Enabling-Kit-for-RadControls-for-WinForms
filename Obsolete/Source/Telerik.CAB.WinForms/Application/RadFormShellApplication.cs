using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using System.Windows.Forms;

namespace Telerik.CAB.WinForms
{
    /// <summary>
    /// A CAB shell application class used to start an application using a specified Form.
    /// </summary>
    /// <typeparam name="TWorkitem">The type of the root application work item.</typeparam>
    /// <typeparam name="TShell">The type of the form for the shell to use.</typeparam>
    public class RadFormShellApplication<TWorkItem, TShell> : RadWindowsFormsApplication<TWorkItem, TShell>
        where TWorkItem : WorkItem, new()
        where TShell : Form
    {
        #region Start

        /// <summary>
        /// Calls <see cref="Application.Run(Form)"/> to start the application.
        /// </summary>
        protected override void Start()
        {
            Application.Run(this.Shell);
        }

        #endregion
    }
}
