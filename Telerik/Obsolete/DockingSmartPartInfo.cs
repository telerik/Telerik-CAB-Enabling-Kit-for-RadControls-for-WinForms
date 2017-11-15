using System;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Practices.CompositeUI.SmartParts;
using Telerik.WinControls.UI.Docking;

namespace Telerik.CAB.WinForms.SmartPartInfos
{
    /// <summary>
	/// Provides smart part  information for RadDockableWorkspace.
    /// </summary>
    [Obsolete()]
    public class DockingSmartPartInfo : SmartPartInfo
    {
        #region Private Members
        
        private int height;
        private int width;
        private string parentName = null;
        private bool autoHideOnLoad = false;
        private bool tabbedDocument = false;
		private bool captionVisible = true;
		private bool closeButtonVisible = true;
		private bool dropDownButtonVisible = true;
		private bool hideButtonVisible = true;
		private bool tabStripVisible = true;
		private Color backColor = SystemColors.Control;
        private Image image = null;
        private DockPosition dockPosition = DockPosition.Left;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DockingSmartPartInfo"/> class.
        /// </summary>
        public DockingSmartPartInfo()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DockingSmartPartInfo"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        public DockingSmartPartInfo(string title, string description)
            : base(title, description)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DockingSmartPartInfo"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
		/// <param name="dockPosition">The dock position.</param>
		public DockingSmartPartInfo(string title, string description, DockPosition dockPosition)
            : this(title, description)
        {
			this.dockPosition = dockPosition;
        }
        #endregion

        #region Properties

		/// <summary>
		/// Gets or sets the backdound color of the dock window.
		/// </summary>
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}
			set
			{
				this.backColor = value;
			}
		}

        /// <summary>
        /// Gets or sets the docking position of the docked window.
        /// </summary>
        /// <value>The docking position.</value>
        [Category("Layout")]
		public DockPosition DockPosition
        {
            get
			{
				return this.dockPosition;
			}
            set
			{
				this.dockPosition = value;
			}
        }

        /// <summary>
        /// Gets/sets the name of the parent control to which the control should be docked. If this is not specified
        ///  the DockingManager will be used.
        /// </summary>
        public string ParentName
        {
            get
            {
                return parentName;
            }
            set
            {
                parentName = value;
            }
        }
        /// <summary>
        /// Gets/sets whether the control should autohide on load.
        /// </summary>
        [Category("Layout")]
        public bool AutoHideOnLoad
        {
            get
            {
                return autoHideOnLoad;
            }
            set
            {
                autoHideOnLoad = value;
            }
        }

        /// <summary>
        /// Gets/Sets the Height of the dockable window
        /// </summary>
        [Category("Layout")]
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        /// <summary>
		/// Gets/Sets the Width of the dockable window
        /// </summary>
        [Category("Layout")]
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

		/// <summary>
		/// Gets or sets the image for the dockable window.
		/// </summary>
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the caption is visible.
		/// </summary>
		public bool CaptionVisible
		{
			get
			{
				return this.captionVisible;
			}
			set
			{
				this.captionVisible = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the close button is visible.
		/// </summary>
		public bool CloseButtonVisible
		{
			get
			{
				return this.closeButtonVisible;
			}
			set
			{
				this.closeButtonVisible = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the drop down button is visible.
		/// </summary>
		public bool DropDownButtonVisible
		{
			get
			{
				return this.dropDownButtonVisible;
			}
			set
			{
				this.dropDownButtonVisible = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the hide button is visible.
		/// </summary>
		public bool HideButtonVisible
		{
			get
			{
				return this.hideButtonVisible;
			}
			set
			{
				this.hideButtonVisible = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the tab strip should be visible.
		/// </summary>
		public bool TabStripVisible
		{
			get
			{
				return this.tabStripVisible;
			}
			set
			{
				this.tabStripVisible = value;
			}
		}

		public bool TabbedDocument
		{
			get
			{
				return this.tabbedDocument;
			}
			set
			{
				this.tabbedDocument = value;
			}
		}

        #endregion
    }
}
