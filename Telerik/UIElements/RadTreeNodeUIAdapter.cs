using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    /// <summary>
    /// An adapter that wraps a <see cref="RadTreeNode"/> for use as an <see cref="IUIElementAdapter"/> 
    /// and uses the location of the wrapped item to determine where new items are positioned.
    /// </summary>
	public class RadTreeNodeUIAdapter : UIElementAdapter<RadTreeNode>
    {
        private RadTreeNode treeNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="RadTreeNodeUIAdapter"/> class.
        /// </summary>
		/// <param name="treeNode">Node whose owning collection will be updated with any added elements.</param>
        public RadTreeNodeUIAdapter(RadTreeNode treeNode)
		{
            Guard.ArgumentNotNull(treeNode, "treeNode");
            this.treeNode = treeNode;
        }

		/// <summary>
		/// Adds a <see cref="RadTreeNode"/> to the collection associated with the adapter.
		/// </summary>
		/// <param name="uiElement">The node to add.</param>
		/// <returns>The added node.</returns>
		protected override RadTreeNode Add(RadTreeNode uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "uiElement");
			this.treeNode.Nodes.Add(uiElement);
			return uiElement;
		}

		/// <summary>
		/// Removes the specified <see cref="RadTreeNode"/> from the associated collection.
		/// </summary>
		/// <param name="uiElement">The item to be removed.</param>
		protected override void Remove(RadTreeNode uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "uiElement");
			this.treeNode.Nodes.Remove(uiElement);
		}

		/// <summary>
		/// The <see cref="RadTreeNode"/> that is represented by the adapter.
		/// </summary>
		protected RadTreeNode RadTreeNode
		{
			get { return this.treeNode; }
		}
	}
}
