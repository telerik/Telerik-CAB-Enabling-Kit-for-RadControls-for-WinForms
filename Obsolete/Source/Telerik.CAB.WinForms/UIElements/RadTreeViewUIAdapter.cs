using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.UIElements;
using Telerik.WinControls.UI;
using Microsoft.Practices.CompositeUI.Utility;

namespace Telerik.CAB.WinForms.UIElements
{
    /// <summary>
	/// An adapter that wraps <see cref="RadTreeView"/> for the use of an <see cref="IUIElementAdapter"/>.
    /// </summary>
    public class RadTreeViewUIAdapter : UIElementAdapter<RadTreeNode>
    {
        private RadTreeView treeView;

        /// <summary>
		/// Initializes a new instance of the <see cref="RadTreeViewUIAdapter"/> class.
        /// </summary>
		/// <param name="treeView">The <see cref="RadTreeView"/> represented by the UI adapter.</param>
        public RadTreeViewUIAdapter(RadTreeView treeView)
        {
			Guard.ArgumentNotNull(treeView, "treeView");
            this.treeView = treeView;
        }

        /// <summary>
        /// Adds a <see cref="RadTreeNode"/> to the collection associated with the adapter.
        /// </summary>
        /// <param name="uiElement">The node to add.</param>
        /// <returns>The added node.</returns>
        protected override RadTreeNode Add(RadTreeNode uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");
            this.treeView.Nodes.Add(uiElement);
            return uiElement;
        }


        /// <summary>
        /// Removes the specified <see cref="RadTreeNode"/> from the associated collection.
        /// </summary>
        /// <param name="uiElement">The item to be removed.</param>
        protected override void Remove(RadTreeNode uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");
			this.treeView.Nodes.Remove(uiElement);
        }

        /// <summary>
        /// The <see cref="RadTreeView"/> that is represented by the adapter.
        /// </summary>
        public RadTreeView RadTreeView
        {
            get
			{
				return this.treeView;
			}
        }
    }
}
