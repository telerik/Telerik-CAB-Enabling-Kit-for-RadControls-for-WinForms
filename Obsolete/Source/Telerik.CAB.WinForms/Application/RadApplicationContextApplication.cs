using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;

namespace Telerik.CAB.WinForms
{
    /// <summary>
    /// A CAB shell application class used to start an application using a specified ApplicationContext.
    /// </summary>
    /// <typeparam name="TWorkItem">The type of the root application work item.</typeparam>
    /// <typeparam name="TShell">The type for the shell to use.</typeparam>
    public class RadApplicationContextApplication<TWorkItem, TShell> :
        RadWindowsFormsApplication<TWorkItem, TShell> where TWorkItem : WorkItem, new()
        where TShell : ApplicationContext
    {
        # region Start

        /// <summary>
        /// Calls <see cref="Application.Run(ApplicationContext)"/> to start the application.
        /// </summary>
        protected override void Start()
        {
            Application.Run(this.Shell);
        }

        # endregion
    }
}
